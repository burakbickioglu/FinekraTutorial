using FinekraTutorial.DataAccess.Context;
using FinekraTutorial.DataAccess.Interfaces;
using FinekraTutorial.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinekraTutorial.DataAccess.Repositories;

public class DataRepository<T, TContext> : IDataRepository<T>
      where T : class, IBaseEntity
    where TContext : FinekraDbContext
{
    public readonly TContext context;

    public DataRepository(TContext context)
    {
        this.context = context;
    }

    public async Task<bool> AddAsync(T entity)
    {
        entity.IsDeleted = false;
        entity.CreatedOn = DateTime.Now;
        entity.Id = Guid.NewGuid();
        context.Set<T>().Add(entity);
        return await SaveAsync(entity);
    }

    public async Task<int> CountAsync()
    {
        return await context.Set<T>().CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
    {
        return await context.Set<T>().Where(expression).CountAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await context.Set<T>().FindAsync(id);
        if (entity == null)
            return false;

        context.Set<T>().Remove(entity);
        return await SaveAsync(entity);
    }

    public async Task<T> Get(Guid id)
    {
        var data = await context.Set<T>().FindAsync(id);
        if (data == null) return null;

        context.Entry(data).State = EntityState.Detached;
        return data;
    }
    public List<T> GetAll()
    {
        return context.Set<T>().ToList();
    }

    public IQueryable<T> GetAllFiltered(Expression<Func<T, bool>>? expression = null)
    {
        var result = context.Set<T>().Where(s => !s.IsDeleted).OrderByDescending(s => s.CreatedOn).AsSplitQuery();
        if (expression != null)
            result = result.Where(expression);

        return result;
    }

    public async Task<bool> Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        return await SaveAsync(entity);
    }

    public async Task<bool> SaveAsync(T data)
    {
        try
        {
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            context.Entry(data).State = EntityState.Detached;
            return false;
        }
    }
}
