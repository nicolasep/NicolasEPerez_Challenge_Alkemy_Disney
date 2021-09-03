using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Interfaces
{
    public interface IRepository<TEntity>
    {
        List<TEntity> GetAllEntities();

        TEntity Get(int id);

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Delete(int id);

    }
}
