using System;

namespace CourtApp.Application.DTOs.DOTypes
{
    public class DOTypeByIdResponse
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Name_En { get; set; }
    }
}
