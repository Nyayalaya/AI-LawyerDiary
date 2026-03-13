using CourtApp.Application.Features.WorkMaster;
using CourtApp.Application.Features.WorkMasterSub;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class WorkMasterSubController : BaseController<WorkMasterSubController>
    {
        public IActionResult Index()
        {
            var model = new WorkMasterSubViewModel();
            return View(model);
        }
        public async Task<IActionResult> LoadAllAsync()
        {
            var response = await _mediator.Send(new GWorkSubMstQuery() { PageNumber = 1, PageSize = 1000 });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<WorkMasterSubViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
        public async Task<JsonResult> OnGetCreateOrEdit(Guid Id)
        {
            var wMasterList = await _mediator.Send(new GetWorkMasterCommand());
            if (Id == Guid.Empty)
            {
                var ViewModel = new WorkMasterSubViewModel();
                ViewModel.Works = new List<WorkSubMaster> { new WorkSubMaster() };
                var wMasterViewModel = _mapper.Map<List<WorkMasterViewModel>>(wMasterList.Data);
                ViewModel.WMasters = new SelectList(wMasterViewModel, nameof(WorkMasterViewModel.Id), nameof(WorkMasterViewModel.Work_En), null, null);
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GWorkSubMstByIdQuery { Id = Id });
                if (response.Succeeded)
                {
                    var brandViewModel = _mapper.Map<WorkMasterSubViewModel>(response.Data);
                    var wMasterViewModel = _mapper.Map<List<WorkMasterViewModel>>(wMasterList.Data);
                    brandViewModel.WMasters = new SelectList(wMasterViewModel, nameof(WorkMasterViewModel.Id), nameof(WorkMasterViewModel.Work_En), null, null);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Edit", brandViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid Id, WorkMasterSubViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Id == Guid.Empty)
                {
                    try
                    {
                        var cmd = _mapper.Map<CreateWorkSubMstCommand>(viewModel);
                        var result = await _mediator.Send(cmd);
                        if (result.Succeeded)
                        {
                            Id = result.Data;
                            _notify.Success($"Work Master Sub with ID {result.Data} Created.");
                            var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                            return new JsonResult(new { isValid = true, html = html });

                        }
                        else
                        {
                            _notify.Success(result.Message);
                            var html = await _viewRenderer.RenderViewToStringAsync("_Create", viewModel);
                            return new JsonResult(new { isValid = false, html = html });
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message.ToString());
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    var cmd = _mapper.Map<UpdateWorkSubMstCommand>(viewModel);
                    var result = await _mediator.Send(cmd);
                    if (result.Succeeded)
                        _notify.Information($"Work Master with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GWorkSubMstQuery() { PageNumber = 1, PageSize = 1000 });
                if (response.Succeeded)
                {
                    var dt = _mapper.Map<List<WorkMasterSubViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", dt);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_Create", viewModel);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            var deleteCommand = await _mediator.Send(new DeleteWorkSubMstCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Work Master Sub with ID {id} Deleted.");
                var response = await _mediator.Send(new GWorkSubMstQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<WorkMasterSubViewModel>>(response.Data);
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
