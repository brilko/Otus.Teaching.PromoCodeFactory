using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Mappers;
using System;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class EmployeeResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public List<RoleItemResponse> Roles { get; set; }

        public int AppliedPromocodesCount { get; set; }
    }

    public static class EmployeeCoreToResponse
    {
        public static Mapper CreateMapper()
        {
            var roleMapper = RoleCoreToItemResponse.CreateMapper();
            var mapper = MapperFactory<Employee, EmployeeResponse>.Produce(
               (cfg) => cfg.ForMember("Roles", opt => opt.MapFrom(
                   (c) => roleMapper.Map<List<RoleItemResponse>>(c.Roles)
                ))
            );
            return mapper;
        }
    }
}