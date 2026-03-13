using CourtApp.Application.Common;
using CourtApp.Application.DTOs.FormBuilder;
using CourtApp.Application.Interfaces.CacheRepositories.FormBuilder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormBuilder
{
    public class GetTemplateInfoCachedByIdQuery : IRequest<Result<GetTemplateInfoByIdDto>>
    {
        public Guid Id { get; set; }
    }
    public class GetTemplateInfoCachedByIdQueryHandler : IRequestHandler<GetTemplateInfoCachedByIdQuery, Result<GetTemplateInfoByIdDto>>
    {
        private readonly ITemplateInfoCacheRepository _cacheRepository;
        public GetTemplateInfoCachedByIdQueryHandler(ITemplateInfoCacheRepository _cacheRepository)
        {
            this._cacheRepository = _cacheRepository;
        }
        public async Task<Result<GetTemplateInfoByIdDto>> Handle(GetTemplateInfoCachedByIdQuery request, CancellationToken cancellationToken)
        {
            var details = await _cacheRepository.GetByIdAsync(request.Id);
            if (details != null)
            {
                List<Tags> Tg = new List<Tags>();
                var dt = new GetTemplateInfoByIdDto();
                dt.TemplatePath = details.TemplatePath;
                dt.TemplateName = details.TemplateName;
                foreach (var t in details.Tags)
                    Tg.Add(new Tags() { Tag = t.Tag });
                dt.Tags = Tg;
                return Result<GetTemplateInfoByIdDto>.Success(dt);
            }
            return null;
        }
    }
}
