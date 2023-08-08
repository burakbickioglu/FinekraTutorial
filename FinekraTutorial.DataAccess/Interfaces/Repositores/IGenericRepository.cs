using FinekraTutorial.Entity.Interfaces;

namespace FinekraTutorial.DataAccess.Interfaces.Repositores;

public interface IGenericRepository<T> : IDataRepository<T> where T : class, IBaseEntity
{
}

