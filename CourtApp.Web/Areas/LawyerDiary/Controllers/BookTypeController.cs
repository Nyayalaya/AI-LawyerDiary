using CourtApp.Application.Features.BookTypes.Command;
using CourtApp.Application.Features.BookTypes.Query.GetAllCached;
using CourtApp.Application.Features.BookTypes.Query.GetById;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class BookTypeController : BaseController<BookTypeController>
    {
        public IActionResult Index()
        {
            var model = new BookTypeViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllBookTypeCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<BookTypeViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            //var bookTypeResponse = await _mediator.Send(new GetAllBrandsCachedQuery());

            if (id == Guid.Empty)
            {
                var ViewModel = new BookTypeViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetBookTypeByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var brandViewModel = _mapper.Map<BookTypeViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }


        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, BookTypeViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createBookTypeCommand = _mapper.Map<CreateBookTypeCommand>(btViewModel);
                    var result = await _mediator.Send(createBookTypeCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Book type with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateBookCommand = _mapper.Map<UpdateCreateBookTypeCommand>(btViewModel);
                    var result = await _mediator.Send(updateBookCommand);
                    if (result.Succeeded) _notify.Information($"Book type with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAllBookTypeCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<BookTypeViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteCreateBookTypeCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Brand with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllBookTypeCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<BookTypeViewModel>>(response.Data);
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
