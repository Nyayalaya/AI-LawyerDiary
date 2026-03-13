using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CourtApp.Application.Features.Auth.Commands;

namespace CourtApp.Api.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor)
            : base(mediator, httpContextAccessor)
        {
        }

        /// <summary>
        /// Register a new user account
        /// </summary>
        /// <param name="registerCommand">Registration details including user type, email, password, and user-specific info</param>
        /// <returns>Returns user ID if registration is successful</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand registerCommand)
        {
            
            var result = await Mediator.Send(registerCommand, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Authenticate user and get JWT token
        /// </summary>
        /// <param name="command">Login credentials (email and password)</param>
        /// <returns>Returns JWT token if authentication is successful</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            command.IpAddress = GetClientIpAddress();
            var result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Confirm user email address
        /// </summary>
        /// <param name="command">User ID and confirmation code</param>
        /// <returns>Returns success message if email is confirmed</returns>
        [HttpPost("confirm-email")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Request password reset email
        /// </summary>
        /// <param name="command">Email address for password reset</param>
        /// <returns>Returns success message</returns>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordCommand command)
        {
            command.Origin = $"{Request.Scheme}://{Request.Host}";
            var result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="command">Email, new password, and reset token</param>
        /// <returns>Returns success message if password is reset</returns>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        
    }
}
