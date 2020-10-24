using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class CarDatabase : DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}
