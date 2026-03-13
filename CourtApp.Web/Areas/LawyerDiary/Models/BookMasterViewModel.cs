using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class BookMasterViewModel
    {
        public int Id { get; set; }        
        public int Year { get; set; }        
        public string PublisherName { get; set; }
        public string BookName { get; set; }
        public string BookType { get; set; }
        public int BookTypeId { get; set; }
        public SelectList BookTypes { get; set; }
        public int PublisherId { get; set; }
        public SelectList Publishers { get; set; }
    }
}
