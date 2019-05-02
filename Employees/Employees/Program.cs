using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    class Program
    {
        static void Main(string[] args)
        {
            var employees = new[] 
            {
                new { Name = "Andras", Salary = 420},
                new { Name = "Bela", Salary = 400},
                new { Name = "Csaba", Salary = 250},
                new { Name = "David", Salary = 300},
                new { Name = "Endre", Salary = 620},
                new { Name = "Ferenc", Salary = 350},
                new { Name = "Gabor", Salary = 410},
                new { Name = "Hunor", Salary = 500},
                new { Name = "Imre", Salary = 900},
                new { Name = "Janos", Salary = 600},
                new { Name = "Karoly", Salary = 700},
                new { Name = "Laszlo", Salary = 400},
                new { Name = "Marton", Salary = 500}
            };

            //Display the name of the employee who earn the most
            var q1 = employees.OrderByDescending(e => e.Salary).First().Name;

            //Display the name of the employees who earn less than the company average.
            var q2 = employees.Where(e => e.Salary < employees.Average(s => s.Salary)).Select(e => e.Name);

            //Sort the employees by their salaries in an ascending order
            var q3 = employees.OrderBy(e => e.Salary);

            //Display the name of employees who earn the same amount and sort the result by salaries then names in an ascending order
            var q4 = employees.Where(e => employees.Count(emp => emp.Salary == e.Salary) > 1).OrderBy(o => o.Salary).ThenBy(t => t.Name).Select(x => x.Name + " - " + x.Salary);

            //Group the employees in the following salary ranges: 200-399, 400-599, 600-799, 800-999
            int[] range = new int[] { 199, 399, 599, 799, 999 };
            var q5 = from employee in employees
                       group employee by range.First(item => item > employee.Salary) into g
                       orderby g.Key
                       select new { Range = g.Key, Details = g.ToList() };

            Console.ReadKey();
        }
    }
}
