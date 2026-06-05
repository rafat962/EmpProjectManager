using System;
using System.Linq;
using EmpProjectManager.Data;
using EmpProjectManager.models;
using Microsoft.EntityFrameworkCore;

public class EmployeeService
{
    public void DisplayAll()
    {
        using var context = new AppDbContext();
        var employees = context.Employees
            .Include(e => e.Department)
            .Include(e => e.Projects)
            .ToList();

        if (!employees.Any())
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine("\n--- Employee List ---");
        foreach (var emp in employees)
        {
            string deptName = emp.Department != null ? emp.Department.Name : "No Department Assigned";
            string projects = emp.Projects.Any() ? string.Join(", ", emp.Projects.Select(p => p.Name)) : "No Projects Assigned";

            Console.WriteLine($"Employee: {emp.Name} | Department: {deptName} | Projects: {projects}");
        }
    }

    public void Add(string name)
    {
        using var context = new AppDbContext();
        context.Employees.Add(new Employee { Name = name });
        context.SaveChanges();
        Console.WriteLine("Employee added successfully!");
    }

    public void Edit()
    {
        using var context = new AppDbContext();
        var employees = context.Employees.ToList();

        if (!employees.Any())
        {
            Console.WriteLine("No employees available to edit.");
            return;
        }

        Console.WriteLine("\nSelect an employee to edit:");
        var empMenu = SelectionHelper.DisplayAndGetMenu(employees, e => e.Name, e => e.Id);

        Console.Write("Enter employee serial number: ");
        if (!int.TryParse(Console.ReadLine(), out int empChoice) || !empMenu.ContainsKey(empChoice))
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        int empId = empMenu[empChoice];
        var employee = context.Employees
            .Include(e => e.Department)
            .Include(e => e.Projects)
            .FirstOrDefault(e => e.Id == empId);

        if (employee == null) return;

        Console.WriteLine("\n--- Edit Employee Options ---");
        Console.WriteLine("[1] Edit Name");
        Console.WriteLine("[2] Assign / Change Department");
        Console.WriteLine("[3] Add to Project");
        Console.WriteLine("[4] Remove from Project");
        Console.Write("Select option: ");
        string subChoice = Console.ReadLine();

        switch (subChoice)
        {
            case "1":
                Console.WriteLine($"\nCurrent Name: {employee.Name}");
                Console.Write("Enter new name (or press Enter to keep current): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName)) employee.Name = newName;
                break;

            case "2":
                string currentDept = employee.Department != null ? employee.Department.Name : "None";
                Console.WriteLine($"\nCurrent Department: {currentDept}");

                var departments = context.Departments.ToList();
                if (!departments.Any())
                {
                    Console.WriteLine("No departments available. Create a department first.");
                    return;
                }

                Console.WriteLine("Select New Department:");
                var deptMenu = SelectionHelper.DisplayAndGetMenu(departments, d => d.Name, d => d.Id);
                Console.Write("Enter department serial number: ");
                if (int.TryParse(Console.ReadLine(), out int dChoice) && deptMenu.ContainsKey(dChoice))
                {
                    employee.DepartmentId = deptMenu[dChoice];
                }
                break;

            case "3":
                var allProjects = context.Projects.ToList();
                var availableProjects = allProjects.Where(p => !employee.Projects.Any(ep => ep.Id == p.Id)).ToList();

                if (!availableProjects.Any())
                {
                    Console.WriteLine("Employee is already assigned to all available projects, or no projects exist.");
                    return;
                }

                Console.WriteLine("\nSelect a project to add the employee to:");
                var projMenuAdd = SelectionHelper.DisplayAndGetMenu(availableProjects, p => p.Name, p => p.Id);
                Console.Write("Enter project serial number: ");
                if (int.TryParse(Console.ReadLine(), out int pChoiceAdd) && projMenuAdd.ContainsKey(pChoiceAdd))
                {
                    var projToAdd = context.Projects.Find(projMenuAdd[pChoiceAdd]);
                    if (projToAdd != null) employee.Projects.Add(projToAdd);
                }
                break;

            case "4":
                if (!employee.Projects.Any())
                {
                    Console.WriteLine("Employee is not assigned to any projects currently.");
                    return;
                }

                Console.WriteLine("\nSelect a project to remove the employee from:");
                var projMenuRemove = SelectionHelper.DisplayAndGetMenu(employee.Projects, p => p.Name, p => p.Id);
                Console.Write("Enter project serial number: ");
                if (int.TryParse(Console.ReadLine(), out int pChoiceRemove) && projMenuRemove.ContainsKey(pChoiceRemove))
                {
                    var projToRemove = employee.Projects.FirstOrDefault(p => p.Id == projMenuRemove[pChoiceRemove]);
                    if (projToRemove != null) employee.Projects.Remove(projToRemove);
                }
                break;

            default:
                Console.WriteLine("Invalid option. No changes made.");
                return;
        }

        context.SaveChanges();
        Console.WriteLine("Changes saved successfully!");
    }

    public void Delete()
    {
        using var context = new AppDbContext();
        var employees = context.Employees.ToList();

        if (!employees.Any())
        {
            Console.WriteLine("No employees available to delete.");
            return;
        }

        Console.WriteLine("\nSelect an employee to delete:");
        var menu = SelectionHelper.DisplayAndGetMenu(employees, e => e.Name, e => e.Id);

        Console.Write("Enter serial number: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || !menu.ContainsKey(choice))
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        int actualId = menu[choice];
        var employee = context.Employees.Find(actualId);

        if (employee != null)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();
            Console.WriteLine("Employee deleted successfully!");
        }
    }
}