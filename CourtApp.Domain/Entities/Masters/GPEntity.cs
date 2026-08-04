using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_gp", Schema = "common")]    
    public class GPEntity
    {
        
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int BlockId { get; set; }
        public virtual BlockEntity Block { get; set; }
        public ICollection<VillageEntity> Villages { get; set; }
    }
}
