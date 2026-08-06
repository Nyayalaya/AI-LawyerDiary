using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.FormManagement.Commands;
using CourtApp.Application.Features.FormManagement.DTOs;
using CourtApp.Application.Features.FormManagement.Queries;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormManagement.Handlers
{
    public class FormTypeQueryHandler : IRequestHandler<GetAllFormTypesQuery, PaginatedResult<FormTypeResponseDto>>,
                                        IRequestHandler<GetFormTypeByIdQuery, FormTypeResponseDto>,
                                        IRequestHandler<GetFormTypeByCodeQuery, FormTypeResponseDto>
    {
        private readonly IFormTypeRepository _repository;
        private readonly IMapper _mapper;

        public FormTypeQueryHandler(IFormTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<FormTypeResponseDto>> Handle(GetAllFormTypesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetListAsync();
            var result = _mapper.Map<List<FormTypeResponseDto>>(entities);
            return result.ToPaginatedResult(request.PageNumber, request.PageSize);
        }

        public async Task<FormTypeResponseDto> Handle(GetFormTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<FormTypeResponseDto>(entity);
        }

        public async Task<FormTypeResponseDto> Handle(GetFormTypeByCodeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByCodeAsync(request.Code);
            return _mapper.Map<FormTypeResponseDto>(entity);
        }
    }

    public class FormTypeCommandHandler : IRequestHandler<CreateFormTypeCommand, Guid>,
                                           IRequestHandler<UpdateFormTypeCommand, Guid>,
                                           IRequestHandler<DeleteFormTypeCommand, bool>
    {
        private readonly IFormTypeRepository _repository;
        private readonly IMapper _mapper;

        public FormTypeCommandHandler(IFormTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFormTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new CourtApp.Domain.Entities.Masters.FormTypeEntity
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description
            };
            return await _repository.InsertAsync(entity);
        }

        public async Task<Guid> Handle(UpdateFormTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new CourtApp.Domain.Entities.Masters.FormTypeEntity
            {
                Id = request.Id,
                Code = request.Code,
                Name = request.Name,
                Description = request.Description
            };
            await _repository.UpdateAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Handle(DeleteFormTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity);
            return true;
        }
    }
}
