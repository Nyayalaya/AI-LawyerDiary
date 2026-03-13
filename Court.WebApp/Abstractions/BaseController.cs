using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using CourtApp.Application.Constants;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Application.Features.CaseNatures.Query;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Application.Features.CourtMasters.Query;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.Queries.Cases;
using CourtApp.Application.Features.Queries.Districts;
using CourtApp.Application.Features.TypeOfCases.Query;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.Litigation.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Abstractions
{
    public abstract class BaseController<T> : Controller
    {
        private IMediator _mediatorInstance;
        private ILogger<T> _loggerInstance;
        private IViewRenderService _viewRenderInstance;
        private IMapper _mapperInstance;
        private INotyfService _notifyInstance;
        protected INotyfService _notify => _notifyInstance ??= HttpContext.RequestServices.GetService<INotyfService>();

        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected IViewRenderService _viewRenderer => _viewRenderInstance ??= HttpContext.RequestServices.GetService<IViewRenderService>();
        protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();
        public async Task<JsonResult> LoadDistricts(string StateCode)
        {
            var districts = await _mediator.Send(new GetDistrictQuery() { StateCode = StateCode });
            var data = Json(districts);
            return data;
        }
        public async Task<JsonResult> LoadCourt(int CourtTypeId)
        {
            var response = await _mediator.Send(new GetCourtMasterAllQuery() {
                CourtTypeId= CourtTypeId
                
            });
            return Json(response);
        }
        public async Task<JsonResult> LoadCourtType()
        {
            var response = await _mediator.Send(new GetCourtTypeQuery());
            return Json(response);
        }

        public async Task<SelectList> LoadCaseNature()
        {
            var response = await _mediator.Send(new CaseNatureByAllCachedQuery());
            var CaseNatures = _mapper.Map<List<CaseNatureViewModel>>(response.Data);
            return new SelectList(CaseNatures, nameof(CaseNatureViewModel.Id), nameof(CaseNatureViewModel.CaseNature), null, null);

        }

        public async Task<SelectList> LoadCaseTypes()
        {
            var response = await _mediator.Send(new CaseKindAllCacheQuery());
            var CaseKind = _mapper.Map<List<CaseKindViewModel>>(response.Data);
            return new SelectList(CaseKind, nameof(CaseKindViewModel.Id), nameof(CaseKindViewModel.CaseKind), null, null);

        }

        public async Task<SelectList> LoadCourtTypes()
        {
            var response = await _mediator.Send(new GetCourtTypeQuery());
            var CaseKind = _mapper.Map<List<CourtTypeViewModel>>(response.Data);
            return new SelectList(CaseKind, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null);

        }

        public async Task<SelectList> DdlLoadDistrict(string StateCode )
        {
            var districts = await _mediator.Send(new GetDistrictQuery() { StateCode = StateCode });
            var districtViewModel = _mapper.Map<List<DistrictViewModel>>(districts.Data);
            return new SelectList(districtViewModel, nameof(DistrictViewModel.DistrictCode), nameof(DistrictViewModel.DistrictName), null, null);

        }

        public async Task<SelectList> DdlCaseStages()
        {
            var stages = await _mediator.Send(new CaseStageCacheAllQuery());
            var caseStagesViewModel = _mapper.Map<List<CaseStageViewModel>>(stages.Data);
            return new SelectList(caseStagesViewModel, nameof(CaseStageViewModel.Id), nameof(CaseStageViewModel.CaseStage), null, null);

        }
        public async Task<SelectList> DdlClientCases()
        {
            var cases = await _mediator.Send(new QueryGetAllCaseEntry() { PageNumber = 1, PageSize = 100 });
            var clientCasesViewModel = _mapper.Map<List<CaseViewModel>>(cases.Data);
            return new SelectList(clientCasesViewModel, nameof(CaseViewModel.Id), nameof(CaseViewModel.Title), null, null);

        }

        public async Task<JsonResult> LoadTypeOfCase(int natureId)
        {
            var caseType = await _mediator.Send(new GetAllTypeOfCasesQuery(1, 100) { CaseNatureId = natureId });
            var data = Json(caseType);
            return data;
        }


        #region Static Dropdown Region

        public SelectList DdlYears()
        {   
            return   new SelectList(StaticDropDownDictionaries.Year(), "Key", "Value");
        }
        public SelectList FirstTtitleList()
        {
            return new SelectList(StaticDropDownDictionaries.FirstTitle(), "Key", "Value");
        }
        public SelectList SecondTtitleList()
        {
            return new SelectList(StaticDropDownDictionaries.SecoundTitle(), "Key", "Value");
        }

        public SelectList DdlCaseStatus()
        {
            return new SelectList(StaticDropDownDictionaries.CaseStatus().OrderBy(v=>v.Value), "Key", "Value");
        }
        #endregion


    }
}