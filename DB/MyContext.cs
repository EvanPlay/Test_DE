using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6.DB
{
    public class MyContext:DbContext
    {
        private string ConnectionString =
            "server=***; database=***; user=***; password=***;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
