using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Common
{
    public sealed class AIPluginException : Exception
    {
        public string ErrorCode { get; }

        public AIPluginException(
            string errorCode,
            string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public AIPluginException(
            string errorCode,
            string message,
            Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
