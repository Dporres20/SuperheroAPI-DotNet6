using Microsoft.EntityFrameworkCore;

namespace SuperheroAPI.Data
{
    public class DataContext : DbContext
    {
        //constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Superhero> Superheroes {get; set;}
    }
}

// Conn String Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;