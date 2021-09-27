using Microsoft.EntityFrameworkCore;
using ShopBridge.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private IDbContext _context;

        public Repository(IDbContext context)
        {
            _context = context;
        }

        private DbSet<TEntity> Entities
        {
            get { return this._context.Set<TEntity>(); }
        }

        public IQueryable<TEntity> Table => this._context.Set<TEntity>(); 

        public async Task Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(entity.GetType().Name);

            Entities.Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(entity.GetType().Name);

            Entities.Add(entity);
            await this._context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(entity.GetType().Name);

            await this._context.SaveChangesAsync();
            return entity;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                }
            }
        }
    }
}
