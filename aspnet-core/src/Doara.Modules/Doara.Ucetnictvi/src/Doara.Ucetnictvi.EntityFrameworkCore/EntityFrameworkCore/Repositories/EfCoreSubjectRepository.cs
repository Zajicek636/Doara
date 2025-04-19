using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.EntityFrameworkCore.Base;
using Doara.Ucetnictvi.Repositories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;

namespace Doara.Ucetnictvi.EntityFrameworkCore.Repositories;

public class EfCoreSubjectRepository(IDbContextProvider<UcetnictviDbContext> dbContextProvider)
    : EfCoreRepository<UcetnictviDbContext, Subject>(dbContextProvider), ISubjectRepository
{
    public async Task<Subject> GetAsync(Guid id)
    {
        var subject = await FindAsync(x => x.Id == id);
        if (subject == null)
        {
            throw new EntityNotFoundException(typeof(Subject), id);
        }

        return subject;
    }

    public async Task<List<Subject>> GetAllAsync(int skip, int take, string sortBy, Expression<Func<Subject, bool>>? filter = null)
    {
        var query = await GetQueryableAsync();
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query
            .OrderBy(sortBy)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<long> GetCountAsync(Expression<Func<Subject, bool>>? filter = null)
    {
        var query = await GetQueryableAsync();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.CountAsync();
    }

    public async Task<Subject> CreateAsync(Subject subject)
    {
        return await base.InsertAsync(subject);
    }

    public async Task<Subject> UpdateAsync(Subject subject)
    {
        return await base.UpdateAsync(subject);
    }

    public async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(x => x.Id == id);
    }

    public async Task<bool> AnyAsync(Expression<Func<Subject, bool>> predicate)
    {
        var query = await GetQueryableAsync();
        return await query.AnyAsync(predicate);
    }
}