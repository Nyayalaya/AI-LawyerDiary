using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_cadre", Schema = "ld")]
    public class CadreMasterEntity : AuditableEntity
    {
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
