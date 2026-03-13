using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Client.Model
{
    public class AdvalremFeeViewModel
    {
        public SelectList States { get; set; }
        public string StateCode { get; set; }
        public SelectList FeeKind { get; set; }
        public string FeeKindCode { get; set; }
        public Double Amount { get; set; }
    }
}
