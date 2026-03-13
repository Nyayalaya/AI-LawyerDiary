using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Features.CourtMasters;
using CourtApp.Application.Features.CourtMasters.Command;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.Queries.States;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class CourtMasterController : BaseController<CourtMasterController>
    {
        public IActionResult Index()
        {
            var model = new CourtMasterViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCourtMasterAllQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CourtMasterViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        private async Task BindDropdownAsync(CourtMasterViewModel ViewModel)
        {

            var courtTypeList = await _mediator.Send(new GetCourtTypeQuery());
            if (courtTypeList.Succeeded)
            {
                var DdlCourtTypes = _mapper.Map<List<CourtTypeViewModel>>(courtTypeList.Data);
                ViewModel.CourtTypes = new SelectList(DdlCourtTypes, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null); ;
            }
            var statelist = await _mediator.Send(new GetStateMasterQuery());
            if (statelist.Succeeded)
            {
                var DdlStates = _mapper.Map<List<StateViewModel>>(statelist.Data);
                ViewModel.States = new SelectList(DdlStates, nameof(StateViewModel.Id), nameof(StateViewModel.Name_En), null, null);

            }
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new CourtMasterViewModel();
                ViewModel.CourtTypes = await LoadCourtTypes();
                ViewModel.States = await LoadStates();
                ViewModel.CourtBenches = new List<CourtBench> { new CourtBench() };
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetCourtMasterDataByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<CourtMasterViewModel>(response.Data);
                    ViewModel.States = await LoadStates();
                    ViewModel.CourtTypes = await LoadCourtTypes();
                    //ViewModel.Districts = await DdlLoadDistrict(ViewModel.StateCode);
                    if (ViewModel.IsHighCourt != true)
                    {
                        ViewModel.CourtDistricts = await DdlLoadCourtDistricts(ViewModel.StateCode);
                        ViewModel.CourtComplexes = await GetCourtComplex(ViewModel.CourtDistrictId.Value);
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                return null;
            }
        }

        public async Task<IActionResult> CreateOrUpdate(Guid id)
        {

            if (id == Guid.Empty)
            {
                var ViewModel = new CourtMasterViewModel();
                await BindDropdownAsync(ViewModel);
                return View("_CreateOrEdit", ViewModel);
            }
            else
            {
                var response = await _mediator.Send(new GetCourtMasterDataByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<CourtMasterViewModel>(response.Data);
                    await BindDropdownAsync(ViewModel);
                    return View("_CreateOrEdit", ViewModel);
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, CourtMasterViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    try
                    {
                        var createBookTypeCommand = _mapper.Map<CreateCourtMasterCommand>(btViewModel);
                        var result = await _mediator.Send(createBookTypeCommand);
                        if (result.Succeeded)
                            _notify.Success($"Case type with ID {result.Data} Created.");
                        else
                        {
                            _notify.Error(result.Message);
                            var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", btViewModel);
                            return new JsonResult(new { isValid = false, html = html });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    ;
                }
                else
                {
                    var court = _mapper.Map<UpdateCourtMasterCommand>(btViewModel);
                    court.CBAddress = _mapper.Map<List<CourtBenchResponse>>(btViewModel.CourtBenches);
                    var result = await _mediator.Send(court);
                    if (result.Succeeded) _notify.Information($"Case kind with unique id {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetCourtMasterAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CourtMasterViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", btViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteCourtMasterCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Case Nature with Id {id} Deleted.");
                var response = await _mediator.Send(new GetCourtMasterAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CourtMasterViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }
    }
}
