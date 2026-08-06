using System.ComponentModel.DataAnnotations.Schema;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_habitation", Schema = "common")]   
    public class HabitationEntity
    {
       
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public virtual VillageEntity village { get; set; }
    }
}
