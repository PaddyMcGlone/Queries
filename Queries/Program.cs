using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new QueriesContext();

            var query = from c in context.Courses
                        where c.DatePublished < DateTime.Today
                        orderby c.Title
                        select c;

            foreach (var result in query)
                Console.WriteLine(result.Title);

            var projectionQuery = from c in context.Courses
                                  where c.DatePublished < DateTime.Today
                                  select new { NewName = c.Title, DateOfQuery = c.DatePublished };

            Console.ReadLine();
        }
    }
}
