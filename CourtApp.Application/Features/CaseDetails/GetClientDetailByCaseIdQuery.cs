using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class GetClientDetailByCaseIdQuery : IRequest<Result<GetClientByIdResponse>>
    {
        public Guid CaseId { get; set; }
    }
    public class GetClientDetailByCaseIdQueryHandler : IRequestHandler<GetClientDetailByCaseIdQuery, Result<GetClientByIdResponse>>
    {
        private IUserCaseRepository _userCaseRepository;
        public GetClientDetailByCaseIdQueryHandler(IUserCaseRepository _userCaseRepository)
        {
            this._userCaseRepository = _userCaseRepository;
        }
        public async Task<Result<GetClientByIdResponse>> Handle(GetClientDetailByCaseIdQuery request, CancellationToken cancellationToken)
        {
            var caseDetail = await _userCaseRepository.GetByIdAsync(request.CaseId);
            if (caseDetail != null)
            {
                var clientDetail = caseDetail.Client;
                if (clientDetail != null)
                {
                    GetClientByIdResponse client = new GetClientByIdResponse();
                    client.Name = clientDetail.Name;
                    client.Phone = clientDetail.Phone;
                    client.OfficeEmail = clientDetail.OfficeEmail;
                    client.Mobile = clientDetail.Mobile;
                    client.Address = clientDetail.Address;
                    client.Id = clientDetail.Id;
                    //client.AppearenceID = clientDetail.AppearenceID;
                    //client.OppositCounselId = clientDetail.OppositCounselId != null ? clientDetail.OppositCounselId.Value : Guid.Empty;
                    client.Email = clientDetail.Email;
                    return Result<GetClientByIdResponse>.Success(client);
                }
            }
            return Result<GetClientByIdResponse>.Fail("Information is not exist!");
        }
    }
}
