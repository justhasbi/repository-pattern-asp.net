using Microsoft.EntityFrameworkCore;
using NETCore.Models;

namespace NETCore.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Profiling> Profilings { get; set; }

        public DbSet<University> Universities { get; set; }

        // public DbSet<PersonVM> PersonVMs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // one to one between person and account
            modelBuilder.Entity<Person>()
                .HasOne(a => a.Account)
                .WithOne(p => p.Person)
                .HasForeignKey<Account>(p => p.NIK);

            // one to one between account and profiling
            modelBuilder.Entity<Account>()
                .HasOne(p => p.Profiling)
                .WithOne(a => a.Account);

            // one to many between education and profiling
            modelBuilder.Entity<Profiling>()
                .HasOne(e => e.Education)
                .WithMany(p => p.Profilings);

            // one to many between university and education
            modelBuilder.Entity<Education>()
                .HasOne(u => u.University)
                .WithMany(e => e.Educations);
        }
    }
}
