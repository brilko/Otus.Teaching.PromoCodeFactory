using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Mappers;
using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class RoleItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class RoleCoreToItemResponse : BaseMapper<Role, RoleItemResponse> { }
}