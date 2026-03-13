
using AspNetCoreHero.ThrowR;
using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Mail;
using CourtApp.Application.DTOs.Settings;
using CourtApp.Application.Features.Auth.Dto;
using CourtApp.Application.Features.Auth.Services;
using CourtApp.Application.Interfaces.Shared;
using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.DbContexts;
using CourtApp.Infrastructure.Email.Templates;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IdentityService> _logger;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMailService _mailService;
        private readonly IdentityContext _identityDbContext;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IMailService mailService,
            ILogger<IdentityService> logger,
            IdentityContext identityDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _mailService = mailService;
            _logger = logger;
            _identityDbContext = identityDbContext;
        }

        public async Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                Throw.Exception.IfNull(user, nameof(user), $"No Accounts Registered with {request.Email}.");

                ValidateUserForLogin(user);

                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
                Throw.Exception.IfFalse(result.Succeeded, $"Invalid Credentials for '{request.Email}'.");

                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user, ipAddress);
                var response = BuildTokenResponse(user, jwtSecurityToken);
                var refreshToken = GenerateRefreshToken(ipAddress);
                response.RefreshToken = refreshToken.Token;

                _logger.LogInformation($"User {user.Email} logged in successfully");
                return await Result<TokenResponse>.SuccessAsync(response, "Authenticated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTokenAsync: {ex.Message}");
                return await Result<TokenResponse>.FailAsync(ex.Message);
            }
        }

        public async Task<Result<string>> RegisterAsync(RegisterRequest request)
        {
            try
            {
                ValidateRegistrationRequest(request);

                var userExists = await _userManager.FindByEmailAsync(request.Email);
                Throw.Exception.IfNotNull(userExists,$"Email '{request.Email}' is already registered.");

                var user = CreateApplicationUser(request);
                var result = await _userManager.CreateAsync(user, request.Password);

                Throw.Exception.IfFalse(result.Succeeded, FormatIdentityErrors(result));

                // Add role based on user type
                await _userManager.AddToRoleAsync(user, request.UserType.ToString());

                // Add corporate user if needed
                if (request.UserType == RegisterType.CORPORATE)
                {
                    await AddCorporateUser(user, request.CompanyInfoDto);
                }

                // Generate verification URI and send email
                var verificationUri = await GenerateVerificationUri(user, request.Origin);
                await SendVerificationEmail(user, verificationUri);

                _logger.LogInformation($"User {user.Email} registered successfully as {request.UserType}");
                return Result<string>.Success(user.Id, $"User Registered successfully. Confirmation email has been sent to {user.Email}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in RegisterAsync: {ex.Message}");
                return await Result<string>.FailAsync(ex.Message);
            }
        }

        public async Task<Result<string>> ConfirmEmailAsync(string userId, string code)
        {
            try
            {
                //Throw.Exception.IfNullOrEmpty(userId, nameof(userId), "User ID is required.");
                //Throw.Exception.IfNullOrEmpty(code, nameof(code), "Confirmation code is required.");

                var user = await _userManager.FindByIdAsync(userId);
                Throw.Exception.IfNull(user, nameof(user), "User not found.");

                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, code);

                Throw.Exception.IfFalse(result.Succeeded, $"Error confirming email for {user.Email}.");

                _logger.LogInformation($"Email confirmed for user {user.Email}");
                return Result<string>.Success(user.Id, $"Account Confirmed for {user.Email}. You can now login.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ConfirmEmailAsync: {ex.Message}");
                return await Result<string>.FailAsync(ex.Message);
            }
        }

        public async Task<Result<string>> ForgotPasswordAsync(ForgotPasswordRequest model, string origin)
        {
            try
            {
                
                var account = await _userManager.FindByEmailAsync(model.Email);

                if (account == null)
                {
                    _logger.LogWarning($"Forgot password attempt for non-existent email: {model.Email}");
                    return Result<string>.Success(string.Empty, "If an account exists, a reset link has been sent.");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(account);
                var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var resetLink = $"{origin}/api/identity/reset-password?email={account.Email}&code={encodedCode}";

                var mailRequest = new MailRequest
                {
                    To = account.Email,
                    Subject = "Reset Your Password - Court App",
                    Body = BuildPasswordResetEmail(account.FirstName, resetLink),
                    IsHtml = true
                };

                await _mailService.SendAsync(mailRequest);
                _logger.LogInformation($"Password reset email sent to {account.Email}");

                return Result<string>.Success(string.Empty, "If an account exists, a reset link has been sent.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ForgotPasswordAsync: {ex.Message}");
                return await Result<string>.FailAsync(ex.Message);
            }
        }

        public async Task<Result<string>> ResetPasswordAsync(ResetPasswordRequest model)
        {
            try
            {
                Throw.Exception.IfNull(model, nameof(model), "Reset password request cannot be null.");
                
                var account = await _userManager.FindByEmailAsync(model.Email);
                Throw.Exception.IfNull(account, nameof(account), $"No account found for {model.Email}.");

                var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
                Throw.Exception.IfFalse(result.Succeeded, FormatIdentityErrors(result));

                _logger.LogInformation($"Password reset successful for {model.Email}");
                return Result<string>.Success(model.Email, "Password has been reset successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ResetPasswordAsync: {ex.Message}");
                return await Result<string>.FailAsync(ex.Message);
            }
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                var user = await _userManager.FindByEmailAsync(email);
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in IsEmailExistAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsContactExistAsync(string contact)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contact))
                    return false;

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Mobile == contact);
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in IsContactExistAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsEnrollmentExistAsync(string enrollment)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(enrollment))
                    return false;

                var user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.ProfessionalInfo != null && u.ProfessionalInfo.EnrollmentNo == enrollment);
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in IsEnrollmentExistAsync: {ex.Message}");
                return false;
            }
        }

        #region Private Methods

        private void ValidateUserForLogin(ApplicationUser user)
        {
            Throw.Exception.IfFalse(user.EmailConfirmed, $"Email is not confirmed for '{user.Email}'. Please check your email.");
            Throw.Exception.IfFalse(user.IsActive, $"Account for '{user.Email}' is inactive. Please contact support.");
        }

        private void ValidateRegistrationRequest(RegisterRequest request)
        {
            Throw.Exception.IfNull(request, nameof(request), "Registration request cannot be null.");
            
            if (request.UserType == RegisterType.LAWYER || request.UserType == RegisterType.CLIENT)
            {
                Throw.Exception.IfNull(request.IndividualInfoDto, nameof(request.IndividualInfoDto), "Individual information is required for this user type.");
            }

            if (request.UserType == RegisterType.CORPORATE)
            {
                Throw.Exception.IfNull(request.CompanyInfoDto, nameof(request.CompanyInfoDto), "Company information is required for corporate registration.");
            }
        }

        private ApplicationUser CreateApplicationUser(RegisterRequest request)
        {
            var individualInfo = request.IndividualInfoDto;
            var userName = new MailAddress(request.Email).User;

            var user = new ApplicationUser
            {
                UserType = request.UserType.ToString(),
                UserName = userName,
                Email = request.Email,
                FirstName = individualInfo?.FirstName?.Trim().ToUpper() ?? string.Empty,
                LastName = individualInfo?.LastName?.Trim().ToUpper() ?? string.Empty,
                
                Mobile = request.Contact,
                IsActive = true
                
            };

            // Add professional info for lawyers
            if (request.UserType == RegisterType.LAWYER && individualInfo != null)
            {
                user.ProfessionalInfo = new ProfessionalInfo
                {
                    EnrollmentNo = individualInfo.EnrollmentNumber ?? string.Empty,
                    BarAssociationNumber = string.Empty,
                    PracticeLicenseDate = default,
                    PracticeSince = 0,
                    Specializations = null
                };
            }

            return user;
        }

        private async Task AddCorporateUser(ApplicationUser user, CompanyInfoDto companyInfo)
        {
            try
            {
                var corporateUser = new CorporateUser
                {
                    Id = user.Id,
                    FirmName = companyInfo?.CompanyName ?? string.Empty,
                    RegistrationNo = companyInfo?.RegistrationNumber ?? string.Empty
                    //IncorporationDate = companyInfo?.IncorporationDate,
                    //GstNumber = companyInfo?.GstNumber ?? string.Empty,
                    //AutherizedPerson = companyInfo?.AutherizedPerson ?? string.Empty,
                    //CreatedOn = DateTime.UtcNow
                };

                _identityDbContext.Corporates.Add(corporateUser);
                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddCorporateUser: {ex.Message}");
                throw;
            }
        }

        private async Task<string> GenerateVerificationUri(ApplicationUser user, string origin)
        {
            try
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var verificationUri = $"{origin}/api/identity/confirm-email?userId={user.Id}&code={encodedCode}";
                return verificationUri;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GenerateVerificationUri: {ex.Message}");
                throw;
            }
        }

        private async Task SendVerificationEmail(ApplicationUser user, string verificationUri)
        {
            try
            {
                var emailBody = RegistrationEmailTemplate.GetTemplate(
                    user.UserName,
                    user.FirstName,
                    user.LastName,
                    verificationUri
                );

                var mailRequest = new MailRequest
                {
                    To = user.Email,
                    Subject = "Confirm Your Email Address - Court App",
                    Body = emailBody,
                    IsHtml = true
                };

                await _mailService.SendAsync(mailRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SendVerificationEmail: {ex.Message}");
                throw;
            }
        }

        private TokenResponse BuildTokenResponse(ApplicationUser user, JwtSecurityToken jwtSecurityToken)
        {
            return new TokenResponse
            {
                Id = user.Id,
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime(),
                ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime(),
                Email = user.Email,
                UserName = user.UserName,
                IsVerified = user.EmailConfirmed
            };
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, string ipAddress)
        {
            try
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id),
                    new Claim("first_name", user.FirstName ?? string.Empty),
                    new Claim("last_name", user.LastName ?? string.Empty),
                    new Claim("full_name", $"{user.FirstName} {user.LastName}".Trim()),
                    new Claim("ip", ipAddress ?? "Unknown")
                };

                claims.AddRange(userClaims);
                claims.AddRange(roleClaims);

                return GenerateJwtToken(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GenerateJWToken: {ex.Message}");
                throw;
            }
        }

        private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var randomBytes = new byte[40];
            RandomNumberGenerator.Fill(randomBytes);
            var token = BitConverter.ToString(randomBytes).Replace("-", "");

            return new RefreshToken
            {
                Token = token,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress ?? "Unknown"
            };
        }

        private string FormatIdentityErrors(IdentityResult result)
        {
            return string.Join(", ", result.Errors.Select(e => e.Description));
        }

        private string BuildPasswordResetEmail(string firstName, string resetLink)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; background: #f9f9f9; border-radius: 5px; }}
                        .header {{ background: #007bff; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                        .content {{ background: white; padding: 30px; border-radius: 0 0 5px 5px; }}
                        .button {{ display: inline-block; padding: 12px 30px; background: #007bff; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
                        .footer {{ text-align: center; padding: 20px; color: #666; font-size: 12px; }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <div class=""header"">
                            <h1>Password Reset Request</h1>
                        </div>
                        <div class=""content"">
                            <p>Hi {firstName},</p>
                            <p>We received a request to reset your password. Click the button below to proceed:</p>
                            <a href=""{resetLink}"" class=""button"">Reset Password</a>
                            <p>Or copy and paste this link: <br/><small>{resetLink}</small></p>
                            <p><strong>This link will expire in 1 hour.</strong></p>
                            <p>If you did not request this, please ignore this email.</p>
                        </div>
                        <div class=""footer"">
                            <p>&copy; 2024 Court App. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        #endregion
    }
}