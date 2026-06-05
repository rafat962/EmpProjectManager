using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpProjectManager.models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign Key
        public int? DepartmentId { get; set; } 
        public Department Department { get; set; }

        // Navigation property for Many-to-Many relation
        public List<Project> Projects { get; set; } = new();
    }
}
