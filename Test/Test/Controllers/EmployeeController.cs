using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Domain.Entities;
using Test.Domain.Interfaces;
using Test.Domain.Services;
using Test.Domain.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IGenericService<Employee> generic;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeController(IWebHostEnvironment webHostEnvironment, EmployeeService candidateService)
        {
            generic = candidateService;
            this.webHostEnvironment = webHostEnvironment;
            
        }

        //POST: api/values
        [HttpPost("{search}")]
        public async Task<IActionResult> Search(string search)
        {
            if (string.IsNullOrEmpty(search))
                return BadRequest();
            return Ok(await generic.Search(search));
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Employee> Get() => ((EmployeeService)generic).GetAllEmpAsync();

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var gg = GetFullPathOfFile();
            if (string.IsNullOrEmpty(id.ToString()))
                return BadRequest();

            return Ok(await generic.GetAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] EmployeeViewModel employee)
        {
            if (employee.File == null)
                ModelState.AddModelError("File","Debe seleccionar una foto del empleado.");

            employee.Id = Guid.NewGuid();
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                var emp = ((EmployeeService)generic).GetEmployee(employee, GetFullPathOfFile());
                return Ok(await generic.AddAsync(emp));
            }
            return  BadRequest(ModelState);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] EmployeeViewModel employee)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                return BadRequest();

            if (ModelState.IsValid)
            {
                employee.Id = Guid.Parse(id.ToString());
                var emp = ((EmployeeService)generic).GetEmployee(employee, GetFullPathOfFile());
                return Ok(await generic.UpdateAsync(emp));
            }

            return BadRequest(ModelState);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                return BadRequest();

            return Ok(await generic.DeleteAsync(Guid.Parse(id.ToString())));
        }

        private string GetFullPathOfFile()
        {
            return $@"{webHostEnvironment.ContentRootPath}/Upload/PhotoEmployee";
        }
    }
}
