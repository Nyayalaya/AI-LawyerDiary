using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Lawyer
{
    public class GetLawyerByNameQuery : IRequest<Result<List<string>>>
    {
        public string Referal { get; set; }
    }
    public class GetLawyerByNameQueryHandler : IRequestHandler<GetLawyerByNameQuery, Result<List<string>>>
    {
        private readonly ILawyerRepository _repository;
        public GetLawyerByNameQueryHandler(ILawyerRepository lawyer)
        {
            _repository = lawyer;
        }
        public async Task<Result<List<string>>> Handle(GetLawyerByNameQuery request, CancellationToken cancellationToken)
        {
            if (request.Referal != string.Empty)
            {
                var dt = await _repository.Entities.Where(w => w.FirstName.ToLower().Contains(request.Referal.ToLower()))
                    .Select(s => s.FirstName.ToUpper() + " " + s.LastName.ToUpper())
                    .ToListAsync();
                return await Result<List<string>>.SuccessAsync(dt);
            }
            else
                return await Result<List<string>>.FailAsync();
        }
    }
}
