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

            Console.WriteLine(brickAndMortarCustomers.CountMinusOne());
            IEnumerator<Customer> enumerator = brickAndMortarCustomers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Id);
            }
        }
    }
}
