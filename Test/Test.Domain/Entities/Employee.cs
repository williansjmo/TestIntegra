using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public DateTime HiringDate { get; set; }
    }
}
