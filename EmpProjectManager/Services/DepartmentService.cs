using System;
using System.Linq;
using EmpProjectManager.Data;
using EmpProjectManager.models;
using Microsoft.EntityFrameworkCore;

public class DepartmentService
{
    public void DisplayAll()
    {
        using var context = new AppDbContext();
        var departments = context.Departments.Include(d => d.Employees).ToList();

        if (!departments.Any())
        {
            Console.WriteLine("No departments found.");
            return;
        }

        Console.WriteLine("\n--- Department List ---");
        foreach (var dept in departments)
        {
            Console.WriteLine($"Department: {dept.Name} (Employees Count: {dept.Employees.Count})");
            foreach (var emp in dept.Employees)
            {
                Console.WriteLine($"  - Employee: {emp.Name}");
            }
            Console.WriteLine("-----------------------------------");
        }
    }

    public void Add(string name)
    {
        using var context = new AppDbContext();
        context.Departments.Add(new Department { Name = name });
        context.SaveChanges();
        Console.WriteLine("Department added successfully!");
    }

    public void Edit()
    {
        using var context = new AppDbContext();
        var departments = context.Departments.ToList();

        if (!departments.Any())
        {
            Console.WriteLine("No departments available to edit.");
            return;
        }

        Console.WriteLine("\nSelect a department to edit:");
        var menu = SelectionHelper.DisplayAndGetMenu(departments, d => d.Name, d => d.Id);

        Console.Write("Enter serial number: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || !menu.ContainsKey(choice))
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        int actualId = menu[choice];
        var department = context.Departments.Find(actualId);

        if (department != null)
        {
            Console.WriteLine($"\nCurrent Name: {department.Name}");
            Console.Write("Enter new name (or press Enter to keep current): ");
            string newName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newName))
            {
                department.Name = newName;
                context.SaveChanges();
                Console.WriteLine("Department updated successfully!");
            }
            else
            {
                Console.WriteLine("No changes made.");
            }
        }
    }

    public void Delete()
    {
        using var context = new AppDbContext();
        var departments = context.Departments.ToList();

        if (!departments.Any())
        {
            Console.WriteLine("No departments available to delete.");
            return;
        }

        Console.WriteLine("\nSelect a department to delete:");
        var menu = SelectionHelper.DisplayAndGetMenu(departments, d => d.Name, d => d.Id);

        Console.Write("Enter serial number: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || !menu.ContainsKey(choice))
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        int actualId = menu[choice];
        var department = context.Departments.Include(d => d.Employees).FirstOrDefault(d => d.Id == actualId);

        if (department != null)
        {
            foreach (var emp in department.Employees)
            {
                emp.DepartmentId = null;
            }

            context.Departments.Remove(department);
            context.SaveChanges();
            Console.WriteLine("Department deleted successfully!");
        }
    }
}