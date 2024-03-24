using System.ComponentModel.DataAnnotations;

namespace TechBlogApp.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public DateTime Publication_date { get; set; }
    }
}
