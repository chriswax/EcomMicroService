using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ProductWebApi.Models;

namespace ProductWebApi
{
  
        public class ProductDbContext : DbContext
        {
        public DbSet<Product> products { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions) : base(dbContextOptions) 
            {
                try
                {
                    var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                    if (databaseCreator != null)
                    {
                        //create Database if cannot connect
                        if (!databaseCreator.CanConnect()) databaseCreator.Create();
                        //create Tables if no table exist
                        if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                    }
                }
                catch(Exception ex) { 
                    Console.WriteLine(ex.Message);
                }
            }
        
    }

       
    
}
