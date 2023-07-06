using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Otus.Teaching.PromoCodeFactory.DataAccess.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class EfRepository<T>
        : IRepository<T>
        where T : BaseEntity
    {

        protected readonly DbSet<T> entitySet;
        protected readonly ApplicationContext context;

        public EfRepository(ApplicationContext context)
        {
            this.context = context;
            entitySet = context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Task.Run(() => {
                if (!IsExist(id, out T ent)) {
                    return false;
                }
                entitySet.Remove(ent);
                context.SaveChanges();
                return true;
            });
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entitySet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await entitySet.FindAsync(id);
        }


        private bool IsExist(Guid id, out T entity) 
        {
            entity = entitySet.Find(id);
            return entity != null;
        }
        public async Task<bool> IsExistAsync(Guid id)
        {
            return await Task.Run(() => {
                return IsExist(id, out _);
            });
        }

        public async Task<Guid> PostAsync(T entity)
        {
            return await Task.Run(() => {
                var entToReturn = entitySet.Add(entity);
                context.SaveChanges();
                return entToReturn.Entity.Id;
            });
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await Task.Run(() => {
                if (!IsExist(entity.Id, out _)) {
                    return false;
                }
                entitySet.Update(entity);
                context.SaveChanges();
                return true;
            });
        }
    }
}
