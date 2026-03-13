using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using CourtApp.Domain.Entities.FormBuilder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.FormBuilder
{
    public class CreateTemplateFormMappingCommand : IRequest<Result<Guid>>
    {
        public Guid TemplateId { get; set; }
        public Guid FormId { get; set; }
        public List<MappingDto> FieldsMapping { get; set; }
    }
    public class MappingDto
    {
        public string Tag { get; set; }
        public Guid Key { get; set; }
    }

    public class CreateTemplateFormMappingCommandHandler : IRequestHandler<CreateTemplateFormMappingCommand, Result<Guid>>
    {
        private readonly IFormTempMappingRepository repository;
        private IUnitOfWork _UoW { get; set; }
        private readonly IMapper _mapper;
        public CreateTemplateFormMappingCommandHandler(IFormTempMappingRepository repository, IUnitOfWork _UoW, IMapper _mapper)
        {
            this.repository = repository;
            this._mapper = _mapper;
            this._UoW = _UoW;
        }
        public async Task<Result<Guid>> Handle(CreateTemplateFormMappingCommand request, CancellationToken cancellationToken)
        {
            var FormEntity = repository.Entities
                     .Where(w => w.TemplateId==request.TemplateId);

            if (FormEntity.Any())
                return Result<Guid>.Fail("The selected template is already mapped.");
            else
            {
                var _map = _mapper.Map<FormTemplateMappingEntity>(request);
                await repository.InsertAsync(_map);
                await _UoW.Commit(cancellationToken);
                return Result<Guid>.Success(_map.Id);
            }
        }
    }
}
