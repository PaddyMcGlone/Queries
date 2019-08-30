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
            #region Linq Syntax queries

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


            // An example of an inner join

            // Below we use the navigational property to retrieve our data.
            var InnerJoinQuery =
                from c in context.Courses
                select new { c.Author.Name };

            // Adding the group count method
            var groupJoin =
                from a in context.Authors
                join c in context.Courses on a.Id equals c.AuthorId into g
                select new { Name = a.Name, count = g.Count() };

            // into 'g' then makes this join a group join

            #endregion

            #region Linq extension methods

            // Restriction query
            var course = context.Courses.Where(c => c.Id == 1);

            // Ordering - note the use of 'thenby'
            var courseOrdered = context.Courses
                .Where(c => c.Id == 1)
                .OrderBy(c => c.Title)
                .ThenBy(c => c.Description);

            #endregion

            Console.ReadLine();
        }
    }
}
