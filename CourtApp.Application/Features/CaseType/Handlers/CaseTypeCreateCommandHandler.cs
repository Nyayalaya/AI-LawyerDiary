using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseCategory.Services;
using CourtApp.Application.Features.CaseType.Services;
using CourtApp.Application.Features.Typeofcasess.Commands;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.Common;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseType.Handlers
{
    public class CaseTypeCreateCommandHandler: IRequestHandler<CaseTypeCreateCommand, Result<string>>
    {
        private readonly ICaseTypeRepository repository;
        private readonly IMapper mapper;
        private readonly ICaseCategoryRepository caseNatureRepository;

        private IUnitOfWork _unitOfWork { get; set; }
        private readonly IMultiLangWordRepository _multiRepo;

        public CaseTypeCreateCommandHandler(ICaseTypeRepository repository,
            IMapper mapper, IUnitOfWork _unitOfWork, ICaseCategoryRepository caseNatureRepository,
            IMultiLangWordRepository _multiRepo)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
            this.caseNatureRepository = caseNatureRepository;
            this._multiRepo = _multiRepo;
        }
        public async Task<Result<string>> Handle(CaseTypeCreateCommand request, CancellationToken cancellationToken)
        {

            if (request.CaseTypes == null || !request.CaseTypes.Any())
                return Result<string>.Fail("Case type is not supplied!");

            Guid lastInsertedId = Guid.Empty;

            foreach (var c in request.CaseTypes)
            {
                bool isDuplicate = repository.QryEntities.Any(w =>
                    w.Name_En.ToLower() == c.Name_En.ToLower().Trim() &&
                    w.CourtTypeId == request.CourtTypeId &&
                    w.NatureId == request.NatureId
                );

                if (isDuplicate)
                    return Result<string>.Fail($"The name '{c.Name_En}' already exists for the given Court Type and Nature.");

                var entity = new TypeOfCasesEntity
                {
                    Name_En = c.Name_En.Trim(),
                    Name_Hn = c.Name_Hn?.Trim(),
                    CourtTypeId = request.CourtTypeId,
                    NatureId = request.NatureId,
                    Abbreviation = c.Abbreviation.Trim()
                };

                await repository.InsertAsync(entity);
                lastInsertedId = entity.Id;
            }

            // Commit once after loop for better performance
            await _unitOfWork.Commit(cancellationToken);

            var keywords = request.CaseTypes
        .SelectMany(c => c.Name_En.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        .Select(w => w.Trim())
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .Select(w => new MultiLangDictEntity
        {
            KeyWord = w
        })
        .ToList();

            if (keywords.Any())
            {
                await _multiRepo.BulkInsertAsync(keywords);
                await _unitOfWork.Commit(cancellationToken);
            }
            return Result<string>.Success("Record inserted successfully!");

        }
    }
}
}
