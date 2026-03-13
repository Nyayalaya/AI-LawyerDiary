using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.Client
{
    public class ClientFeeDto
    {
        public Decimal SettledAmount { get; set; }
        public Decimal AdvanceAmount { get; set; }
    }
}
