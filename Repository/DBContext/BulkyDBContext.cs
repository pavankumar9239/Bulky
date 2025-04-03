using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Models;

namespace Repository.DBContext
{
    public class BulkyDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BulkyDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public BulkyDBContext(DbContextOptions<BulkyDBContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
               new Category
                {
                    Id = 1,
                    Name = "Action",
                    DisplayOrder = 1
                },
                new Category
                {
                    Id = 2,
                    Name = "Science Fiction",
                    DisplayOrder = 2
                },
                new Category
                {
                    Id = 3,
                    Name = "Romance",
                    DisplayOrder = 3
                }
            });
        }
    }
}
