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
    : EfCoreRepository<UcetnictviDbContext, Subject, Guid>(dbContextProvider), ISubjectRepository
{
    public async Task<Subject> GetAsync(Guid id)
    {
        var subject = await FindAsync(id);
        if (subject == null)
        {
            throw new EntityNotFoundException(typeof(Subject), id);
        }

        return subject;
    }

    public async Task<List<Subject>> GetAllAsync(int skip, int take, string sortBy, bool withDetail, Expression<Func<Subject, bool>>? filter = null)
    {
        var query = withDetail ? await WithDetailsAsync(): await GetQueryableAsync();
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
        await base.DeleteAsync(id);
    }

    public async Task<bool> AnyAsync(Expression<Func<Subject, bool>> predicate)
    {
        var query = await GetQueryableAsync();
        return await query.AnyAsync(predicate);
    }
    
    public override async Task<IQueryable<Subject>> WithDetailsAsync()
    {
        return (await base.WithDetailsAsync()).Include(x => x.Address)
            .Include(x => x.Address.Country);
    }
}