using FeaturesPractice.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeaturesPractice
{
    class Program
    {
        // Create some data that are of types that implement IEnumerable<T>
        // Create an extension method extending IEnumerable<T>
        // Use a Named Method, Anonymous Method, and finally a Lambda Expression to define filter criteria
        // Create a Func<> that cubes an integer; a Func<> that subtracts 2 integers; update the filtering criteria; add ordering
        // Use var to have cleaner-looking code
        static void Main(string[] args)
        {
            Func<int, int> cube = y => y * y * y;
            // Without a full body block
            // Func<int, int, int> subtract = (a, b) => a - b;

            // With a full body block
            Func<int, int, int> subtract = (a, b) =>
            {
                var result = a - b;
                return result;
            };

            Action<int> write = x => Console.WriteLine(x);

            // Without action
            //Console.WriteLine(cube(subtract(5, 3)));
            // With action
            write(cube(subtract(5, 3)));

            var brickAndMortarCustomers = new Customer[]
            {
                new Customer { Id = 1, FullName = "Joe Smith" },
                new Customer { Id = 2, FullName = "Maggie Brown" },
                new Customer { Id = 3, FullName = "Sammy Tegerson"}
            };

            var eCommCustomers = new List<Customer>
            {
                new Customer { Id = 4, FullName = "Bob Plank"}
            };

            // Named Method
            /*
            foreach (var customer in brickAndMortarCustomers.Where(FullNameStartsWithM))
            {
                Console.WriteLine(customer.FullName);
            }
            */

            // Anonymous Method
            /*
            foreach (var customer in brickAndMortarCustomers.Where(
                delegate (Customer customer)
                {
                    return customer.FullName.StartsWith("M");
                }))
            {
                Console.WriteLine(customer.FullName);
            }
            */

            // Lambda Expression
            /*
             * foreach (var customer in brickAndMortarCustomers.Where(c => c.FullName.StartsWith("M")))
             */
            // Change filtering and order

            var query = brickAndMortarCustomers.Where(c => c.FullName.Contains("a"))
                                                .OrderByDescending(c => c.FullName);
            foreach (var customer in query)
            {
                Console.WriteLine(customer.FullName);
            }
        }

        private static bool FullNameStartsWithM(Customer customer)
        {
            return customer.FullName.StartsWith("M");
        }
    }
}
