using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpProjectManager.models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for 1-to-Many relation
        public List<Employee> Employees { get; set; } = new();
    }
}
