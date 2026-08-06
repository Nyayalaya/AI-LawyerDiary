using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormManagement.Interfaces
{
    public interface IFormMasterRepository
    {
        IQueryable<FormMasterEntity> Entities { get; }
        Task<FormMasterEntity> GetByIdAsync(Guid id);
        Task<FormMasterEntity> GetByCodeAsync(string code);
        Task<List<FormMasterEntity>> GetListAsync();
        Task<List<FormMasterEntity>> GetByFormTypeAsync(Guid formTypeId);
        Task<Guid> InsertAsync(FormMasterEntity entity);
        Task UpdateAsync(FormMasterEntity entity);
        Task DeleteAsync(FormMasterEntity entity);
    }

    public interface IFormSubtypeRepository
    {
        IQueryable<FormSubtypeEntity> Entities { get; }
        Task<FormSubtypeEntity> GetByIdAsync(Guid id);
        Task<FormSubtypeEntity> GetByCodeAsync(string code);
        Task<List<FormSubtypeEntity>> GetListAsync();
        Task<List<FormSubtypeEntity>> GetByFormAsync(Guid formId);
        Task<Guid> InsertAsync(FormSubtypeEntity entity);
        Task UpdateAsync(FormSubtypeEntity entity);
        Task DeleteAsync(FormSubtypeEntity entity);
    }

    public interface IFormTemplateRepository
    {
        IQueryable<FormTemplateEntity> Entities { get; }
        Task<FormTemplateEntity> GetByIdAsync(Guid id);
        Task<List<FormTemplateEntity>> GetListAsync();
        Task<List<FormTemplateEntity>> GetBySubtypeAsync(Guid formSubtypeId);
        Task<Guid> InsertAsync(FormTemplateEntity entity);
        Task UpdateAsync(FormTemplateEntity entity);
        Task DeleteAsync(FormTemplateEntity entity);
    }

    public interface IFormTemplateVersionRepository
    {
        IQueryable<FormTemplateVersionEntity> Entities { get; }
        Task<FormTemplateVersionEntity> GetByIdAsync(Guid id);
        Task<List<FormTemplateVersionEntity>> GetListAsync();
        Task<List<FormTemplateVersionEntity>> GetByTemplateAsync(Guid formTemplateId);
        Task<Guid> InsertAsync(FormTemplateVersionEntity entity);
        Task UpdateAsync(FormTemplateVersionEntity entity);
        Task DeleteAsync(FormTemplateVersionEntity entity);
    }

    public interface IFormCaseCategoryMappingRepository
    {
        IQueryable<FormCaseCategoryMapping> Entities { get; }
        Task<FormCaseCategoryMapping> GetByIdAsync(Guid id);
        Task<List<FormCaseCategoryMapping>> GetListAsync();
        Task<List<FormCaseCategoryMapping>> GetByFormSubtypeAsync(Guid formSubtypeId);
        Task<FormCaseCategoryMapping> GetBySubtypeAndCategoryAsync(Guid formSubtypeId, Guid caseCategoryId);
        Task<Guid> InsertAsync(FormCaseCategoryMapping entity);
        Task UpdateAsync(FormCaseCategoryMapping entity);
        Task DeleteAsync(FormCaseCategoryMapping entity);
    }

    public interface IFormCourtMappingRepository
    {
        IQueryable<FormCourtMapping> Entities { get; }
        Task<FormCourtMapping> GetByIdAsync(Guid id);
        Task<List<FormCourtMapping>> GetListAsync();
        Task<List<FormCourtMapping>> GetByFormSubtypeAsync(Guid formSubtypeId);
        Task<FormCourtMapping> GetBySubtypeAndCourtAsync(Guid formSubtypeId, Guid courtTypeId);
        Task<Guid> InsertAsync(FormCourtMapping entity);
        Task UpdateAsync(FormCourtMapping entity);
        Task DeleteAsync(FormCourtMapping entity);
    }
}
