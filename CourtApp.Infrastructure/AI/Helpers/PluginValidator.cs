using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Helpers
{
    public static class PluginValidator
    {
        public static void EnsureGuid(
            Guid value,
            string parameterName)
        {
            if (value == Guid.Empty)
                throw new ArgumentException(
                    $"{parameterName} cannot be empty.",
                    parameterName);
        }

        public static void EnsureNotNull<T>(
            T value,
            string parameterName)
        {
            if (value is null)
                throw new ArgumentNullException(parameterName);
        }

        public static void EnsureNotEmpty(
            string value,
            string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(
                    $"{parameterName} cannot be empty.",
                    parameterName);
        }
    }
}
