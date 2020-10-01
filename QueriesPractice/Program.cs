using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueriesPractice
{
    class Program
    {
        // Create a list of books and filter out books published after 1850
        // Create my own extension method for filtering
        // Use yield return to use deferred execution with custom filter method
        // See how deferred execution is happening
        // Avoiding multiple enumerations by not using deferred execution
        // Look at how exceptions work with deferred queries
        static void Main(string[] args)
        {
            var books = new List<Book>
            {
                new Book { Title = "Emma", Author = "Jane Austen", YearPublished = 1815 },
                new Book { Title = "Great Expectations", Author = "Charles Dickens", YearPublished = 1860 },
                new Book { Title = "The Count of Monte Cristo", Author = "Alexandre Dumas", YearPublished = 1844}
            };

            // method syntax
            /*
            var query = books.Where(b => b.YearPublished <= 1850);

            foreach (var item in query)
            {
                Console.WriteLine(item.Title);
            }

            Console.WriteLine("****");
            */

            // var queryWithMyCustomFilter = Enumerable.Empty<Book>();

            // trying out new method

            /*
            try
            {
                // Will catch the error because of immediate execution
                //queryWithMyCustomFilter = books.Filter(b => b.YearPublished <= 1850).ToList();

                // Will not catch the error because of deferred execution
                queryWithMyCustomFilter = books.Filter(b => b.YearPublished <= 1850);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            */

            var queryWithMyCustomFilter = books.Filter(b => b.YearPublished <= 1850).ToList();

            // Deferred execution causes this to be enumerated twice
            // queryWithMyCustomFilter = queryWithMyCustomFilter.Take(1);

            // Enumerating the IEnumerable here disables deferred execution so it only gets enumerated once
            // queryWithMyCustomFilter = queryWithMyCustomFilter.Take(1).ToList();

            Console.WriteLine(queryWithMyCustomFilter.Count());

            //foreach (var item in queryWithMyCustomFilter)
            //{
            //    Console.WriteLine(item.Title);
            //}

            // Use the enumerator to see how deferred execution happens
            var enumerator = queryWithMyCustomFilter.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }
}
