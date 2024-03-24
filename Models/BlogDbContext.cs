using Microsoft.EntityFrameworkCore;

namespace TechBlogApp.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BlogPost>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost { Id = 1, Title = "First Post", Author = "Ravi Kishore",Summary = "New Blog", Publication_date = DateTime.Now },
            new BlogPost { Id = 2, Title = "Second Post", Author = "Ravi Kishore", Summary = "2nd Blog", Publication_date = DateTime.Now }
                );
        }
    }
}
