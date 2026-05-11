namespace Machine_Product_Service.DbContext;
using Machine_Product_Service.User;
using Machine_Product_Service.MachineProduct;
using Microsoft.EntityFrameworkCore;
public class DBcontext : DbContext
{
    DbSet<User>  Users { get; set; }
    DbSet<Machine> Machines { get; set; }

    public DBcontext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasMany(u => u.Products)
            .WithOne(p => p.user)
            .HasForeignKey(u => u.MachineId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Machine>()
            .Property(m => m.MachineModel)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Entity<Machine>()
            .Property(m => m.MachineName)
            .HasMaxLength(256)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(u => u.UserName)
            .HasMaxLength(256)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .HasMaxLength(256)
            .IsRequired();
            
        modelBuilder.Entity<Machine>()
            .Property(m => m.MachineRun)
            .HasMaxLength(256)
            .IsRequired();
        
        modelBuilder.Entity<Machine>()
            .Property(m => m.MachineDescription)
            .HasMaxLength(256)
            .IsRequired();

    }
}