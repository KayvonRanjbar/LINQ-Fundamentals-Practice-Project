using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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
        // Efficiently aggregate with the aggregate extension method
        // Build an xml file from fuel.csv
        // Use functional construction of the xml for less code - more declarative, less imperative
        // Load and query the xml for just the car names of Toyota
        // Work with xml namespaces
        // Set up Entity Framework
        // Insert data into a db
        // Write a basic query with LINQ

        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDatabase>());
            InsertData();
            QueryData();
        }

        private static void QueryData()
        {
            var db = new CarDatabase();
            db.Database.Log = Console.WriteLine;

            // Get all cars and order the cars by highway fuel efficiency
            var query = db.Cars.OrderByDescending(c => c.Highway)
                               .ThenBy(c => c.Name)
                               .Take(5);

            foreach (var car in query)
            {
                Console.WriteLine($"{car.Name}: {car.Highway}");
            }
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDatabase();
            db.Database.Log = Console.WriteLine;

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static void QueryXml()
        {
            var ns = (XNamespace)"dummyNamespace";
            var ex = (XNamespace)"dummyExtendedNamespace";
            var document = XDocument.Load("fuel.xml");
            var query = document.Element(ns + "Cars").Elements(ex + "Car").Where(e => e.Attribute("Manufacturer").Value == "Toyota")
                                                                .Select(e => e.Attribute("Name").Value);

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXml()
        {
            var records = ProcessCars("fuel.csv");

            var ns = (XNamespace)"dummyNamespace";
            var ex = (XNamespace)"dummyExtendedNamespace";
            var document = new XDocument();
            var cars = new XElement(ns + "Cars",
                from record in records
                select new XElement(ex + "Car",
                                new XAttribute("Name", record.Name),
                                new XAttribute("Combined", record.Combined),
                                new XAttribute("Manufacturer", record.Manufacturer))
            );

            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));
            document.Add(cars);
            document.Save("fuel.xml");
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

    public class CarStatistics
    {
        public CarStatistics()
        {
            Max = int.MinValue;
            Min = int.MaxValue;
        }

        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }

        public CarStatistics Accumulate(Car car)
        {
            Total += car.Highway;
            Count++;
            Max = Math.Max(Max, car.Highway);
            Min = Math.Min(Min, car.Highway);

            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;

            return this;
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
