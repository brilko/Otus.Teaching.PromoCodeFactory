using Otus.Teaching.PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task<Guid> PostAsync(T entity);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> UpdateAsync(T entity);

        Task<bool> IsExistAsync(Guid id);
    }
}