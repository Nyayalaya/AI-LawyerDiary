using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Infrastructure.AI.Helpers;
using Microsoft.SemanticKernel;
using System;

using System.ComponentModel;

using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Plugins.CasePlugin
{
    public sealed partial class CasePlugin
    {
        [KernelFunction]
        [Description( "Returns complete details of a case using the Case Id. " +
                      "Use this function whenever the user asks about a specific case.")]
        public async Task<Result<CaseDetailInfoDto>> GetCaseDetailsAsync(Guid caseId,
    CancellationToken cancellationToken = default)
        {
            PluginValidator.EnsureGuid(
                caseId,
                nameof(caseId));

            var query = new GetCaseDetailInfoQuery
            {
                CaseId = caseId
            };

            return await SendAsync(
                query,
                nameof(GetCaseDetailsAsync),
                cancellationToken);
        }
    }
}
