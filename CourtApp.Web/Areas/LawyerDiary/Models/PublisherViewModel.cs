using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class PublisherViewModel
    {
        public Guid Id { get; set; }
        public string PublicationName { get; set; }
        public string PropriatorName { get; set; }
    }
}
