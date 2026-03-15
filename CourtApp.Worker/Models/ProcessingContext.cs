using CourtApp.Domain.Enums;

namespace CourtApp.Worker.Models
{
    public class ProcessingContext
    {
        public Guid DocumentId { get; set; }

        public string FilePath { get; set; }
        public DocumentType DocumentType { get; set; }
        public Stream stream { get; set; }
    }
}
