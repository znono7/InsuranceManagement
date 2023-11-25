using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;
namespace InsuranceManagement.API
{
    /// <summary>
    /// The database representational model for the application
    /// This class will handle database connections
    /// using Entity Framework
    /// </summary>
    public class EFDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<State> States { get; set; }


        /// <summary>
        /// EFDbContext constructor
        /// </summary>
        /// <param name="options">The database context options</param>
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasOne(m => m.Municipality)
                .WithMany(s => s.Users)
                .HasForeignKey(m => m.IdCommune)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ApplicationUser>()
                .HasOne(s => s.State)
                .WithMany(s => s.Users)
                .HasForeignKey(a => a.StateOfBirth)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Agency)
                .WithMany(s => s.Users)
                .HasForeignKey(a => a.IdAgency)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Municipality>()
                .HasOne(d => d.District)
                .WithMany(s => s.Municipalities)
                .HasForeignKey(d => d.IdDistrict)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<District>()
                .HasOne(d => d.State)
                .WithMany(s => s.Districts)
                .HasForeignKey(d => d.IdState)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Insurance>()
                .HasOne(d => d.Customer)
                .WithMany(s => s.Insurances)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
