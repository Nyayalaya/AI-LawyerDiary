
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.CourtType.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Handlers
{
    public class GetCourtTypeByIdQueryHandler : IRequestHandler<GetCourtTypeByIdQuery, Result<GetCourtTypeResponse>>
    {
        private readonly ICourtTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCourtTypeByIdQueryHandler> _logger;

        public GetCourtTypeByIdQueryHandler(
            ICourtTypeRepository repository,
            IMapper mapper,
            ILogger<GetCourtTypeByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<GetCourtTypeResponse>> Handle(GetCourtTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courtType = await _repository.CourtTypeEntities
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (courtType == null)
                {
                    _logger.LogWarning($"Court type not found with ID: {request.Id}");
                    return Result<GetCourtTypeResponse>.Fail("Court type not found.");
                }

                var dto = _mapper.Map<GetCourtTypeResponse>(courtType);
                return Result<GetCourtTypeResponse>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCourtTypeByIdQueryHandler: {ex.Message}");
                return Result<GetCourtTypeResponse>.Fail(ex.Message);
            }
        }
    }
}       