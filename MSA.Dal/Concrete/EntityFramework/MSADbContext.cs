using Microsoft.EntityFrameworkCore;
using MSA.Entities.Entities;

namespace MSA.Dal.Concrete.EntityFramework
{
    public class MSADbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           var connection = @"Server=(localdb)\MSSQLLocalDB; Database=MSADbContext; Integrated Security=true;  MultipleActiveResultSets=True";
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }

        DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Product 
            modelBuilder.Entity<Product>().Property(p => p.InProductId)
             .HasColumnName("InProductId");
            modelBuilder.Entity<Product>().Property(p => p.StName)
            .HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.StCode)
            .HasMaxLength(100);
            #endregion
        }
    }
}
