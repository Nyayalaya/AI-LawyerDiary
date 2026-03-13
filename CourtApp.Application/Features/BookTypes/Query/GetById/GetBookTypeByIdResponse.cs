using System;

namespace CourtApp.Application.Features.BookTypes.Query.GetById
{
    public class GetBookTypeByIdResponse
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
    }
}