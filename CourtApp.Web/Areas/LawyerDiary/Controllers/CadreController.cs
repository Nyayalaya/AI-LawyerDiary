using CourtApp.Application.Features.Cadre;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class CadreController : BaseController<CadreController>
    {
        public IActionResult Index()
        {
            var model = new CadreMasterViewModel();
            return View(model);
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetQueryCadre());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CadreMasterViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var ViewModel = new CadreMasterViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetQueryByIdCadre() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<CadreMasterViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, CadreMasterViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<CadreCreateCommand>(ViewModel);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                        _notify.Success($"Cadre with ID {result.Data} Created.");
                    else
                    {
                        _notify.Error(result.Message);
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateCadreCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded)
                        _notify.Information($"Cadre with ID {result.Data} Updated.");
                    else
                    {
                        _notify.Error(result.Message);
                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                var response = await _mediator.Send(new GetQueryCadre());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CadreMasterViewModel>>(response.Data);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteCadreCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Cadre with Id {id} Deleted.");
                var response = await _mediator.Send(new GetQueryCadre());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CadreMasterViewModel>>(response.Data);
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
