using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Queries.Lawyer
{
    public class ResponseGetAllLawyer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
}
