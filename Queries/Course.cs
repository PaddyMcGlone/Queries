using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public class Course
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        //[Required]
        public string Description { get; set; }

        public CourseLevel Level { get; set; }

        public float FullPrice { get; set; }

        [ForeignKey(nameof(Author))] // Identify the name of annotation
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public IList<Tag> Tags { get; set; }

        public DateTime DatePublished { get; set; }
    }
}
