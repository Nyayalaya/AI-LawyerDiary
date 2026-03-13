using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using CourtApp.Domain.Entities.Drafting;
using CourtApp.Domain.Entities.FormBuilder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormBuilder
{
    public class UpdateFormBuilderCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string FormName { get; set; }
        public FormFieldsDto Form { get; set; }
    }
    public class UpdateFormBuilderCommandHandler : IRequestHandler<UpdateFormBuilderCommand, Result<Guid>>
    {
        private readonly IFormBuilderRepository repository;
        private IUnitOfWork _UoW { get; set; }
        private readonly IMapper _mapper;

        public UpdateFormBuilderCommandHandler(IFormBuilderRepository repository, IUnitOfWork _UoW, IMapper _mapper)
        {
            this.repository = repository;
            this._mapper = _mapper;
            this._UoW = _UoW;
        }
        public async Task<Result<Guid>> Handle(UpdateFormBuilderCommand request, CancellationToken cancellationToken)
        {
            if (request.FormName != null)
            {
                var detail = await repository.GetByIdAsync(request.Id);
                if (detail == null)
                    return Result<Guid>.Fail($"Selected Record is not exists!");
                else
                {
                    detail.FormName = request.FormName;
                    FormFieldsEntity fm = new FormFieldsEntity();
                    fm.Fields = _mapper.Map<List<FieldDetailsEntity>>(request.Form.Fields);
                    detail.FieldsDetails = fm;
                    await repository.UpdateAsync(detail);
                    await _UoW.Commit(cancellationToken);
                    return Result<Guid>.Success(detail.Id);
                }
            }
            return null;
        }
    }
}
