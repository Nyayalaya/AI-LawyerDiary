using CourtApp.Application.Features.BookMasters.Command;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Application.Features.BookTypes.Query.GetAllCached;
using CourtApp.Application.Features.Publications.Queries;
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
    public class BookMasterController : BaseController<BookMasterController>
    {

        public IActionResult Index()
        {
            var model = new BookMasterViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllBookMasterQuery(1,100));
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<BookMasterViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }       

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            var bookTypes = await _mediator.Send(new GetAllBookTypeCachedQuery());
            var publications = await _mediator.Send(new GetAllPublisherCachedQuery());
            if (id == Guid.Empty)
            {
                var ViewModel = new BookMasterViewModel();

                if (bookTypes.Succeeded)
                {
                    var bookTypeViewModel = _mapper.Map<List<BookTypeViewModel>>(bookTypes.Data);
                    ViewModel.BookTypes = new SelectList(bookTypeViewModel, nameof(BookTypeViewModel.Id), nameof(BookTypeViewModel.Name_En), null, null);
                }
                if (publications.Succeeded)
                {
                    var pubViewModel = _mapper.Map<List<PublisherViewModel>>(publications.Data);
                    ViewModel.Publishers = new SelectList(pubViewModel, nameof(PublisherViewModel.Id), nameof(PublisherViewModel.PublicationName), null, null);
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetBookMasterByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var ViewModel = _mapper.Map<BookMasterViewModel>(response.Data);
                    if (bookTypes.Succeeded)
                    {
                        var bookTypeViewModel = _mapper.Map<List<BookTypeViewModel>>(bookTypes.Data);
                        ViewModel.BookTypes = new SelectList(bookTypeViewModel, nameof(BookTypeViewModel.Id), nameof(BookTypeViewModel.Name_En), null, null);
                    }
                    if (publications.Succeeded)
                    {
                        var pubViewModel = _mapper.Map<List<PublisherViewModel>>(publications.Data);
                        ViewModel.Publishers = new SelectList(pubViewModel, nameof(PublisherViewModel.Id), nameof(PublisherViewModel.PublicationName), null, null);
                    }

                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, BookMasterViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createCommand = _mapper.Map<CreateBookMasterCommand>(ViewModel);
                    var result = await _mediator.Send(createCommand);
                    if (result.Succeeded)
                        _notify.Success($"Book Master with ID {result.Data} Created.");
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateCommand = _mapper.Map<UpdateBookMasterCommand>(ViewModel);
                    var result = await _mediator.Send(updateCommand);
                    if (result.Succeeded) 
                        _notify.Information($"Book type with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAllBookMasterQuery(1, 100));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<BookMasterViewModel>>(response.Data);
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
            var deleteCommand = await _mediator.Send(new DeleteBookMasterCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Book Master with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllBookMasterQuery(1, 100));
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<BookMasterViewModel>>(response.Data);
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
