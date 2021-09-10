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
        private readonly TContext _context;
        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet ??= _context.Set<TEntity>(); }
        }
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
            TEntity entity = _context.Find<TEntity>(id);
            _context.Remove(entity);
            _context.SaveChanges();
            return entity;
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
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }
    }
}
