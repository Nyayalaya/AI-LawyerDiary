using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Entities.LawyerDiary
{
    [Table("m_barassociation", Schema = "cases")]
    public class BarAssociationEntity : AuditableEntity
    {
        
    }
}