using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Masters
{
    [Table("m_ward")]   
    public class WardEntity
    {
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int CityId { get; set; }
        public virtual CityEntity city { get; set; }
    }
}
