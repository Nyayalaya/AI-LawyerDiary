using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Constants
{
    public static class HeaderConstants
    {
        public const string CorrelationId = "X-Correlation-Id";

        public const string RequestId = "X-Request-Id";

        public const string ConversationId = "X-Conversation-Id";

        public const string SessionId = "X-Session-Id";

        public const string UserId = "X-User-Id";

        public const string TenantId = "X-Tenant-Id";

        public const string AIProvider = "X-AI-Provider";

        public const string AIModel = "X-AI-Model";

        public const string TokenUsage = "X-Token-Usage";
    }
}
