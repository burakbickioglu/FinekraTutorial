using FinekraTutorial.Entity.Interfaces;
using System.Linq.Expressions;

namespace FinekraTutorial.DataAccess.Interfaces;

public interface IDataRepository<T> where T : class, IBaseEntity
{
    List<T> GetAll();
    Task<T> Get(Guid id);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> expression);
    IQueryable<T> GetAllFiltered(Expression<Func<T, bool>>? expression = null);
    Task<bool> AddAsync(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(Guid id);
    Task<bool> SaveAsync(T data);

}
