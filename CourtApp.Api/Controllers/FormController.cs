using CourtApp.Application.Common;
using CourtApp.Application.Features.FormManagement.Commands;
using CourtApp.Application.Features.FormManagement.DTOs;
using CourtApp.Application.Features.FormManagement.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{
    public sealed class FormController : BaseController
    {
        #region FormType Endpoints

        /// <summary>Create a new form type</summary>
        [HttpPost("form-type")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateFormTypeAsync([FromBody] CreateFormTypeCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return Created(result, "Form type created successfully");
        }

        /// <summary>Get form type by ID</summary>
        [HttpGet("form-type/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<FormTypeResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormTypeByIdAsync(Guid id)
        {
            var result = await Mediator.Send(new GetFormTypeByIdQuery { Id = id }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form type not found");
            return Success(result);
        }

        /// <summary>Get form type by code</summary>
        [HttpGet("form-type/code/{code}")]
        [ProducesResponseType(typeof(ApiResponse<FormTypeResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormTypeByCodeAsync(string code)
        {
            var result = await Mediator.Send(new GetFormTypeByCodeQuery { Code = code }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form type not found");
            return Success(result);
        }

        /// <summary>Get all form types</summary>
        [HttpGet("form-type")]
        [ProducesResponseType(typeof(ApiResponse<List<FormTypeResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllFormTypesAsync()
        {
            var result = await Mediator.Send(new GetAllFormTypesQuery(), RequestAborted);
            return Success(result);
        }

        /// <summary>Update an existing form type</summary>
        [HttpPut("form-type/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateFormTypeAsync(
            Guid id, [FromBody] UpdateFormTypeCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command, RequestAborted);
            return Success(result, "Form type updated successfully");
        }

        /// <summary>Delete a form type</summary>
        [HttpDelete("form-type/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteFormTypeAsync(Guid id)
        {
            var result = await Mediator.Send(new DeleteFormTypeCommand { Id = id }, RequestAborted);
            if (!result)
                return NotFoundResponse("Form type not found");
            return Success(result, "Form type deleted successfully");
        }

        #endregion

        #region FormMaster Endpoints

        /// <summary>Create a new form master</summary>
        [HttpPost("form-master")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateFormMasterAsync([FromBody] CreateFormMasterCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return Created(result, "Form master created successfully");
        }

        /// <summary>Get form master by ID</summary>
        [HttpGet("form-master/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<FormMasterResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormMasterByIdAsync(Guid id)
        {
            var result = await Mediator.Send(new GetFormMasterByIdQuery { Id = id }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form master not found");
            return Success(result);
        }

        /// <summary>Get form master by code</summary>
        [HttpGet("form-master/code/{code}")]
        [ProducesResponseType(typeof(ApiResponse<FormMasterResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormMasterByCodeAsync(string code)
        {
            var result = await Mediator.Send(new GetFormMasterByCodeQuery { Code = code }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form master not found");
            return Success(result);
        }

        /// <summary>Get form masters by form type</summary>
        [HttpGet("form-master/by-type/{formTypeId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<List<FormMasterResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormMastersByTypeAsync(Guid formTypeId)
        {
            var result = await Mediator.Send(
                new GetFormMastersByTypeQuery { FormTypeId = formTypeId }, RequestAborted);
            return Success(result);
        }

        /// <summary>Get all form masters</summary>
        [HttpGet("form-master")]
        [ProducesResponseType(typeof(ApiResponse<List<FormMasterResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllFormMastersAsync()
        {
            var result = await Mediator.Send(new GetAllFormMastersQuery(), RequestAborted);
            return Success(result);
        }

        /// <summary>Update an existing form master</summary>
        [HttpPut("form-master/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateFormMasterAsync(
            Guid id, [FromBody] UpdateFormMasterCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command, RequestAborted);
            return Success(result, "Form master updated successfully");
        }

        /// <summary>Delete a form master</summary>
        [HttpDelete("form-master/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteFormMasterAsync(Guid id)
        {
            var result = await Mediator.Send(new DeleteFormMasterCommand { Id = id }, RequestAborted);
            if (!result)
                return NotFoundResponse("Form master not found");
            return Success(result, "Form master deleted successfully");
        }

        #endregion

        #region FormSubtype Endpoints

        /// <summary>Create a new form subtype</summary>
        [HttpPost("form-subtype")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateFormSubtypeAsync([FromBody] CreateFormSubtypeCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return Created(result, "Form subtype created successfully");
        }

        /// <summary>Get form subtype by ID</summary>
        [HttpGet("form-subtype/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<FormSubtypeResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormSubtypeByIdAsync(Guid id)
        {
            var result = await Mediator.Send(new GetFormSubtypeByIdQuery { Id = id }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form subtype not found");
            return Success(result);
        }

        /// <summary>Get form subtype by code</summary>
        [HttpGet("form-subtype/code/{code}")]
        [ProducesResponseType(typeof(ApiResponse<FormSubtypeResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormSubtypeByCodeAsync(string code)
        {
            var result = await Mediator.Send(new GetFormSubtypeByCodeQuery { Code = code }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form subtype not found");
            return Success(result);
        }

        /// <summary>Get form subtypes by form master</summary>
        [HttpGet("form-subtype/by-form/{formId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<List<FormSubtypeResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormSubtypesByFormAsync(Guid formId)
        {
            var result = await Mediator.Send(
                new GetFormSubtypesByFormQuery { FormId = formId }, RequestAborted);
            return Success(result);
        }

        /// <summary>Get all form subtypes</summary>
        [HttpGet("form-subtype")]
        [ProducesResponseType(typeof(ApiResponse<List<FormSubtypeResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllFormSubtypesAsync()
        {
            var result = await Mediator.Send(new GetAllFormSubtypesQuery(), RequestAborted);
            return Success(result);
        }

        /// <summary>Update an existing form subtype</summary>
        [HttpPut("form-subtype/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateFormSubtypeAsync(
            Guid id, [FromBody] UpdateFormSubtypeCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command, RequestAborted);
            return Success(result, "Form subtype updated successfully");
        }

        /// <summary>Delete a form subtype</summary>
        [HttpDelete("form-subtype/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteFormSubtypeAsync(Guid id)
        {
            var result = await Mediator.Send(new DeleteFormSubtypeCommand { Id = id }, RequestAborted);
            if (!result)
                return NotFoundResponse("Form subtype not found");
            return Success(result, "Form subtype deleted successfully");
        }

        #endregion

        #region FormTemplate Endpoints

        /// <summary>Create a new form template</summary>
        [HttpPost("form-template")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateFormTemplateAsync([FromBody] CreateFormTemplateCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return Created(result, "Form template created successfully");
        }

        /// <summary>Get form template by ID</summary>
        [HttpGet("form-template/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<FormTemplateResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormTemplateByIdAsync(Guid id)
        {
            var result = await Mediator.Send(new GetFormTemplateByIdQuery { Id = id }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form template not found");
            return Success(result);
        }

        /// <summary>Get form templates by subtype</summary>
        [HttpGet("form-template/by-subtype/{formSubtypeId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<List<FormTemplateResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormTemplatesBySubtypeAsync(Guid formSubtypeId)
        {
            var result = await Mediator.Send(
                new GetFormTemplatesBySubtypeQuery { FormSubtypeId = formSubtypeId }, RequestAborted);
            return Success(result);
        }

        /// <summary>Get all form templates</summary>
        [HttpGet("form-template")]
        [ProducesResponseType(typeof(ApiResponse<List<FormTemplateResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllFormTemplatesAsync()
        {
            var result = await Mediator.Send(new GetAllFormTemplatesQuery(), RequestAborted);
            return Success(result);
        }

        /// <summary>Update an existing form template</summary>
        [HttpPut("form-template/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateFormTemplateAsync(
            Guid id, [FromBody] UpdateFormTemplateCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command, RequestAborted);
            return Success(result, "Form template updated successfully");
        }

        /// <summary>Delete a form template</summary>
        [HttpDelete("form-template/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteFormTemplateAsync(Guid id)
        {
            var result = await Mediator.Send(new DeleteFormTemplateCommand { Id = id }, RequestAborted);
            if (!result)
                return NotFoundResponse("Form template not found");
            return Success(result, "Form template deleted successfully");
        }

        #endregion

        #region FormTemplateVersion Endpoints

        /// <summary>Create a new form template version</summary>
        [HttpPost("form-template-version")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateFormTemplateVersionAsync([FromBody] CreateFormTemplateVersionCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return Created(result, "Form template version created successfully");
        }

        /// <summary>Get form template version by ID</summary>
        [HttpGet("form-template-version/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<FormTemplateVersionResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormTemplateVersionByIdAsync(Guid id)
        {
            var result = await Mediator.Send(new GetFormTemplateVersionByIdQuery { Id = id }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form template version not found");
            return Success(result);
        }

        /// <summary>Get form template versions by template</summary>
        [HttpGet("form-template-version/by-template/{formTemplateId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<List<FormTemplateVersionResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormTemplateVersionsAsync(Guid formTemplateId)
        {
            var result = await Mediator.Send(
                new GetFormTemplateVersionsQuery { FormTemplateId = formTemplateId }, RequestAborted);
            return Success(result);
        }

        /// <summary>Get all form template versions</summary>
        [HttpGet("form-template-version")]
        [ProducesResponseType(typeof(ApiResponse<List<FormTemplateVersionResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllFormTemplateVersionsAsync()
        {
            var result = await Mediator.Send(new GetAllFormTemplateVersionsQuery(), RequestAborted);
            return Success(result);
        }

        /// <summary>Update an existing form template version</summary>
        [HttpPut("form-template-version/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateFormTemplateVersionAsync(
            Guid id, [FromBody] UpdateFormTemplateVersionCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command, RequestAborted);
            return Success(result, "Form template version updated successfully");
        }

        /// <summary>Delete a form template version</summary>
        [HttpDelete("form-template-version/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteFormTemplateVersionAsync(Guid id)
        {
            var result = await Mediator.Send(new DeleteFormTemplateVersionCommand { Id = id }, RequestAborted);
            if (!result)
                return NotFoundResponse("Form template version not found");
            return Success(result, "Form template version deleted successfully");
        }

        #endregion

        #region FormCaseCategoryMapping Endpoints

        /// <summary>Create a new form case category mapping</summary>
        [HttpPost("form-case-category-mapping")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateFormCaseCategoryMappingAsync([FromBody] CreateFormCaseCategoryMappingCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return Created(result, "Form case category mapping created successfully");
        }

        /// <summary>Get form case category mapping by ID</summary>
        [HttpGet("form-case-category-mapping/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<FormCaseCategoryMappingResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormCaseCategoryMappingByIdAsync(Guid id)
        {
            var result = await Mediator.Send(new GetFormCaseCategoryMappingByIdQuery { Id = id }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form case category mapping not found");
            return Success(result);
        }

        /// <summary>Get form case category mappings by subtype</summary>
        [HttpGet("form-case-category-mapping/by-subtype/{formSubtypeId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<List<FormCaseCategoryMappingResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormCaseCategoryMappingsBySubtypeAsync(Guid formSubtypeId)
        {
            var result = await Mediator.Send(
                new GetFormCaseCategoryMappingsBySubtypeQuery { FormSubtypeId = formSubtypeId }, RequestAborted);
            return Success(result);
        }

        /// <summary>Get all form case category mappings</summary>
        [HttpGet("form-case-category-mapping")]
        [ProducesResponseType(typeof(ApiResponse<List<FormCaseCategoryMappingResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllFormCaseCategoryMappingsAsync()
        {
            var result = await Mediator.Send(new GetAllFormCaseCategoryMappingsQuery(), RequestAborted);
            return Success(result);
        }

        /// <summary>Update an existing form case category mapping</summary>
        [HttpPut("form-case-category-mapping/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateFormCaseCategoryMappingAsync(
            Guid id, [FromBody] UpdateFormCaseCategoryMappingCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command, RequestAborted);
            return Success(result, "Form case category mapping updated successfully");
        }

        /// <summary>Delete a form case category mapping</summary>
        [HttpDelete("form-case-category-mapping/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteFormCaseCategoryMappingAsync(Guid id)
        {
            var result = await Mediator.Send(new DeleteFormCaseCategoryMappingCommand { Id = id }, RequestAborted);
            if (!result)
                return NotFoundResponse("Form case category mapping not found");
            return Success(result, "Form case category mapping deleted successfully");
        }

        #endregion

        #region FormCourtMapping Endpoints

        /// <summary>Create a new form court mapping</summary>
        [HttpPost("form-court-mapping")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateFormCourtMappingAsync([FromBody] CreateFormCourtMappingCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return Created(result, "Form court mapping created successfully");
        }

        /// <summary>Get form court mapping by ID</summary>
        [HttpGet("form-court-mapping/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<FormCourtMappingResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormCourtMappingByIdAsync(Guid id)
        {
            var result = await Mediator.Send(new GetFormCourtMappingByIdQuery { Id = id }, RequestAborted);
            if (result == null)
                return NotFoundResponse("Form court mapping not found");
            return Success(result);
        }

        /// <summary>Get form court mappings by subtype</summary>
        [HttpGet("form-court-mapping/by-subtype/{formSubtypeId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<List<FormCourtMappingResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFormCourtMappingsBySubtypeAsync(Guid formSubtypeId)
        {
            var result = await Mediator.Send(
                new GetFormCourtMappingsBySubtypeQuery { FormSubtypeId = formSubtypeId }, RequestAborted);
            return Success(result);
        }

        /// <summary>Get all form court mappings</summary>
        [HttpGet("form-court-mapping")]
        [ProducesResponseType(typeof(ApiResponse<List<FormCourtMappingResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllFormCourtMappingsAsync()
        {
            var result = await Mediator.Send(new GetAllFormCourtMappingsQuery(), RequestAborted);
            return Success(result);
        }

        /// <summary>Update an existing form court mapping</summary>
        [HttpPut("form-court-mapping/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateFormCourtMappingAsync(
            Guid id, [FromBody] UpdateFormCourtMappingCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command, RequestAborted);
            return Success(result, "Form court mapping updated successfully");
        }

        /// <summary>Delete a form court mapping</summary>
        [HttpDelete("form-court-mapping/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteFormCourtMappingAsync(Guid id)
        {
            var result = await Mediator.Send(new DeleteFormCourtMappingCommand { Id = id }, RequestAborted);
            if (!result)
                return NotFoundResponse("Form court mapping not found");
            return Success(result, "Form court mapping deleted successfully");
        }

        #endregion
    }
}
