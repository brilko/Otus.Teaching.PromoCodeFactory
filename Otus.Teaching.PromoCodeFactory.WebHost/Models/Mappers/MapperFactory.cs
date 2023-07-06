using AutoMapper;
using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Mappers
{
    public static class MapperFactory<T1, T2>
    {
        public static Mapper Produce(
            Func<IMappingExpression<T1, T2>, IMappingExpression<T1, T2>> changeMappingExpression = null)
        {
            if (changeMappingExpression == null)
                return new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>()));
            var configuration = new MapperConfiguration(
                    cfg => changeMappingExpression(cfg.CreateMap<T1, T2>()));
            return new Mapper(configuration);
        }
    }
}
