
using CourtApp.Application.Features.FormManagement.Interfaces;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class FormMasterRepository : IFormMasterRepository
    {
        private readonly IRepositoryAsync<FormMasterEntity> _repository;

        public IQueryable<FormMasterEntity> Entities => _repository.Entities;

        public FormMasterRepository(IRepositoryAsync<FormMasterEntity> repository)
        {
            _repository = repository;
        }

        public async Task<FormMasterEntity> GetByIdAsync(Guid id)
        {
            return await _repository.Entities
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<FormMasterEntity> GetByCodeAsync(string code)
        {
            return await _repository.Entities
                .Where(x => x.Code == code)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FormMasterEntity>> GetListAsync()
        {
            return await _repository.Entities
                .Include(f=>f.FormType)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<FormMasterEntity>> GetByFormTypeAsync(Guid formTypeId)
        {
            return await _repository.Entities
                .Where(x => x.FormTypeId == formTypeId)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Guid> InsertAsync(FormMasterEntity entity)
        {
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(FormMasterEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(FormMasterEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }
    }

    public class FormSubtypeRepository : IFormSubtypeRepository
    {
        private readonly IRepositoryAsync<FormSubtypeEntity> _repository;

        public IQueryable<FormSubtypeEntity> Entities => _repository.Entities;

        public FormSubtypeRepository(IRepositoryAsync<FormSubtypeEntity> repository)
        {
            _repository = repository;
        }

        public async Task<FormSubtypeEntity> GetByIdAsync(Guid id)
        {
            return await _repository.Entities
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<FormSubtypeEntity> GetByCodeAsync(string code)
        {
            return await _repository.Entities
                .Where(x => x.Code == code)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FormSubtypeEntity>> GetListAsync()
        {
            return await _repository.Entities
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<FormSubtypeEntity>> GetByFormAsync(Guid formId)
        {
            return await _repository.Entities
                .Where(x => x.FormId == formId)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Guid> InsertAsync(FormSubtypeEntity entity)
        {
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(FormSubtypeEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(FormSubtypeEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }
    }

    public class FormTemplateRepository : IFormTemplateRepository
    {
        private readonly IRepositoryAsync<FormTemplateEntity> _repository;

        public IQueryable<FormTemplateEntity> Entities => _repository.Entities;

        public FormTemplateRepository(IRepositoryAsync<FormTemplateEntity> repository)
        {
            _repository = repository;
        }

        public async Task<FormTemplateEntity> GetByIdAsync(Guid id)
        {
            return await _repository.Entities
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FormTemplateEntity>> GetListAsync()
        {
            return await _repository.Entities
                .Where(x => x.IsActive)
                .OrderBy(x => x.Title)
                .ToListAsync();
        }

        public async Task<List<FormTemplateEntity>> GetBySubtypeAsync(Guid formSubtypeId)
        {
            return await _repository.Entities
                .Where(x => x.FormSubtypeId == formSubtypeId)
                .OrderBy(x => x.Title)
                .ToListAsync();
        }

        public async Task<Guid> InsertAsync(FormTemplateEntity entity)
        {
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(FormTemplateEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(FormTemplateEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }
    }

    public class FormTemplateVersionRepository : IFormTemplateVersionRepository
    {
        private readonly IRepositoryAsync<FormTemplateVersionEntity> _repository;

        public IQueryable<FormTemplateVersionEntity> Entities => _repository.Entities;

        public FormTemplateVersionRepository(IRepositoryAsync<FormTemplateVersionEntity> repository)
        {
            _repository = repository;
        }

        public async Task<FormTemplateVersionEntity> GetByIdAsync(Guid id)
        {
            return await _repository.Entities
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FormTemplateVersionEntity>> GetListAsync()
        {
            return await _repository.Entities
                .OrderByDescending(x => x.Version)
                .ToListAsync();
        }

        public async Task<List<FormTemplateVersionEntity>> GetByTemplateAsync(Guid formTemplateId)
        {
            return await _repository.Entities
                .Where(x => x.FormTemplateId == formTemplateId)
                .OrderByDescending(x => x.Version)
                .ToListAsync();
        }

        public async Task<Guid> InsertAsync(FormTemplateVersionEntity entity)
        {
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(FormTemplateVersionEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(FormTemplateVersionEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }
    }

    public class FormCaseCategoryMappingRepository : IFormCaseCategoryMappingRepository
    {
        private readonly IRepositoryAsync<FormCaseCategoryMapping> _repository;

        public IQueryable<FormCaseCategoryMapping> Entities => _repository.Entities;

        public FormCaseCategoryMappingRepository(IRepositoryAsync<FormCaseCategoryMapping> repository)
        {
            _repository = repository;
        }

        public async Task<FormCaseCategoryMapping> GetByIdAsync(Guid id)
        {
            return await _repository.Entities
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FormCaseCategoryMapping>> GetListAsync()
        {
            return await _repository.Entities
                .Where(x => x.IsActive)
                .OrderBy(x => x.FormSubtypeId)
                .ToListAsync();
        }

        public async Task<List<FormCaseCategoryMapping>> GetByFormSubtypeAsync(Guid formSubtypeId)
        {
            return await _repository.Entities
                .Where(x => x.FormSubtypeId == formSubtypeId && x.IsActive)
                .ToListAsync();
        }

        public async Task<FormCaseCategoryMapping> GetBySubtypeAndCategoryAsync(Guid formSubtypeId, Guid caseCategoryId)
        {
            return await _repository.Entities
                .Where(x => x.FormSubtypeId == formSubtypeId && x.CaseCategoryId == caseCategoryId)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> InsertAsync(FormCaseCategoryMapping entity)
        {
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(FormCaseCategoryMapping entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(FormCaseCategoryMapping entity)
        {
            await _repository.DeleteAsync(entity);
        }
    }

    public class FormCourtMappingRepository : IFormCourtMappingRepository
    {
        private readonly IRepositoryAsync<FormCourtMapping> _repository;

        public IQueryable<FormCourtMapping> Entities => _repository.Entities;

        public FormCourtMappingRepository(IRepositoryAsync<FormCourtMapping> repository)
        {
            _repository = repository;
        }

        public async Task<FormCourtMapping> GetByIdAsync(Guid id)
        {
            return await _repository.Entities
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FormCourtMapping>> GetListAsync()
        {
            return await _repository.Entities
                .OrderBy(x => x.FormSubtypeId)
                .ToListAsync();
        }

        public async Task<List<FormCourtMapping>> GetByFormSubtypeAsync(Guid formSubtypeId)
        {
            return await _repository.Entities
                .Where(x => x.FormSubtypeId == formSubtypeId)
                .ToListAsync();
        }

        public async Task<FormCourtMapping> GetBySubtypeAndCourtAsync(Guid formSubtypeId, Guid courtTypeId)
        {
            return await _repository.Entities
                .Where(x => x.FormSubtypeId == formSubtypeId && x.CourtTypeId == courtTypeId)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> InsertAsync(FormCourtMapping entity)
        {
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(FormCourtMapping entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(FormCourtMapping entity)
        {
            await _repository.DeleteAsync(entity);
        }
    }
}
