using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Mappers;
using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class EmployeeShortResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }

    public class EmployeeCoreToShortResponse : BaseMapper<Employee, EmployeeShortResponse> { }
}