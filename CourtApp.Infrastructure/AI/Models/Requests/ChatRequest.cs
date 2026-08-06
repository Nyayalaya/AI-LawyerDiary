using CourtApp.Infrastructure.AI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Models.Requests
{
    public sealed class ChatRequest
    {
        /// <summary>
        /// User question.
        /// </summary>
        public string Prompt { get; set; } = string.Empty;

        /// <summary>
        /// Previous chat history.
        /// </summary>
        public IList<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        

        /// <summary>
        /// Uploaded files.
        /// </summary>
        public IList<ChatAttachment>? Attachments { get; set; }

        /// <summary>
        /// Override Temperature.
        /// </summary>
        public double? Temperature { get; set; }

        /// <summary>
        /// Override Max Tokens.
        /// </summary>
        public int? MaxTokens { get; set; }

        /// <summary>
        /// Enable streaming response.
        /// </summary>
        public bool Stream { get; set; }

        /// <summary>
        /// Enable Semantic Kernel Function Calling.
        /// </summary>
        public bool EnableFunctionCalling { get; set; }

        /// <summary>
        /// Override system prompt.
        /// </summary>
        public string? SystemPrompt { get; set; }

        /// <summary>
        /// Current conversation.
        /// </summary>
        public Guid? ConversationId { get; set; }

        /// <summary>
        /// Logged in user.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Browser session.
        /// </summary>
        public string? SessionId { get; set; }

        public IDictionary<string, object>? Metadata { get; set; }
    }
}
