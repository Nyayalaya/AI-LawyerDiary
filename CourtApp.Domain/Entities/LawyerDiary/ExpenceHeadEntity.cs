using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_expense_head", Schema = "accounts")]
    public class ExpenseHeadEntity : AuditableEntity
    {
        public required string HeadName { get; set; }
    }
}