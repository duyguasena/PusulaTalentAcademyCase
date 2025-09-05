using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class FilterEmployees
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        var filteredEmployees = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate.Year > 2017)
            .ToList();

        var names = filteredEmployees
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .Select(e => e.Name)
            .ToList();

        var totalSalary = filteredEmployees.Sum(e => e.Salary);
        var count = filteredEmployees.Count();
        var averageSalary = count > 0 ? filteredEmployees.Average(e => e.Salary) : 0;
        var minSalary = count > 0 ? filteredEmployees.Min(e => e.Salary) : 0;
        var maxSalary = count > 0 ? filteredEmployees.Max(e => e.Salary) : 0;
        
        var result = new
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = Math.Round(averageSalary, 2),
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}