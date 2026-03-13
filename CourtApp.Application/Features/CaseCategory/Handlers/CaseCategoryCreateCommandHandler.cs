
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseCategory.Commands;
using CourtApp.Application.Features.CaseCategory.Services;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.Common;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Handlers
{
    public class CaseCategoryCreateCommandHandler : IRequestHandler<CaseCategoryCreateCommand, Result<string>>
    {
        private readonly ICaseCategoryRepository repository;
        private readonly IMapper mapper;
        private readonly IMultiLangWordRepository _multiRepo;
        private IUnitOfWork _unitOfWork { get; set; }
        public CaseCategoryCreateCommandHandler(ICaseCategoryRepository repository, IMapper mapper,
            IUnitOfWork _unitOfWork, IMultiLangWordRepository _multiRepo)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
            this._multiRepo = _multiRepo;
        }
        public async Task<Result<string>> Handle(CaseCategoryCreateCommand request, CancellationToken cancellationToken)
        {
            var isCatgegoryExist = await repository.IsCaseCategoryExistAsync(request.CourtTypeId,request.Name_En);
            if (isCatgegoryExist) return Result<string>.Fail("Case category with the same name already exists for the given court type.");

            var entity = mapper.Map<NatureEntity>(request);
            await repository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            var keywords = request.Name_En
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new MultiLangDictEntity
                {
                    KeyWord = s
                })
                .ToList();

            if (keywords.Count > 0)
            {
                await _multiRepo.BulkInsertAsync(keywords);
                await _unitOfWork.Commit(cancellationToken);
            }
            return Result<string>.Success("Category is created successfully!");
        }
    }
}
