using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Domain.Entities;

namespace Test.Domain.ViewModel
{
    public class EmployeeViewModel : Employee
    {
        public IFormFile File { get; set; }
    }
}
