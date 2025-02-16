using EFCoreModelApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreModelApp;

class Program
{
    static void Main(string[] args)
    {
        using (HRDbContext context = new())
        {
            List<Employee> allEmployees = context.Employees.Include(e => e.Department).ToList();
            foreach (Employee employee in allEmployees)
            {
                Console.WriteLine(
                    $"{employee.FirstName} {employee.LastName} works in the {employee.Department.Name} department.");
            }
            
            List<Employee> hrEmployees = context.Employees
                .Include(e => e.Department)
                .Where(e => e.Department.Name == "HR")
                .ToList();
            Console.WriteLine("HR Department Employees:");
            foreach (Employee employee in hrEmployees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName}");
            }
            
            Employee newEmployee = new Employee
            {
                FirstName = "Sally",
                LastName = "Jones",
                HireDate = DateTime.Now,
                DepartmentID = 2
            };
            context.Employees.Add(newEmployee);
            context.SaveChanges();
            
            Console.WriteLine("All Employees:");
            foreach (Employee employee in context.Employees.Include(e => e.Department))
            {
                Console.WriteLine(
                    $"{employee.FirstName} {employee.LastName} works in the {employee.Department.Name} department.");
            }
        }
    }
}