using System;
using System.IO;

namespace CourtApp.Application.Features.CaseDocuments.Dtos
{
    public class DocumentStreamResult
    {
        public Stream Stream { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
