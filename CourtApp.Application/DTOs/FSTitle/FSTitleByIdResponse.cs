using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.FSTitle
{
    public class FSTitleByIdResponse
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
