using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Client.Model;
using CourtApp.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CourtApp.Web.Areas.Litigation.Controllers
{
    [Area("Client")]
    public class ManageClientController : BaseController<ManageClientController>
    {

        public IActionResult Index()
        {
            var model = new GClientViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllClientCachedQuery()
            {
                PageNumber = 1,
                PageSize = 10000,
                LinkedIds = User.GetUserLinkedIds(),
            });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<GClientViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [HttpGet]
        public IActionResult LoadCorporatForm()
        {
            // This action will return the partial view for Corporat
            return PartialView("_CorporateProfile");
        }

        public async Task<IActionResult> CreateOrEditAsync(Guid id)
        {
            try
            {
                TempData["Where"] = "Client";
                if (id == Guid.Empty)
                {
                    var ViewModel = new ClientViewModel();
                    _logger.LogInformation("Form load successfully");
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                }
                else
                {
                    var response = await _mediator.Send(new GetClientByIdQuery() { Id = id });
                    if (response.Succeeded)
                    {
                        var ViewModel = _mapper.Map<ClientViewModel>(response.Data);
                        _logger.LogInformation("Form data by id load successfully");
                        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreateOrEdit(Guid id, ClientViewModel btViewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    var createClientCommand = _mapper.Map<CreateClientCommand>(btViewModel);
                    createClientCommand.UserId = CurrentUser.Id;
                    var result = await _mediator.Send(createClientCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        TempData["ClientId"] = id;
                        _notify.Success($"Client with ID {result.Data} Created.");
                        var frm = TempData["Where"];
                        btViewModel.StatusMessage = "Record created successfully";
                        if (frm.Equals("Case"))
                        {
                            btViewModel.StatusMessage = result.Message;
                            _notify.Success($"Client with ID {result.Data} Created.");
                            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", btViewModel) });
                        }
                        else
                        {
                            var res = await _mediator.Send(new GetAllClientCachedQuery() { LinkedIds = User.GetUserLinkedIds(), PageNumber = 1, PageSize = 10000 });
                            if (res.Succeeded)
                            {
                                var viewModel = _mapper.Map<List<GClientViewModel>>(res.Data);
                                var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                                return new JsonResult(new { isValid = true, html = html });
                            }
                        }
                    }
                    else
                    {

                        _notify.Error(result.Message);
                        var res = await _mediator.Send(new GetAllClientCachedQuery() { LinkedIds = User.GetUserLinkedIds(), PageNumber = 1, PageSize = 1000 });
                        if (res.Succeeded)
                        {
                            var viewModel = _mapper.Map<List<GClientViewModel>>(res.Data);
                            var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                            return new JsonResult(new { isValid = true, html = html });
                        }
                        else
                        {
                            _notify.Error(res.Message);
                            return null;
                        }
                    }
                }
                else
                {
                    var updateClientCommand = _mapper.Map<UpdateClientCommand>(btViewModel);
                    var result = await _mediator.Send(updateClientCommand);
                    if (result.Succeeded) _notify.Information($"Client with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAllClientCachedQuery() { LinkedIds = User.GetUserLinkedIds(), PageNumber = 1, PageSize = 1000 });
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<GClientViewModel>>(response.Data);
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
        public async Task<JsonResult> OnPostDelete(Guid Id)
        {
            var deleteCommand = await _mediator.Send(new DeleteClientCommand { Id = Id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Client with Id {Id} Deleted.");
                var res = await _mediator.Send(new GetAllClientCachedQuery() { LinkedIds = User.GetUserLinkedIds(), PageNumber = 1, PageSize = 1000 });
                if (res.Succeeded)
                {
                    var viewModel = _mapper.Map<List<GClientViewModel>>(res.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(res.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                var res = await _mediator.Send(new GetAllClientCachedQuery() { LinkedIds = User.GetUserLinkedIds(), PageNumber = 1, PageSize = 1000 });
                var viewModel = _mapper.Map<List<GClientViewModel>>(res.Data);
                var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                return new JsonResult(new { isValid = true, html = html });
            }
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid CaseId)
        {
            var ViewModel = new ClientViewModel();
            ViewModel.CaseId = CaseId;
            //ViewModel.OppositCounsels = await DdlLawyerAsync();
            //ViewModel.Appearences = await DdlFSTypes(0);
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
        }

        [HttpPost]
        public async Task<JsonResult> OnCreateClientByCaseId(Guid ClientId, string Name, string Mobile)
        {
            var response = await _mediator.Send(new CreateClientByCaseIdCommand
            {
                ClientId = ClientId,
                UserId = CurrentUser.Id,
                Name = Name,
                Mobile = Mobile
            });
            return Json(response.Succeeded);
        }
    }
}
