using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public class QueriesContext : DbContext
    {
        public QueriesContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Add Required attribute
            //modelBuilder.Entity<Course>()
            //    .Property(c => c.Description)
            //    .IsRequired();
        }
    }
}
