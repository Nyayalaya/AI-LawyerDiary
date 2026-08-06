using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Application.Features.Auth.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{
    public sealed class AuthController : BaseController
    {
        // No constructor needed — Mediator resolved lazily from base

        /// <summary>
        /// Register a new user account
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command)
        {
            Result<string> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>
        /// Authenticate user and get JWT token
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<TokenResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            command.IpAddress = CurrentUser.IpAddress;  // from base via CurrentUserService
            Result<TokenResponse> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Confirm user email address
        /// </summary>
        [HttpPost("confirm-email")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailCommand command)
        {
            Result result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Request password reset email
        /// </summary>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordCommand command)
        {
            command.Origin = RequestOrigin;  // helper property on base (see below)
            Result result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Reset user password
        /// </summary>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordCommand command)
        {
            Result result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Refresh JWT token using refresh token
        /// </summary>
        //[HttpPost("refresh-token")]
        //[AllowAnonymous]
        //[ProducesResponseType(typeof(ApiResponse<TokenResponse>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        //public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand command)
        //{
        //    command.IpAddress = CurrentUser.IpAddress;
        //    Result<TokenResponse> result = await Mediator.Send(command, RequestAborted);
        //    return FromResult(result);
        //}

        /// <summary>
        /// Logout and revoke refresh token
        /// </summary>
        //[HttpPost("logout")]
        //[ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        //public async Task<IActionResult> LogoutAsync([FromBody] LogoutCommand command)
        //{
        //    command.UserId = UserId;        // from base
        //    command.IpAddress = CurrentUser.IpAddress;
        //    Result result = await Mediator.Send(command, RequestAborted);
        //    return FromResult(result);
        //}
    }
}
