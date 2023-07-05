using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Mappers
{
    public class BaseMapper<T1, T2>
    {
        public static Mapper CreateMapper()
        {
            return MapperFactory<T1, T2>.Produce();
        }
    }
}
