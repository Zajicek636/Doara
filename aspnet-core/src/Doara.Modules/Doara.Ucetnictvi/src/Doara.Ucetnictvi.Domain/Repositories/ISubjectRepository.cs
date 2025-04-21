using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Entities;

namespace Doara.Ucetnictvi.Repositories;

public interface ISubjectRepository
{
    Task<Subject> GetAsync(Guid id);
    Task<List<Subject>> GetAllAsync(int skip, int take, string sortBy, bool withDetail, Expression<Func<Subject, bool>>? filter = null);
    Task<long> GetCountAsync(Expression<Func<Subject, bool>>? filter = null);
    Task<Subject> CreateAsync(Subject subject);
    Task<Subject> UpdateAsync(Subject subject);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<Subject, bool>> predicate);
}