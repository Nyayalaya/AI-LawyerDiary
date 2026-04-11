using AuditTrail.Abstrations;
using CourtApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("client", Schema = "ld")]
    [Index(nameof(Email), IsUnique = false)]
    [Index(nameof(Mobile), IsUnique = false)]
    public class ClientEntity : AuditableEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }

        [MaxLength(150)]
        public string OfficeEmail { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string ReferalBy { get; set; }

        [MaxLength(100)]
        public string RegNo { get; set; }

        [MaxLength(200)]
        public string Proprietor { get; set; }
        
        public ClientType ClientType { get; set; }

        
        [MaxLength(20)]
        public string PAN { get; set; }

        [MaxLength(20)]
        public string GST { get; set; }
    }
}