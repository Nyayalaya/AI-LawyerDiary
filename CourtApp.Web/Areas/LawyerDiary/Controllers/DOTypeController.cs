using CourtApp.Application.Features.DOType;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("lawyerdiary")]
    public class DOTypeController : BaseController<DOTypeController>
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllDOTypeCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<DOTypeViewModel>>(response.Data);
                var count = viewModel.Count();
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                var viewModel = new DOTypeViewModel();
                viewModel.Types = DOTypes();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", viewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetDOTypeByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<DOTypeViewModel>(response.Data);
                    ViewModel.Types = DOTypes();
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                return null;
            }
        }
        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, DOTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var cmd = _mapper.Map<CreateDOTypeCommand>(model);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Draft order with ID {result.Data} Created.");
                    }
                    else
                    {
                        _notify.Error(result.Message);
                        return await RenderForm(model, false, "_CreateOrEdit");
                    }
                }
                else
                {
                    var updateBrandCommand = _mapper.Map<UpdateDOTypeCommand>(model);
                    var result = await _mediator.Send(updateBrandCommand);
                    if (result.Succeeded) _notify.Information($"Brand with ID {result.Data} Updated.");
                    else
                    {
                        _notify.Error(result.Message);
                        return await RenderForm(model, false, "_CreateOrEdit");
                    }
                }
                var response = await _mediator.Send(new GetAllDOTypeCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<DOTypeViewModel>>(response.Data);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", model);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteDOTypeCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Draft & Order with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllDOTypeCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<DOTypeViewModel>>(response.Data);
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
