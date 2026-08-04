using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Dtos;
using CourtApp.Application.Features.CaseDetails.Queries;
using CourtApp.Infrastructure.AI.Helpers;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Plugins.DocumentPlugin
{
    public sealed partial class DocumentPlugin
    {
        [KernelFunction]
        [Description("Retrieves all documents associated with a case including petitions, " +
    "court orders, notices, affidavits, evidence documents, judgments, contracts " +
    "and other uploaded case documents.")]
        public Task<Result<List<CaseDocumentDto>>> GetDocumentDetailsAsync(Guid documentId, CancellationToken cancellationToken = default)
        {
            PluginValidator.EnsureGuid(
                documentId,
                nameof(documentId));
            var query = new CaseDocumentQuery
            {
                CaseId= documentId,
            };
            return SendAsync(
                query,
                nameof(GetDocumentDetailsAsync),
                cancellationToken);
        }
    }
}
