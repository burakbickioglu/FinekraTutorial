using FinekraTutorial.Business.Interfaces;
using FinekraTutorial.DataAccess.Interfaces;
using FinekraTutorial.DataAccess.Interfaces.Repositores;
using FinekraTutorial.Entity.Interfaces;
using System.Linq.Expressions;

namespace FinekraTutorial.Business.Services;

public class BaseService<T> : IBaseService<T> where T : class, IBaseEntity
{
    private readonly IGenericRepository<T> _repository;

    public BaseService(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public List<T> GetAll()
    {
        return _repository.GetAll().ToList();
    }

    public IQueryable<T> GetAllFiltered(Expression<Func<T, bool>>? expression = null)
    {
        return _repository.GetAllFiltered(expression);
    }
}
