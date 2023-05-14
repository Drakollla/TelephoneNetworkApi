using Microsoft.EntityFrameworkCore;
using TelephoneNetworkApi.Domain.Models;

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
        public DbSet<RegistrySubscriptionPayment> RegistrySubscriptionPayments { get; set; }
        public DbSet<AutomaticTelephoneExchange> AutomaticTelephoneExchanges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Subscriber>(entity =>
            {
                entity.ToTable("Subscribers");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.SecondName).IsRequired().HasMaxLength(30);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(30);
                entity.Property(p => p.Surname).IsRequired().HasMaxLength(30);
                entity.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(15);
                entity.Property(p => p.IsIntercityOpen).HasDefaultValue(false);
                entity.HasMany(e => e.AtsSubscribers)
                      .WithOne(s => s.Subscriber)
                      .OnDelete(DeleteBehavior.SetNull); 
            });

            builder.Entity<RegistrySubscriptionPayment>(entity =>
            {
                entity.ToTable("RegistrySubscriptionPayment");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Mounth).IsRequired().HasDefaultValue(DateTime.Now.Month);
                entity.Property(e => e.Year).IsRequired().HasDefaultValue(DateTime.Now.Year);
                entity.Property(e => e.TownshipMinuteCount).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.IntecityMinuteCount).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.Price).IsRequired();
                entity.HasOne(e => e.Subscriber)
                      .WithMany(s => s.RegistrySubscriptionPayments)
                      .HasForeignKey(e => e.SubscriberId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AutomaticTelephoneExchange>(entity =>
            {
                entity.ToTable("AutomaticTelephoneExchange");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(30);
                entity.Property(e => e.CountSubscriber).IsRequired();
                entity.HasMany(e => e.AtsSubscribers)
                      .WithOne(a => a.AutomaticTelephoneExchange)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            AddTestData(builder);
        }

        private void AddTestData(ModelBuilder builder)
        {
            builder.Entity<Subscriber>().HasData
            (
                new Subscriber { Id = 100, SecondName = "Пупкин", Name = "Василий", Surname = "Петрович", PhoneNumber = "23-56-78" },
                new Subscriber { Id = 101, SecondName = "Иванов", Name = "Иван", Surname = "Иванович", PhoneNumber = "13-56-78" }
            );

            builder.Entity<RegistrySubscriptionPayment>().HasData
            (
                new RegistrySubscriptionPayment { Id = 99, TownshipMinuteCount = 5, IntecityMinuteCount = 0, Price = 0.25m, SubscriberId = 101 },
                new RegistrySubscriptionPayment { Id = 100, TownshipMinuteCount = 7, IntecityMinuteCount = 0, Price = 0.35m, SubscriberId = 100 }
            );

            builder.Entity<AutomaticTelephoneExchange>().HasData
            (
                new AutomaticTelephoneExchange { Id = 78, Town = "Витебск", Name = "Телефонная сеть №15", CountSubscriber = 10000 }
            );

            builder.Entity<AtsSubscriber>().HasData
            (
                new AtsSubscriber { Id = 100, SubscriberId = 101, AutomaticTelephoneExchangeId = 78 }
            );
        }
    }
}