using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Entities.Common
{
    [Table("AuditTrail",Schema ="Common")]
    public class AuditEntity
    {
        public Guid Id { get; set; }                    /*Log id*/
        public DateTime AuditDateTimeUtc { get; set; }  /*Log time*/
        public string AuditType { get; set; }           /*Create, Update or Delete*/
        public string UserId { get; set; }           /*Log User*/
        public string TableName { get; set; }           /*Table where rows been created/updated/deleted*/
        public string PrimaryKey { get; set; }           /*Table Pk and it's values*/
        public string OldValues { get; set; }           /*Changed column name and old value*/
        public string NewValues { get; set; }           /*Changed column name and current value*/
        public string ChangedColumns { get; set; }      /*Changed column names*/
    }
}
