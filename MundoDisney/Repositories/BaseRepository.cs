using Microsoft.EntityFrameworkCore;
using MundoDisney.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Repositories
{
    public abstract class BaseRepository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        protected readonly TContext _context;

        protected BaseRepository(TContext context)
        {
            _context = context;
        }
        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity Delete(int id)
        {

            var aux = Get(id);
            if (aux != null)
            {
                _context.Set<TEntity>().Remove(aux);
                _context.SaveChanges();
                return aux;
            }
            return null;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetAllEntities()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity Update(TEntity entity)
        {
            var aux = _context.Set<TEntity>().Find(entity);
            if (aux != null)
            {
                _context.Set<TEntity>().Update(aux);
                _context.SaveChanges();
                return aux;
            }
            return null;
        }
    }
}
