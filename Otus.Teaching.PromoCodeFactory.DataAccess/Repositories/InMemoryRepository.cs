using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>
        : IRepository<T>
        where T : BaseEntity
    {
        protected List<T> Data { get; set; }

        public InMemoryRepository(List<T> data)
        {
            Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult((IEnumerable<T>)Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<Guid> PostAsync(T entity)
        {
            Data.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var indexOfEntity = Data.FindIndex(e => e.Id == id);
            if (indexOfEntity > -1) {
                Data.RemoveAt(indexOfEntity);
                return Task.FromResult(true);
            } else {
                return Task.FromResult(false);
            }
        }

        public Task<bool> UpdateAsync(T entity)
        {
            var indexOfEntity = Data.FindIndex(e => e.Id == entity.Id);
            if (indexOfEntity > -1) {
                Data[indexOfEntity] = entity;
                return Task.FromResult(true);
            } else {
                return Task.FromResult(false);
            }
        }

        public Task<bool> IsExistAsync(Guid id)
        {
            var isExist = Data.Exists(e => e.Id == id);
            return Task.FromResult(isExist);
        }
    }
}