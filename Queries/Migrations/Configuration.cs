namespace Queries.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Queries.QueriesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Queries.QueriesContext context)
        {
            #region Tags

            var tags = new Dictionary<string, Tag>
            {
                {"C#", new Tag{ Id = 1, Name = "C#"}},
                {"JS", new Tag{ Id = 2, Name = "JS"}},
                {"Node", new Tag{ Id = 3, Name = "Node"}}
            };

            foreach (var tag in tags.Values)
                context.Tags.AddOrUpdate(t => t.Id, tag);

            #endregion


            #region Authors

            var authors = new List<Author>()
            {
                new Author{ Id = 1, Name ="Patrick" },
                new Author{ Id = 2, Name ="Tim", Courses = new List<Course>() }
            };

            foreach (var author in authors)
                context.Authors.AddOrUpdate(a => a.Id, author);

            #endregion

            #region Courses

            var courses = new List<Course>()
            {
                new Course
                {
                    Id = 1,
                    Title = "A books title1",
                    Author = authors[0],
                    DatePublished = DateTime.Today.AddYears(-5),
                    Tags = new List<Tag>
                    {
                        tags["C#"]
                    }
                }
            };

            foreach (var course in courses)
                context.Courses.AddOrUpdate(c => c.Id, course);

            #endregion
        }
    }
}
