using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.FormBuilder
{
    public class FormBuilderResponseByIdDto
    {
        public Guid Id { get; set; }
        public string FormName { get; set; }
        public List<FieldDetailsDto> FieldDetails { get; set; }
    }
}
