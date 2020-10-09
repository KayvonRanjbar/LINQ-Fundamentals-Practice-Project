using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        // Implement a file processor to transform the csv file into a list of cars in memory
        // Find the most fuel efficient cars
        // Filter with Where and Last (and LastOrDefault)
        // Project data with a custom extension method and select
        // Flatten data (down to the char level!) with SelectMany
        // Add a manufacturer csv and class
        // Join data using query syntax
        // Join data with extension method syntax
        // Create a join with a composite key
        // Group data by car manufacturer
        // Use groupjoin!
        // Try to find the top 5 cars by country
        // Aggregate data on manufacturers
        static void Main(string[] args)
        {
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query =
                from car in cars
                group car by car.Manufacturer into carGroups
                select new
                {
                    Name = carGroups.Key,
                    Max = carGroups.Max(c => c.Highway),
                    Min = carGroups.Min(c => c.Highway),
                    Avg = carGroups.Average(c => c.Highway)
                } into result
                orderby result.Avg descending
                select result;

            var query2 =
                manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
                    (m, g) =>
                        new
                        {
                            Manufacturer = m,
                            Cars = g
                        })
                .GroupBy(g => g.Manufacturer.Headquarters);

            foreach (var result in query)
            {
                Console.WriteLine(result.Name);
                Console.WriteLine($"\t Max: {result.Max}");
                Console.WriteLine($"\t Min: {result.Min}");
                Console.WriteLine($"\t Avg: {result.Avg}");
            }
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');
                        return new Manufacturer
                        {
                            Name = columns[0],
                            Headquarters = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    });

            return query.ToList();
        }

        private static List<Car> ProcessCars(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCar();

            return query.ToList();
        }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');
                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
