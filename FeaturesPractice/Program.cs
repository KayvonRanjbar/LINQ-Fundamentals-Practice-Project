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
        static void Main(string[] args)
        {
            IEnumerable<Customer> brickAndMortarCustomers = new Customer[]
            {
                new Customer { Id = 1, FullName = "Joe Smith" },
                new Customer { Id = 2, FullName = "Maggie Brown" },
                new Customer { Id = 3, FullName = "Sammy Tegerson"}
            };

            IEnumerable<Customer> eCommCustomers = new List<Customer>
            {
                new Customer { Id = 4, FullName = "Bob Plank"}
            };

            /*
            // Named Method
            foreach (var customer in brickAndMortarCustomers.Where(FullNameStartsWithM))
            {
                Console.WriteLine(customer.FullName);
            }
            */

            /*
            // Anonymous Method
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
            foreach (var customer in brickAndMortarCustomers.Where(c => c.FullName.StartsWith("M")))
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
