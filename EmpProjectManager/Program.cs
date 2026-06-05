using System;

var departmentService = new DepartmentService();
var employeeService = new EmployeeService();
var projectService = new ProjectService();

bool exit = false;

while (!exit)
{
    Console.Clear();
    Console.WriteLine("=====================================");
    Console.WriteLine("  Employee & Project Manager System  ");
    Console.WriteLine("=====================================");
    Console.WriteLine("[1] Department Management");
    Console.WriteLine("[2] Employee Management");
    Console.WriteLine("[3] Project Management");
    Console.WriteLine("[4] Exit");
    Console.WriteLine("=====================================");
    Console.Write("Select an option: ");

    string mainChoice = Console.ReadLine();

    switch (mainChoice)
    {
        case "1":
            HandleDepartmentMenu(departmentService);
            break;
        case "2":
            HandleEmployeeMenu(employeeService);
            break;
        case "3":
            HandleProjectMenu(projectService);
            break;
        case "4":
            exit = true;
            Console.WriteLine("\nThank you for using the system. Goodbye!");
            break;
        default:
            Console.WriteLine("\nInvalid option! Press any key to try again...");
            Console.ReadKey();
            break;
    }
}

static void HandleDepartmentMenu(DepartmentService deptService)
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        Console.WriteLine("--- Department Management ---");
        Console.WriteLine("[1] Display All Departments");
        Console.WriteLine("[2] Add New Department");
        Console.WriteLine("[3] Edit Department");
        Console.WriteLine("[4] Delete Department");
        Console.WriteLine("[5] Back to Main Menu");
        Console.Write("Select an option: ");

        string choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                deptService.DisplayAll();
                break;
            case "2":
                Console.Write("Enter Department Name: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    deptService.Add(name);
                }
                else
                {
                    Console.WriteLine("Department name cannot be empty.");
                }
                break;
            case "3":
                deptService.Edit(); // تم الربط بشكل كامل مع دالة التعديل
                break;
            case "4":
                deptService.Delete();
                break;
            case "5":
                back = true;
                continue;
            default:
                Console.WriteLine("Invalid option!");
                break;
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}

static void HandleEmployeeMenu(EmployeeService empService)
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        Console.WriteLine("--- Employee Management ---");
        Console.WriteLine("[1] Display All Employees");
        Console.WriteLine("[2] Add New Employee");
        Console.WriteLine("[3] Edit Employee (Data / Dept / Projects)");
        Console.WriteLine("[4] Delete Employee");
        Console.WriteLine("[5] Back to Main Menu");
        Console.Write("Select an option: ");

        string choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                empService.DisplayAll();
                break;
            case "2":
                Console.Write("Enter Employee Name: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    empService.Add(name);
                }
                else
                {
                    Console.WriteLine("Employee name cannot be empty.");
                }
                break;
            case "3":
                empService.Edit(); // هنا السحر كله! هيدخلك على قائمة التعديل والإسناد والمسح
                break;
            case "4":
                empService.Delete();
                break;
            case "5":
                back = true;
                continue;
            default:
                Console.WriteLine("Invalid option!");
                break;
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}

static void HandleProjectMenu(ProjectService projService)
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        Console.WriteLine("--- Project Management ---");
        Console.WriteLine("[1] Display All Projects");
        Console.WriteLine("[2] Add New Project");
        Console.WriteLine("[3] Edit Project");
        Console.WriteLine("[4] Delete Project");
        Console.WriteLine("[5] Back to Main Menu");
        Console.Write("Select an option: ");

        string choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                projService.DisplayAll();
                break;
            case "2":
                Console.Write("Enter Project Name: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    projService.Add(name);
                }
                else
                {
                    Console.WriteLine("Project name cannot be empty.");
                }
                break;
            case "3":
                projService.Edit(); // تم الربط بشكل كامل مع دالة التعديل
                break;
            case "4":
                projService.Delete();
                break;
            case "5":
                back = true;
                continue;
            default:
                Console.WriteLine("Invalid option!");
                break;
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}