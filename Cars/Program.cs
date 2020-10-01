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
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");
            foreach (var car in cars)
            {
                Console.WriteLine(car.Name);
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            // method syntax
            /*
            return
                File.ReadAllLines(path)
                    .Where(line => line.Length > 1)
                    .Skip(1)
                    .Select(Car.ParseFromCSV)
                    .ToList();
            */

            // query syntax
            var query =
                from line in File.ReadAllLines(path).Skip(1)
                where line.Length > 1
                select Car.ParseFromCSV(line);
            return query.ToList();
        }
    }
}
