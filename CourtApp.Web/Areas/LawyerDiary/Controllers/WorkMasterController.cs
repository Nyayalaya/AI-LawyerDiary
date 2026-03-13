using CourtApp.Application.Enums;
using CourtApp.Application.Features.WorkMaster;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class WorkMasterController : BaseController<WorkMasterController>
    {
        public IActionResult Index()
        {
            var model = new WorkMasterViewModel();
            return View(model);
        }
        public async Task<IActionResult> LoadAllAsync()
        {
            var response = await _mediator.Send(new GetWorkMasterCommand());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<WorkMasterViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        public async Task<JsonResult> OnGetCreateOrEdit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                var ViewModel = new WorkMasterViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetWorkMasterCommand() { Id = Id });
                if (response.Succeeded)
                {
                    var data = response.Data.Where(o => o.Id == Id).FirstOrDefault();
                    var brandViewModel = _mapper.Map<WorkMasterViewModel>(data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid Id, WorkMasterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    var cmd = _mapper.Map<WorkMasterCommand>(viewModel);
                    cmd.ActionType = ((int)ActionTypes.Add);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded)
                    {
                        Id = result.Data;
                        _notify.Success($"Work Master with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var cmd = _mapper.Map<WorkMasterCommand>(viewModel);
                    cmd.ActionType = ((int)ActionTypes.Update);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded)
                        _notify.Information($"Work Master with ID {result.Data} Updated.");
                    else _notify.Error(result.Message);
                }
                var response = await _mediator.Send(new GetWorkMasterCommand());
                if (response.Succeeded)
                {
                    var vm = _mapper.Map<List<WorkMasterViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", vm);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", viewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new WorkMasterCommand { Id = id, ActionType = ((int)ActionTypes.Delete) });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Work Master  Masterwith ID {id} Deleted.");
                var response = await _mediator.Send(new GetWorkMasterCommand());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<WorkMasterViewModel>>(response.Data);
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
