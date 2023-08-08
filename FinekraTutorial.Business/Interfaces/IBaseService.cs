using FinekraTutorial.DataAccess.Interfaces;
using FinekraTutorial.Entity.Interfaces;
using System.Linq.Expressions;

namespace FinekraTutorial.Business.Interfaces;

public interface IBaseService<T> where T : class, IBaseEntity
{
    List<T> GetAll();
    IQueryable<T> GetAllFiltered(Expression<Func<T, bool>>? expression = null);
}
