using Microsoft.EntityFrameworkCore;
using UncleApp.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace UncleApp.Context 
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DataContext(){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseNpgsql(@"Host=localhost;Port=5432;Username=postgres;Password=kyaw;database=uncleapp");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasOne(p=>p.Customer);
            modelBuilder.Entity<OrderItem>().HasOne(p => p.Order).WithMany(p=>p.Items);
            modelBuilder.Entity<OrderItem>().HasOne(p => p.Type);
            modelBuilder.Entity<DumblingType>().HasIndex(p => p.Name).IsUnique(true);
            modelBuilder.Entity<Customer>().HasIndex(p => p.Fbname).IsUnique(true);
            modelBuilder.Entity<Customer>().HasMany(p=>p.address).WithOne(p=>p.customer);
            modelBuilder.Entity<Address>().Property(p => p.Address_String).HasColumnName("address");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[]{
                new IdentityRole(){
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole(){
                    Name="Shopkeeper",
                    NormalizedName="SHOPKEEPER"
                }
            });
            
        }
        public DbSet<Order> orders { set; get; }
        public DbSet<OrderItem> orderItems { set; get; }
        public DbSet<Customer> customers { set; get; }
        public DbSet<DumblingType> dumblingTypes { set; get; }
        public DbSet<Address> addresses { set; get; }
        public DbSet<VerifyWaitingList> waitingLists { set; get; }

    }
}
