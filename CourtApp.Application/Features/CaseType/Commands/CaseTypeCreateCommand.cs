using CourtApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Typeofcasess.Commands
{
    public class CaseTypeCreateCommand : IRequest<Result<string>>
    {
        public Guid NatureId { get; set; }
        public Guid CourtTypeId { get; set; }
        public int StateId { get; set; }
        public List<TypeOfCase> CaseTypes { get; set; }

    }
    public class TypeOfCase
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
}
