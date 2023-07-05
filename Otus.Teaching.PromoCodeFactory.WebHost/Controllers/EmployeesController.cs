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

            var mapper = EmployeeCoreToShortResponse.CreateMapper();

            var employeesModelList = mapper.Map<List<EmployeeShortResponse>>(employees);

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

            var mapper = EmployeeCoreToResponse.CreateMapper();

            var employeeModel = mapper.Map<Employee, EmployeeResponse>(employee);

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
            var mapper = EmployeeCreateToCore.CreateMapper();
            var employeeDataBase = mapper.Map<Employee>(employee);
            employeeDataBase.Id = id;     
            id = await _employeeRepository.PostAsync(employeeDataBase);
            return id;
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteEmployeeAsync(Guid id)
        {
            var isSucces = await _employeeRepository.DeleteAsync(id);
            if (isSucces) {
                return Ok();
            } else {
                return NotFound();
            }
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