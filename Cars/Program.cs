using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        // Implement a file processor to transform the csv file into a list of cars in memory
        // Find the most fuel efficient cars
        // Filter with Where and Last (and LastOrDefault)
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");

            var query =
                from car in cars
                where car.Manufacturer == "Porsche" && car.Year == 2020
                orderby car.Combined descending, car.Highway descending
                select car;

            var result = cars.Contains(null);

            Console.WriteLine(result);

            foreach (var car in query.Take(15))
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined} : {car.Highway}");
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            var query =
                from line in File.ReadAllLines(path).Skip(1)
                where line.Length > 1
                select Car.ParseFromCSV(line);
            return query.ToList();
        }
    }
}
