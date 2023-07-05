using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Mappers;
using System;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class EmployeeCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }

    public static class EmployeeCreateToCore
    {
        public static Mapper CreateMapper()
        {
            return MapperFactory<EmployeeCreate, Employee>.Produce(
                (cfg) => cfg
                    .ForMember("Roles", (opt) => opt.MapFrom(
                        (c) => new List<Role>()))
                    .ForMember("AppliedPromocodesCount", (opt) => opt.MapFrom(
                        (c) => 0))
                    .ForMember("id", (opt) => opt.MapFrom(
                        (c) => Guid.Empty))
                );
        }
    }
}
