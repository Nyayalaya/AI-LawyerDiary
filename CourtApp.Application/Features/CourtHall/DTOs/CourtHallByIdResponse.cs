using CourtApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtHall.DTOs
{
    public class CourtHallByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string JudgeName { get; set; }
        public string RoomNumber { get; set; }
        public Guid CourtComplexId { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}
