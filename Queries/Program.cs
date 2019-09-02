using System;
using System.Data.Entity;
using System.Linq;

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

            // Projection
            var courseProjection = context.Courses
                    .Where(c => c.Id == 1)
                    .Select(c => new { Name = c.Author.Name, Title = c.Title });


            // Grouping
            var courseGroup = context.Courses
                .GroupBy(c => c.Level);

            // GroupBy the course level

            foreach (var group in courseGroup)
            {
                // Each group is produced a key
                Console.WriteLine($"key - {group.Key}");

                // Iterate over each item within the group
                foreach (var item in group)
                    Console.WriteLine($"course - {item.Title}");
            }

            // Joining

            // Inner Join (Course & Authors)
            //context.Courses.Join(context.Authors,
            //    c => c.AuthorId, 
            //    a => a.Id, 
            //    (course, author) => new
            //        {   CourseName = course.Title,
            //            AuthorName = author.Name
            //        }
            //    );



            #endregion


            #region Lazy Loading

            var LazyResults = context.Courses;

            // This is lazy loading and also an example of the N+1 problem within Lazy Loading in EF.
            foreach (var lazyCourse in LazyResults)
                Console.WriteLine($"Course name : {lazyCourse.Title} - Course Author :{lazyCourse.Author.Name}");

            #endregion

            #region Eager Loading

            var eagerLoading = context.Courses.Include("Author").ToList();

            foreach (var lazyCourse in LazyResults)
                Console.WriteLine($"Course name : {lazyCourse.Title} - Course Author :{lazyCourse.Author.Name}");

            // Adding Lambda definition instead (c => c)
            var eagerLoadingWithLambda = context.Courses
                                                .Include(c => c.Author)
                                                .ToList();

            #endregion

            #region Explicit loading

            var ExplicitLoading = context.Authors.Single(a => a.Id == 1);

            // The MSDN approach of Explicit loading (Entry, Collection, Load)
            context.Entry(ExplicitLoading).Collection(a => a.Courses).Load();


            #endregion

            #region Improved Searching Query

            var searchAuthors = context.Authors.ToList();
            var searchAuthorIds = searchAuthors.Select(a => a.Id);


            // We are adding a list within the where clause
            // Basically does this current course contain an id within this list.
            var searchCourses = context.Courses.Where(c => searchAuthorIds.Contains(c.AuthorId));


            #endregion

            Console.ReadLine();
        }
    }
}
