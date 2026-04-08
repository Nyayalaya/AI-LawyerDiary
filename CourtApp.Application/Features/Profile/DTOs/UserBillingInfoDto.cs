using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.DTOs
{
    public class UserBillingInfoDto
    {
        public string UserId { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string Branch { get; set; }
        public string Pan { get; set; }
        public string GstNo { get; set; }
    }
}
