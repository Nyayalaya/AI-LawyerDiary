using CourtApp.Application.Features.Publications.Command;
using CourtApp.Application.Features.Publications.Queries;
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
    public class PublisherController : BaseController<PublisherController>
    {
        public IActionResult Index()
        {
            var model = new PublisherViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllPublisherCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<PublisherViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id )
        {
          
            if (id == Guid.Empty)
            {
                var ViewModel = new PublisherViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetPublicationByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var brandViewModel = _mapper.Map<PublisherViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, PublisherViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createBookTypeCommand = _mapper.Map<CreatePublicationCommand>(btViewModel);
                    var result = await _mediator.Send(createBookTypeCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Publication with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateBookCommand = _mapper.Map<UpdatePublicationCommand>(btViewModel);
                    var result = await _mediator.Send(updateBookCommand);
                    if (result.Succeeded) _notify.Information($"Publication with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAllPublisherCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<PublisherViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeletePublicationCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Publication with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllPublisherCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<PublisherViewModel>>(response.Data);
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
