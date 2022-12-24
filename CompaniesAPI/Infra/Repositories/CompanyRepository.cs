﻿using CompaniesAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CompaniesAPI.Infra.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public CompanyRepository(ApplicationDbContext applicationDbContext)
        {
            _appDbContext = applicationDbContext;
        }

        public async Task<Company> AddAsync(Company entity)
        {
            await _appDbContext.Companies.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public void DeleteAsync(Company entity)
        {
            _appDbContext.Companies.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public async Task<Company> GetAsync(int id)
        {
            var entity = await _appDbContext.Companies.Where(c => c.Id == id).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<IList<Company>> GetAsync()
        {
            var list = await _appDbContext.Companies.ToListAsync();
            return list;
        }

        public async Task<IList<Company>> GetByNameAsync(string name)
        {
            var list = await _appDbContext.Companies.Where(c => c.Name.Contains(name)).ToListAsync();
            return list;
        }

        public async Task UpdateAsync(Company entity)
        {
            _appDbContext.Attach(entity);
            _appDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
