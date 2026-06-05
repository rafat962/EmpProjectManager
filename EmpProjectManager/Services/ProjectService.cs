using System;
using System.Linq;
using EmpProjectManager.Data;
using EmpProjectManager.models;
using Microsoft.EntityFrameworkCore;

public class ProjectService
{
    public void DisplayAll()
    {
        using var context = new AppDbContext();
        var projects = context.Projects.Include(p => p.Employees).ToList();

        if (!projects.Any())
        {
            Console.WriteLine("No projects found.");
            return;
        }

        Console.WriteLine("\n--- Project List ---");
        foreach (var proj in projects)
        {
            Console.WriteLine($"Project: {proj.Name}");
            foreach (var emp in proj.Employees)
            {
                Console.WriteLine($"  - Assigned Employee: {emp.Name}");
            }
            Console.WriteLine("-----------------------------------");
        }
    }

    public void Add(string name)
    {
        using var context = new AppDbContext();
        context.Projects.Add(new Project { Name = name });
        context.SaveChanges();
        Console.WriteLine("Project added successfully!");
    }

    public void Edit()
    {
        using var context = new AppDbContext();
        var projects = context.Projects.ToList();

        if (!projects.Any())
        {
            Console.WriteLine("No projects available to edit.");
            return;
        }

        Console.WriteLine("\nSelect a project to edit:");
        var menu = SelectionHelper.DisplayAndGetMenu(projects, p => p.Name, p => p.Id);

        Console.Write("Enter serial number: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || !menu.ContainsKey(choice))
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        int actualId = menu[choice];
        var project = context.Projects.Find(actualId);

        if (project != null)
        {
            Console.WriteLine($"\nCurrent Name: {project.Name}");
            Console.Write("Enter new name (or press Enter to keep current): ");
            string newName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newName))
            {
                project.Name = newName;
                context.SaveChanges();
                Console.WriteLine("Project updated successfully!");
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
        var projects = context.Projects.ToList();

        if (!projects.Any())
        {
            Console.WriteLine("No projects available to delete.");
            return;
        }

        Console.WriteLine("\nSelect a project to delete:");
        var menu = SelectionHelper.DisplayAndGetMenu(projects, p => p.Name, p => p.Id);

        Console.Write("Enter serial number: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || !menu.ContainsKey(choice))
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        int actualId = menu[choice];
        var project = context.Projects.Find(actualId);

        if (project != null)
        {
            context.Projects.Remove(project);
            context.SaveChanges();
            Console.WriteLine("Project deleted successfully!");
        }
    }
}