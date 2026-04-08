
using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("m_user_specialization", Schema = "Identity")]
    public class UserSpecialization:AuditableEntity
    {   
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
