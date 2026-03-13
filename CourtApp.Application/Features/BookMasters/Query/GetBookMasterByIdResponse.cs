using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookMasters.Query
{
    public class GetBookMasterByIdResponse
    {
        public Guid Id { get; set; }
        public Guid BookTypeId { get; set; }
        public Guid PublisherId { get; set; }
        public string BookName { get; set; }
        public int Year { get; set; }
    }
}
