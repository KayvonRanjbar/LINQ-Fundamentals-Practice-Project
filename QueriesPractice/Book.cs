using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueriesPractice
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        int _yearPublished;
        public int YearPublished
        {
            get
            {
                //throw new Exception("I wanted an exception!");
                Console.WriteLine($"Returned {_yearPublished} for {Title}");
                return _yearPublished;
            }
            set
            {
                _yearPublished = value;
            }
        }

        private string GetDebuggerDisplay()
        {
            return $"{Title}: {YearPublished}";
        }
    }
}
