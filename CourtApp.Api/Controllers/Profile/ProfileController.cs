using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.Commands;
using CourtApp.Application.Features.Profile.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class ProfileController : BaseController
    {
        /// <summary>
        /// Get user profile by user ID
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <returns>User profile details</returns>
        [HttpGet("{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<UserProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserProfileAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return Failure("User ID is required", 400);

            var query = new GetUserProfileQuery(userId);
            var result = await Mediator.Send(query, RequestAborted);
            return result.Succeeded ? FromResult(result) : FromResult(result, 404);
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="request">Profile update data</param>
        /// <returns>Updated profile</returns>
        [HttpPut("{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<UserProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProfileAsync(string userId, [FromBody] UpdateProfileRequest request)
        {
            if (string.IsNullOrEmpty(userId))
                return Failure("User ID is required", 400);

            if (request == null)
                return Failure("Profile data is required", 400);

            request.UserId = userId;
            var command = new UpdateProfileCommand(request);
            var result = await Mediator.Send(command, RequestAborted);
            return result.Succeeded ? Success(result.Data) : FromResult(result);
        }

        /// <summary>
        /// Complete user profile (initial setup)
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="request">Profile completion data</param>
        /// <returns>Completed profile</returns>
        [HttpPost("{userId}/complete")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<UserProfileDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CompleteProfileAsync(string userId, [FromBody] CompleteProfileRequest request)
        {
            if (string.IsNullOrEmpty(userId))
                return Failure("User ID is required", 400);

            if (request == null)
                return Failure("Profile data is required", 400);

            var command = new CompleteProfileCommand(userId, request);
            var result = await Mediator.Send(command, RequestAborted);
            return result.Succeeded ? Created(result.Data) : FromResult(result);
        }

        /// <summary>
        /// Create a new organization
        /// </summary>
        /// <param name="userId">The user ID (owner)</param>
        /// <param name="request">Organization creation data</param>
        /// <returns>Created organization ID</returns>
        [HttpPost("{userId}/organization")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateOrganizationAsync(string userId, [FromBody] CreateOrganizationRequest request)
        {
            if (string.IsNullOrEmpty(userId))
                return Failure("User ID is required", 400);

            if (request == null)
                return Failure("Organization data is required", 400);

            var command = new CreateOrganizationCommand(userId, request);
            var result = await Mediator.Send(command, RequestAborted);
            return result.Succeeded ? Created(result.Data) : FromResult(result);
        }

        /// <summary>
        /// Create a sub-user (Clerk or Associate)
        /// </summary>
        /// <param name="parentUserId">The parent user ID</param>
        /// <param name="request">Sub-user creation data</param>
        /// <returns>Created sub-user ID</returns>
        [HttpPost("{parentUserId}/subuser")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateSubUserAsync(string parentUserId, [FromBody] CreateSubUserRequest request)
        {
            if (string.IsNullOrEmpty(parentUserId))
                return Failure("Parent User ID is required", 400);

            if (request == null)
                return Failure("Sub-user data is required", 400);

            var command = new CreateSubUserCommand(parentUserId, request);
            var result = await Mediator.Send(command, RequestAborted);
            return result.Succeeded ? Created(result.Data) : FromResult(result);
        }

        /// <summary>
        /// Update user organization mapping
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="request">Organization mapping data</param>
        /// <returns>Success status</returns>
        [HttpPut("{userId}/organization-mapping")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUserOrganizationAsync(string userId, [FromBody] UserOrganizationRequest request)
        {
            if (string.IsNullOrEmpty(userId))
                return Failure("User ID is required", 400);

            if (request == null)
                return Failure("Organization data is required", 400);

            request.UserId = userId;
            var command = new UpdateUserOrganizationCommand(userId, request);
            var result = await Mediator.Send(command, RequestAborted);
            return result.Succeeded ? Success(result.Data) : FromResult(result);
        }

        /// <summary>
        /// Add user billing information
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="request">Billing information</param>
        /// <returns>Success status</returns>
        [HttpPost("{userId}/billing-info")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddBillingInfoAsync(string userId, [FromBody] UserBillingInfoDto request)
        {
            if (string.IsNullOrEmpty(userId))
                return Failure("User ID is required", 400);

            if (request == null)
                return Failure("Billing information is required", 400);

            request.UserId = userId;
            var command = new AddUserBillingInfoCommand(userId, request);
            var result = await Mediator.Send(command, RequestAborted);
            return result.Succeeded ? Created(result.Data) : FromResult(result);
        }
    }
}
