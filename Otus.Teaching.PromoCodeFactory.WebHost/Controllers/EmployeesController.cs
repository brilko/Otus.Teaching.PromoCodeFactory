using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController
        : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x => 
                new EmployeeShortResponse()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FullName = x.FullName,
                    }).ToList();

            return employeesModelList;
        }
        
        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();
            
            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        /// <summary>
        /// Создать сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> PostEmployeeAsync(EmployeeCreate employee)
        {
            return await CreateEmployeeWithGuid(employee, Guid.NewGuid());
        }

        private async Task<Guid> CreateEmployeeWithGuid(EmployeeCreate employee, Guid id) {
            var employeeDataBase = new Employee() {
                Id = id,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Roles = new List<Role>(),
                AppliedPromocodesCount = 0
            };
            id = await _employeeRepository.PostAsync(employeeDataBase);
            return id;
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteEmployeeAsync(Guid id)
        {
            var isSucces = await _employeeRepository.DeleteAsync(id);
            return isSucces;
        }

        /// <summary>
        /// Изменить данные сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployee(Guid id, EmployeeCreate employee) {
            var isExist = await _employeeRepository.IsExistAsync(id);
            if(!isExist) 
                return NotFound();
            var entity = await _employeeRepository.GetByIdAsync(id);
            entity.Email = employee.Email;
            entity.FirstName = employee.FirstName;
            entity.LastName = employee.LastName;
            await _employeeRepository.UpdateAsync(entity);
            return await GetEmployeeByIdAsync(id);
        }
    }
}