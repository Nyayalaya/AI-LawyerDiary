using CourtApp.Application.Features.CaseCategory;
using CourtApp.Application.Features.CaseNatures.Command;
using CourtApp.Application.Features.CourtType.Query;
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
    public class CaseNatureController : BaseController<CaseNatureController>
    {
        public IActionResult Index()
        {
            var model = new CaseNatureViewModel();

            return View(model);
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetQueryCaseCategory());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CaseNatureViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        private async Task BindDropdownAsync(CaseNatureViewModel ViewModel)
        {
            var courtTypeList = await _mediator.Send(new GetCourtTypeQuery());
            if (courtTypeList.Succeeded)
            {
                var DdlCourtTypes = _mapper.Map<List<CourtTypeViewModel>>(courtTypeList.Data);
                ViewModel.CourtTypes = new SelectList(DdlCourtTypes, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null); ;
            }
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new CaseNatureViewModel();
                await BindDropdownAsync(ViewModel);
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetQueryByIdCaseCategory() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<CaseNatureViewModel>(response.Data);
                    await BindDropdownAsync(ViewModel);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, CaseNatureViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createBookTypeCommand = _mapper.Map<CreateCaseNatureCommand>(btViewModel);
                    var result = await _mediator.Send(createBookTypeCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Case Category with ID {result.Data} Created.");
                    }
                    else
                    {
                        _notify.Error(result.Message);
                        return await RenderForm(btViewModel, false, "_CreateOrEdit");
                    }
                }
                else
                {
                    var updateBookCommand = _mapper.Map<UpdateCaseNatureCommand>(btViewModel);
                    var result = await _mediator.Send(updateBookCommand);
                    if (result.Succeeded) _notify.Information($"Case Category with ID {result.Data} Updated.");
                    else
                    {
                        _notify.Error(result.Message);
                        return await RenderForm(btViewModel, false, "_CreateOrEdit");
                    }
                }
                var response = await _mediator.Send(new GetQueryCaseCategory());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CaseNatureViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteCaseNatureCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Case Category with Id {id} Deleted.");
                var response = await _mediator.Send(new GetQueryCaseCategory());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CaseNatureViewModel>>(response.Data);
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
