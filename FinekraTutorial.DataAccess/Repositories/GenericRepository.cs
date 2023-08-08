using FinekraTutorial.DataAccess.Context;
using FinekraTutorial.DataAccess.Interfaces.Repositores;
using FinekraTutorial.Entity.Interfaces;

namespace FinekraTutorial.DataAccess.Repositories;

public class GenericRepository<T> : DataRepository<T, FinekraDbContext>, IGenericRepository<T> where T : class, IBaseEntity
{
    public GenericRepository(FinekraDbContext context) : base(context)
    {
    }
}
