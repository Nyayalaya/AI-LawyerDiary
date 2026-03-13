using CourtApp.Application.Features.CaseStages.Command;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class CaseStageController : BaseController<CaseStageController>
    {
        public IActionResult Index()
        {
            var model = new CaseStageViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new CaseStageCacheAllQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CaseStageViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new CaseStageViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new CaseStageByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var brandViewModel = _mapper.Map<CaseStageViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }


        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, CaseStageViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createBookTypeCommand = _mapper.Map<CreateCaseStageCommand>(btViewModel);
                    var result = await _mediator.Send(createBookTypeCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Case stage with ID {result.Data} Created.");
                    }
                    else
                    {
                        _notify.Error(result.Message);
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", btViewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                else
                {
                    var updateBookCommand = _mapper.Map<UpdateCaseStageCommand>(btViewModel);
                    var result = await _mediator.Send(updateBookCommand);
                    if (result.Succeeded) _notify.Information($"Case Nature with ID {result.Data} Updated.");
                    else
                    {
                        _notify.Error(result.Message);
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", btViewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                var response = await _mediator.Send(new CaseStageCacheAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CaseStageViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteCaseStageCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Case Nature with Id {id} Deleted.");
                var response = await _mediator.Send(new CaseStageCacheAllQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CaseStageViewModel>>(response.Data);
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
