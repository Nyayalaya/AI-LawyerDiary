using CourtApp.Application.Features.CaseDocuments.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDocuments.Services
{
    public interface IPdfTextExtractorService
    {
        Task<List<string>> ExtractAsync(Stream stream);
    }
}
