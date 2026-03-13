using CourtApp.Application.Features.CourtDistrict;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class CourtDistrictController : BaseController<CourtDistrictController>
    {
        public IActionResult Index()
        {
            var model = new CourtDistrictViewModel();
            return View(model);
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetCourtDistrictQuery() { PageNumber = 1, PageSize = 100 });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CourtDistrictViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }


        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new CourtDistrictViewModel();
                ViewModel.States = await LoadStates();
                ViewModel.CourtDistricts = new List<CourtDistrict> { new CourtDistrict() };
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetCourtDistrictQueryById() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<CourtDistrictViewModel>(response.Data);
                    ViewModel.States = await LoadStates();
                    //ViewModel.Districts = await DdlLoadDistrict(ViewModel.StateId);                    
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Edit", ViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, CourtDistrictViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<CreateCourtDistrictCommand>(btViewModel);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Court District with ID {result.Data} Created.");
                    }
                    else
                    {
                        _notify.Error(result.Message);
                        var html = await _viewRenderer.RenderViewToStringAsync("_Create", btViewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateCourtDistrictCommand>(btViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded) _notify.Information($"Court District with ID {result.Data} Updated.");
                    else
                    {
                        _notify.Error(result.Message);
                        var html = await _viewRenderer.RenderViewToStringAsync("_Create", btViewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                var response = await _mediator.Send(new GetCourtDistrictQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CourtDistrictViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteCourtDistrictCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Court district with Id {id} Deleted.");
                var response = await _mediator.Send(new GetCourtDistrictQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CourtDistrictViewModel>>(response.Data);
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
