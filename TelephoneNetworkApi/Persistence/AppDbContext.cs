using Microsoft.EntityFrameworkCore;
using TelephoneNetworkApi.Models;

namespace TelephoneNetworkApi.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Subscriber>().ToTable("Subscribers");
            builder.Entity<Subscriber>().HasKey(p => p.Id);
            builder.Entity<Subscriber>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Subscriber>().Property(p => p.SecondName).IsRequired().HasMaxLength(30);
            builder.Entity<Subscriber>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Subscriber>().Property(p => p.Surname).IsRequired().HasMaxLength(30);
            builder.Entity<Subscriber>().Property(p => p.IsIntercityOpen).HasDefaultValue(false);
            builder.Entity<Subscriber>().Property(p => p.HasBenefit).HasDefaultValue(false);

            builder.Entity<Subscriber>().HasData
            (
                new Subscriber { Id = 100, SecondName = "Пупкин", Name = "Василий", Surname = "Петрович", PhoneNumber = "23-56-78" },
                new Subscriber { Id = 101, SecondName = "Иванов", Name = "Иван", Surname = "Иванович", PhoneNumber = "13-56-78" }
            );
        }
    }
}
