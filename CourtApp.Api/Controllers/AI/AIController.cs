using CourtApp.Application.Common;
using CourtApp.Infrastructure.AI.Models.Requests;
using CourtApp.Infrastructure.AI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourtApp.Api.Controllers
{

    public sealed class AIController : BaseController
    {
        private readonly ILawyerAssistant _lawyerAssistant;
        private readonly ILogger<AIController> _logger;

        public AIController(
            ILawyerAssistant lawyerAssistant,
            ILogger<AIController> logger)
        {
            _lawyerAssistant = lawyerAssistant;
            _logger = logger;
        }

        /// <summary>
        /// AI Chat endpoint.
        /// Uses Semantic Kernel with Gemini Function Calling.
        /// </summary>
        [HttpPost("chat")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            if (request == null)
                return Failure("Request cannot be null.");

            if (string.IsNullOrWhiteSpace(request.Prompt))
                return Failure("Prompt cannot be empty.");

            try
            {
                // Enrich request with user information
                request.Metadata ??= new Dictionary<string, object>();

                request.Metadata["UserId"] = UserId;
                request.Metadata["UserName"] = UserName ?? string.Empty;
                request.Metadata["UserEmail"] = UserEmail ?? string.Empty;
                request.Metadata["Roles"] = Roles;
                request.Metadata["CorrelationId"] = CorrelationId;
                request.Metadata["RequestOrigin"] = RequestOrigin;

                var response = await _lawyerAssistant.ChatAsync(request, RequestAborted);

                return Success(response);
            }
            catch (OperationCanceledException)
            {
                return Failure(
                    "Request was cancelled.",
                    StatusCodes.Status499ClientClosedRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "AI Chat failed. CorrelationId: {CorrelationId}",
                    CorrelationId);

                return ServerError(
                    "Unable to process AI request.");
            }
        }
    }
}
