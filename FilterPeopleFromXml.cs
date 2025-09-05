using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class FilterPeopleFromXml
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrEmpty(xmlData))
        {
            return "{\"Names\":[],\"TotalSalary\":0,\"AverageSalary\":0,\"MaxSalary\":0,\"Count\":0}";
        }
        
        var xmlDoc = XDocument.Parse(xmlData);
        var people = xmlDoc.Descendants("Person")
            .Select(p => new
            {
                Name = p.Element("Name")?.Value,
                Age = int.Parse(p.Element("Age")?.Value),
                Department = p.Element("Department")?.Value,
                Salary = decimal.Parse(p.Element("Salary")?.Value),
                HireDate = DateTime.Parse(p.Element("HireDate")?.Value)
            });

        var filteredPeople = people
            .Where(p => p.Age > 30)
            .Where(p => p.Department == "IT")
            .Where(p => p.Salary > 5000)
            .Where(p => p.HireDate.Year < 2019)
            .ToList();

        var names = filteredPeople.Select(p => p.Name).OrderBy(n => n).ToList();
        var totalSalary = filteredPeople.Sum(p => p.Salary);
        var averageSalary = filteredPeople.Any() ? filteredPeople.Average(p => p.Salary) : 0;
        var maxSalary = filteredPeople.Any() ? filteredPeople.Max(p => p.Salary) : 0;
        var count = filteredPeople.Count();

        var result = new
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = averageSalary,
            MaxSalary = maxSalary,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}