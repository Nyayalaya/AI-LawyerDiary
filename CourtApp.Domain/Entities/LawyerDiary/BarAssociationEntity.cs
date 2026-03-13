using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Entities.LawyerDiary
{
    [Table("m_barassociation", Schema = "ld")]
    public class BarAssociationEntity : AuditableEntity
    {
        
    }
}