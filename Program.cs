using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Department { get; set; } = "";
    public string JobTitle { get; set; } = "";
    public decimal MonthlySalary { get; set; }
    public double TaxRate { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; } = "";
}

class Program
{
    static List<Employee> employees = new List<Employee>();
    static int nextId = 1001;

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Employee Payroll & HR Management System - MAIN B===");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View Employees");
            Console.WriteLine("3. Search/Filter Employees");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Exit");
            Console.Write("\nSelect an option: ");

            string choice = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddEmp();
                    break;

                case "2":
                    DisplayEmps();
                    break;

                case "3":
                    SearchFilterEmp();
                    break;

                case "4":
                    UpdateEmp();
                    break;

                case "5":
                    DeleteEmp();
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        Console.WriteLine("Program ended.");
    }

    // CREATE
    static void AddEmp()
    {
        Console.WriteLine("--- Add New Employee ---");

        Console.Write("Enter First Name: ");
        string fname = Console.ReadLine() ?? "";

        Console.Write("Enter Last Name: ");
        string lname = Console.ReadLine() ?? "";

        Console.Write("Enter Department: ");
        string dept = Console.ReadLine() ?? "";

        Console.Write("Enter Job Title: ");
        string jtitle = Console.ReadLine() ?? "";

        Console.Write("Enter Monthly Salary: ");
        decimal.TryParse(Console.ReadLine(), out decimal salary);

        Console.Write("Enter Tax Rate (e.g. 0.12 for 12%): ");
        double.TryParse(Console.ReadLine(), out double tax);

        Console.Write("Enter Hire Date (yyyy-MM-dd): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime hireDate);

        Console.Write("Enter Is Active (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool active);

        Console.Write("Enter Email: ");
        string email = Console.ReadLine() ?? "";

        Employee newEmployee = new Employee
        {
            Id = nextId++,
            FirstName = fname,
            LastName = lname,
            Department = dept,
            JobTitle = jtitle,
            MonthlySalary = salary,
            TaxRate = tax,
            HireDate = hireDate,
            IsActive = active,
            Email = email
        };

        employees.Add(newEmployee);

        Console.WriteLine("\nEmployee added successfully!");
        Console.WriteLine($"Employee ID: {newEmployee.Id}");
    }

    // READ
    static void DisplayEmps()
    {
        Console.WriteLine("--- Current Employee Records ---");

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine(
            $"{"ID",-5} {"Name",-22} {"Department",-15} {"Job Title",-20} {"Salary",12} {"Tax",8} {"Hire Date",-12} {"Active",-8} {"Email",-30}"
        );

        Console.WriteLine(new string('-', 145));

        foreach (Employee employee in employees)
        {
            string fullName = employee.FirstName + " " + employee.LastName;

            Console.WriteLine(
                $"{employee.Id,-5} " +
                $"{fullName,-22} " +
                $"{employee.Department,-15} " +
                $"{employee.JobTitle,-20} " +
                $"{employee.MonthlySalary,12:C} " +
                $"{employee.TaxRate,7:P0} " +
                $"{employee.HireDate,-12:yyyy-MM-dd} " +
                $"{employee.IsActive,-8} " +
                $"{employee.Email,-30}"
            );
        }
    }

    // SEARCH / FILTER
    static void SearchFilterEmp()
    {
        Console.WriteLine("--- Search / Filter Employees ---");
        Console.WriteLine("1. Search by First Name");
        Console.WriteLine("2. Search by Department");
        Console.WriteLine("3. Search by Job Title");
        Console.WriteLine("4. Show Active Employees");
        Console.WriteLine("5. Show Inactive Employees");

        Console.Write("\nSelect an option: ");
        string choice = Console.ReadLine() ?? "";

        IEnumerable<Employee> results;

        switch (choice)
        {
            case "1":
                Console.Write("Enter first name: ");
                string firstName = Console.ReadLine() ?? "";

                results = employees.Where(e =>
                    e.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase));
                break;

            case "2":
                Console.Write("Enter department: ");
                string department = Console.ReadLine() ?? "";

                results = employees.Where(e =>
                    e.Department.Contains(department, StringComparison.OrdinalIgnoreCase));
                break;

            case "3":
                Console.Write("Enter job title: ");
                string jobTitle = Console.ReadLine() ?? "";

                results = employees.Where(e =>
                    e.JobTitle.Contains(jobTitle, StringComparison.OrdinalIgnoreCase));
                break;

            case "4":
                results = employees.Where(e => e.IsActive);
                break;

            case "5":
                results = employees.Where(e => !e.IsActive);
                break;

            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        Console.WriteLine("\n--- Search Results ---");

        if (!results.Any())
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine(
            $"{"ID",-5} {"Name",-22} {"Department",-15} {"Job Title",-20} {"Salary",12} {"Tax",8} {"Hire Date",-12} {"Active",-8} {"Email",-30}"
        );

        Console.WriteLine(new string('-', 145));

        foreach (Employee employee in results)
        {
            string fullName = employee.FirstName + " " + employee.LastName;

            Console.WriteLine(
                $"{employee.Id,-5} " +
                $"{fullName,-22} " +
                $"{employee.Department,-15} " +
                $"{employee.JobTitle,-20} " +
                $"{employee.MonthlySalary,12:C} " +
                $"{employee.TaxRate,7:P0} " +
                $"{employee.HireDate,-12:yyyy-MM-dd} " +
                $"{employee.IsActive,-8} " +
                $"{employee.Email,-30}"
            );
        }
    }

    // UPDATE
    static void UpdateEmp()
    {
        DisplayEmps();

        if (employees.Count == 0)
            return;

        Console.Write("\nEnter the ID of the employee to update: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Employee employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee != null)
            {
                Console.Write($"Enter new first name (current: {employee.FirstName}): ");
                string newFirstName = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(newFirstName))
                    employee.FirstName = newFirstName;

                Console.Write($"Enter new last name (current: {employee.LastName}): ");
                string newLastName = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(newLastName))
                    employee.LastName = newLastName;

                Console.Write($"Enter new department (current: {employee.Department}): ");
                string newDepartment = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(newDepartment))
                    employee.Department = newDepartment;

                Console.Write($"Enter new job title (current: {employee.JobTitle}): ");
                string newJobTitle = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(newJobTitle))
                    employee.JobTitle = newJobTitle;

                Console.Write($"Enter new monthly salary (current: {employee.MonthlySalary}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newSalary))
                    employee.MonthlySalary = newSalary;

                Console.Write($"Enter new tax rate (current: {employee.TaxRate}): ");
                if (double.TryParse(Console.ReadLine(), out double newTaxRate))
                    employee.TaxRate = newTaxRate;

                Console.Write($"Enter new hire date (current: {employee.HireDate:yyyy-MM-dd}): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newHireDate))
                    employee.HireDate = newHireDate;

                Console.Write($"Enter new active status (current: {employee.IsActive}): ");
                if (bool.TryParse(Console.ReadLine(), out bool newActive))
                    employee.IsActive = newActive;

                Console.Write($"Enter new email (current: {employee.Email}): ");
                string newEmail = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(newEmail))
                    employee.Email = newEmail;

                Console.WriteLine("\nEmployee updated successfully!");
            }
            else
            {
                Console.WriteLine("\nEmployee not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }

    // DELETE
    static void DeleteEmp()
    {
        DisplayEmps();

        if (employees.Count == 0)
            return;

        Console.Write("\nEnter the ID of the employee to delete: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Employee employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee != null)
            {
                employees.Remove(employee);

                Console.WriteLine(
                    $"\nEmployee {employee.FirstName} {employee.LastName} " +
                    $"(ID {id}) deleted successfully."
                );
            }
            else
            {
                Console.WriteLine($"\nNo employee with ID {id} was found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }
}