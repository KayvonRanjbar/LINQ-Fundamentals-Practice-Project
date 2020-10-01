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
        // Distinguish between streaming and non-streaming operators
        static void Main(string[] args)
        {
            var books = new List<Book>
            {
                new Book { Title = "Emma", Author = "Jane Austen", YearPublished = 1815 },
                new Book { Title = "Great Expectations", Author = "Charles Dickens", YearPublished = 1860 },
                new Book { Title = "The Count of Monte Cristo", Author = "Alexandre Dumas", YearPublished = 1844}
            };

            var queryWithMyCustomFilter = books.Filter(b => b.YearPublished <= 1850)
                                                .OrderBy(b => b.YearPublished);

            var enumerator = queryWithMyCustomFilter.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }
}
