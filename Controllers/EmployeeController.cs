using AutoMapper;
using CorporateTraningSystm.Model;
using CorporateTraningSystm.Services;
using CorporateTraningSystm.View;
using Microsoft.AspNetCore.Mvc;

namespace CorporateTraningSystm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeApiController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            var employeeViewModels = _mapper.Map<IEnumerable<EmployeeView>>(employees);
            return Ok(employeeViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeViewModel = _mapper.Map<EmployeeView>(employee);
            return Ok(employeeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeView employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeViewModel);
                await _employeeService.AddEmployeeAsync(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, employee);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeView employeeViewModel)
        {
            if (id != employeeViewModel.EmployeeId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeViewModel);
                await _employeeService.UpdateEmployeeAsync(employee);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
