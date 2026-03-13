using CourtApp.Application.Features.CaseKinds.Commands;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class CaseKindController : BaseController<CaseKindController>
    {
        public IActionResult Index()
        {
            var model = new CaseKindViewModel();
            return View(model);
        }
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new CaseKindAllCacheQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CaseKindViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            var courtTypeList = await _mediator.Send(new GetCourtTypeQuery());
            if (id == Guid.Empty)
            {
                var ViewModel = new CaseKindViewModel();
                var courtTypeViewModel = _mapper.Map<List<CourtTypeViewModel>>(courtTypeList.Data);
                ViewModel.CourtTypes = new SelectList(courtTypeViewModel, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null); ;

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new CaseKindByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<CaseKindViewModel>(response.Data);
                    var courtTypeViewModel = _mapper.Map<List<CourtTypeViewModel>>(courtTypeList.Data);
                    ViewModel.CourtTypes = new SelectList(courtTypeViewModel, nameof(CourtTypeViewModel.Id), nameof(CourtTypeViewModel.CourtType), null, null); ;

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, CaseKindViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createBookTypeCommand = _mapper.Map<CreateCaseKindCommand>(btViewModel);
                    var result = await _mediator.Send(createBookTypeCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Case kind with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateBookCommand = _mapper.Map<UpdateCaseKindCommand>(btViewModel);
                    var result = await _mediator.Send(updateBookCommand);
                    if (result.Succeeded) _notify.Information($"Case kind with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new CaseKindAllCacheQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CaseKindViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteCaseKindCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Case Nature with Id {id} Deleted.");
                var response = await _mediator.Send(new CaseKindAllCacheQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<CaseKindViewModel>>(response.Data);
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
