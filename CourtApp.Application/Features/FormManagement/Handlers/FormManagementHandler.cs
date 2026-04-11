using AutoMapper;
using CourtApp.Application.Features.FormManagement.Commands;
using CourtApp.Application.Features.FormManagement.DTOs;
using CourtApp.Application.Features.FormManagement.Interfaces;
using CourtApp.Application.Features.FormManagement.Queries;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormManagement.Handlers
{
    #region FormMaster Handlers

    public class FormMasterQueryHandler : IRequestHandler<GetAllFormMastersQuery, List<FormMasterResponseDto>>,
                                          IRequestHandler<GetFormMasterByIdQuery, FormMasterResponseDto>,
                                          IRequestHandler<GetFormMasterByCodeQuery, FormMasterResponseDto>,
                                          IRequestHandler<GetFormMastersByTypeQuery, List<FormMasterResponseDto>>
    {
        private readonly IFormMasterRepository _repository;
        private readonly IMapper _mapper;

        public FormMasterQueryHandler(IFormMasterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormMasterResponseDto>> Handle(GetAllFormMastersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetListAsync();
            return _mapper.Map<List<FormMasterResponseDto>>(entities);
        }

        public async Task<FormMasterResponseDto> Handle(GetFormMasterByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<FormMasterResponseDto>(entity);
        }

        public async Task<FormMasterResponseDto> Handle(GetFormMasterByCodeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByCodeAsync(request.Code);
            return _mapper.Map<FormMasterResponseDto>(entity);
        }

        public async Task<List<FormMasterResponseDto>> Handle(GetFormMastersByTypeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetByFormTypeAsync(request.FormTypeId);
            return _mapper.Map<List<FormMasterResponseDto>>(entities);
        }
    }

    public class FormMasterCommandHandler : IRequestHandler<CreateFormMasterCommand, Guid>,
                                            IRequestHandler<UpdateFormMasterCommand, Guid>,
                                            IRequestHandler<DeleteFormMasterCommand, bool>
    {
        private readonly IFormMasterRepository _repository;
        private readonly IMapper _mapper;

        public FormMasterCommandHandler(IFormMasterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFormMasterCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormMasterEntity>(request);
            return await _repository.InsertAsync(entity);
        }

        public async Task<Guid> Handle(UpdateFormMasterCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormMasterEntity>(request);
            await _repository.UpdateAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Handle(DeleteFormMasterCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return false;
            await _repository.DeleteAsync(entity);
            return true;
        }
    }

    #endregion

    #region FormSubtype Handlers

    public class FormSubtypeQueryHandler : IRequestHandler<GetAllFormSubtypesQuery, List<FormSubtypeResponseDto>>,
                                           IRequestHandler<GetFormSubtypeByIdQuery, FormSubtypeResponseDto>,
                                           IRequestHandler<GetFormSubtypeByCodeQuery, FormSubtypeResponseDto>,
                                           IRequestHandler<GetFormSubtypesByFormQuery, List<FormSubtypeResponseDto>>
    {
        private readonly IFormSubtypeRepository _repository;
        private readonly IMapper _mapper;

        public FormSubtypeQueryHandler(IFormSubtypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormSubtypeResponseDto>> Handle(GetAllFormSubtypesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetListAsync();
            return _mapper.Map<List<FormSubtypeResponseDto>>(entities);
        }

        public async Task<FormSubtypeResponseDto> Handle(GetFormSubtypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<FormSubtypeResponseDto>(entity);
        }

        public async Task<FormSubtypeResponseDto> Handle(GetFormSubtypeByCodeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByCodeAsync(request.Code);
            return _mapper.Map<FormSubtypeResponseDto>(entity);
        }

        public async Task<List<FormSubtypeResponseDto>> Handle(GetFormSubtypesByFormQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetByFormAsync(request.FormId);
            return _mapper.Map<List<FormSubtypeResponseDto>>(entities);
        }
    }

    public class FormSubtypeCommandHandler : IRequestHandler<CreateFormSubtypeCommand, Guid>,
                                             IRequestHandler<UpdateFormSubtypeCommand, Guid>,
                                             IRequestHandler<DeleteFormSubtypeCommand, bool>
    {
        private readonly IFormSubtypeRepository _repository;
        private readonly IMapper _mapper;

        public FormSubtypeCommandHandler(IFormSubtypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFormSubtypeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormSubtypeEntity>(request);
            return await _repository.InsertAsync(entity);
        }

        public async Task<Guid> Handle(UpdateFormSubtypeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormSubtypeEntity>(request);
            await _repository.UpdateAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Handle(DeleteFormSubtypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return false;
            await _repository.DeleteAsync(entity);
            return true;
        }
    }

    #endregion

    #region FormTemplate Handlers

    public class FormTemplateQueryHandler : IRequestHandler<GetAllFormTemplatesQuery, List<FormTemplateResponseDto>>,
                                            IRequestHandler<GetFormTemplateByIdQuery, FormTemplateResponseDto>,
                                            IRequestHandler<GetFormTemplatesBySubtypeQuery, List<FormTemplateResponseDto>>
    {
        private readonly IFormTemplateRepository _repository;
        private readonly IMapper _mapper;

        public FormTemplateQueryHandler(IFormTemplateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormTemplateResponseDto>> Handle(GetAllFormTemplatesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetListAsync();
            return _mapper.Map<List<FormTemplateResponseDto>>(entities);
        }

        public async Task<FormTemplateResponseDto> Handle(GetFormTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<FormTemplateResponseDto>(entity);
        }

        public async Task<List<FormTemplateResponseDto>> Handle(GetFormTemplatesBySubtypeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetBySubtypeAsync(request.FormSubtypeId);
            return _mapper.Map<List<FormTemplateResponseDto>>(entities);
        }
    }

    public class FormTemplateCommandHandler : IRequestHandler<CreateFormTemplateCommand, Guid>,
                                              IRequestHandler<UpdateFormTemplateCommand, Guid>,
                                              IRequestHandler<DeleteFormTemplateCommand, bool>
    {
        private readonly IFormTemplateRepository _repository;
        private readonly IMapper _mapper;

        public FormTemplateCommandHandler(IFormTemplateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFormTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormTemplateEntity>(request);
            return await _repository.InsertAsync(entity);
        }

        public async Task<Guid> Handle(UpdateFormTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormTemplateEntity>(request);
            await _repository.UpdateAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Handle(DeleteFormTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return false;
            await _repository.DeleteAsync(entity);
            return true;
        }
    }

    #endregion

    #region FormTemplateVersion Handlers

    public class FormTemplateVersionQueryHandler : IRequestHandler<GetAllFormTemplateVersionsQuery, List<FormTemplateVersionResponseDto>>,
                                                   IRequestHandler<GetFormTemplateVersionByIdQuery, FormTemplateVersionResponseDto>,
                                                   IRequestHandler<GetFormTemplateVersionsQuery, List<FormTemplateVersionResponseDto>>
    {
        private readonly IFormTemplateVersionRepository _repository;
        private readonly IMapper _mapper;

        public FormTemplateVersionQueryHandler(IFormTemplateVersionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormTemplateVersionResponseDto>> Handle(GetAllFormTemplateVersionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetListAsync();
            return _mapper.Map<List<FormTemplateVersionResponseDto>>(entities);
        }

        public async Task<FormTemplateVersionResponseDto> Handle(GetFormTemplateVersionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<FormTemplateVersionResponseDto>(entity);
        }

        public async Task<List<FormTemplateVersionResponseDto>> Handle(GetFormTemplateVersionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetByTemplateAsync(request.FormTemplateId);
            return _mapper.Map<List<FormTemplateVersionResponseDto>>(entities);
        }
    }

    public class FormTemplateVersionCommandHandler : IRequestHandler<CreateFormTemplateVersionCommand, Guid>,
                                                     IRequestHandler<UpdateFormTemplateVersionCommand, Guid>,
                                                     IRequestHandler<DeleteFormTemplateVersionCommand, bool>
    {
        private readonly IFormTemplateVersionRepository _repository;
        private readonly IMapper _mapper;

        public FormTemplateVersionCommandHandler(IFormTemplateVersionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFormTemplateVersionCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormTemplateVersionEntity>(request);
            return await _repository.InsertAsync(entity);
        }

        public async Task<Guid> Handle(UpdateFormTemplateVersionCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormTemplateVersionEntity>(request);
            await _repository.UpdateAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Handle(DeleteFormTemplateVersionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return false;
            await _repository.DeleteAsync(entity);
            return true;
        }
    }

    #endregion

    #region FormCaseCategoryMapping Handlers

    public class FormCaseCategoryMappingQueryHandler : IRequestHandler<GetAllFormCaseCategoryMappingsQuery, List<FormCaseCategoryMappingResponseDto>>,
                                                       IRequestHandler<GetFormCaseCategoryMappingByIdQuery, FormCaseCategoryMappingResponseDto>,
                                                       IRequestHandler<GetFormCaseCategoryMappingsBySubtypeQuery, List<FormCaseCategoryMappingResponseDto>>
    {
        private readonly IFormCaseCategoryMappingRepository _repository;
        private readonly IMapper _mapper;

        public FormCaseCategoryMappingQueryHandler(IFormCaseCategoryMappingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormCaseCategoryMappingResponseDto>> Handle(GetAllFormCaseCategoryMappingsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetListAsync();
            return _mapper.Map<List<FormCaseCategoryMappingResponseDto>>(entities);
        }

        public async Task<FormCaseCategoryMappingResponseDto> Handle(GetFormCaseCategoryMappingByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<FormCaseCategoryMappingResponseDto>(entity);
        }

        public async Task<List<FormCaseCategoryMappingResponseDto>> Handle(GetFormCaseCategoryMappingsBySubtypeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetByFormSubtypeAsync(request.FormSubtypeId);
            return _mapper.Map<List<FormCaseCategoryMappingResponseDto>>(entities);
        }
    }

    public class FormCaseCategoryMappingCommandHandler : IRequestHandler<CreateFormCaseCategoryMappingCommand, Guid>,
                                                        IRequestHandler<UpdateFormCaseCategoryMappingCommand, Guid>,
                                                        IRequestHandler<DeleteFormCaseCategoryMappingCommand, bool>
    {
        private readonly IFormCaseCategoryMappingRepository _repository;
        private readonly IMapper _mapper;

        public FormCaseCategoryMappingCommandHandler(IFormCaseCategoryMappingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFormCaseCategoryMappingCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormCaseCategoryMapping>(request);
            return await _repository.InsertAsync(entity);
        }

        public async Task<Guid> Handle(UpdateFormCaseCategoryMappingCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormCaseCategoryMapping>(request);
            await _repository.UpdateAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Handle(DeleteFormCaseCategoryMappingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return false;
            await _repository.DeleteAsync(entity);
            return true;
        }
    }

    #endregion

    #region FormCourtMapping Handlers

    public class FormCourtMappingQueryHandler : IRequestHandler<GetAllFormCourtMappingsQuery, List<FormCourtMappingResponseDto>>,
                                                IRequestHandler<GetFormCourtMappingByIdQuery, FormCourtMappingResponseDto>,
                                                IRequestHandler<GetFormCourtMappingsBySubtypeQuery, List<FormCourtMappingResponseDto>>
    {
        private readonly IFormCourtMappingRepository _repository;
        private readonly IMapper _mapper;

        public FormCourtMappingQueryHandler(IFormCourtMappingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormCourtMappingResponseDto>> Handle(GetAllFormCourtMappingsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetListAsync();
            return _mapper.Map<List<FormCourtMappingResponseDto>>(entities);
        }

        public async Task<FormCourtMappingResponseDto> Handle(GetFormCourtMappingByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<FormCourtMappingResponseDto>(entity);
        }

        public async Task<List<FormCourtMappingResponseDto>> Handle(GetFormCourtMappingsBySubtypeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetByFormSubtypeAsync(request.FormSubtypeId);
            return _mapper.Map<List<FormCourtMappingResponseDto>>(entities);
        }
    }

    public class FormCourtMappingCommandHandler : IRequestHandler<CreateFormCourtMappingCommand, Guid>,
                                                  IRequestHandler<UpdateFormCourtMappingCommand, Guid>,
                                                  IRequestHandler<DeleteFormCourtMappingCommand, bool>
    {
        private readonly IFormCourtMappingRepository _repository;
        private readonly IMapper _mapper;

        public FormCourtMappingCommandHandler(IFormCourtMappingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFormCourtMappingCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormCourtMapping>(request);
            return await _repository.InsertAsync(entity);
        }

        public async Task<Guid> Handle(UpdateFormCourtMappingCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FormCourtMapping>(request);
            await _repository.UpdateAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Handle(DeleteFormCourtMappingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return false;
            await _repository.DeleteAsync(entity);
            return true;
        }
    }

    #endregion
}
