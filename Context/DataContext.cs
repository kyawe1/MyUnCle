using Microsoft.EntityFrameworkCore;
using UncleApp.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace UncleApp.Context
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {

        

        public DataContext(DbContextOptions<DataContext> options) : base(options){
            
        }
                protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasOne(p => p.Customer);
            modelBuilder.Entity<OrderItem>().HasOne(p => p.Order).WithMany(p => p.Items);
            modelBuilder.Entity<OrderItem>().HasOne(p => p.Type);
            modelBuilder.Entity<DumblingType>().HasIndex(p => p.Name).IsUnique(true);
            modelBuilder.Entity<Customer>().HasIndex(p => p.Fbname).IsUnique(true);
            modelBuilder.Entity<Customer>().HasMany(p => p.address).WithOne(p => p.customer);
            modelBuilder.Entity<Address>().Property(p => p.Address_String).HasColumnName("address");
            base.OnModelCreating(modelBuilder);
            string admin_id = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[]{
                new IdentityRole(){
                    Id=admin_id,
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole(){
                    Name="Shopkeeper",
                    NormalizedName="SHOPKEEPER"
                }
            });
            var hasher = new PasswordHasher<IdentityUser>();
            var user_id= Guid.NewGuid().ToString();
            var user = new IdentityUser()
            {
                Id=user_id,
                UserName = "kyawe225@gmail.com",
                NormalizedUserName = "KYAWE225@GMAIL.COM",
                NormalizedEmail = "KYAWE225@GMAIL.COM",
                Email = "kyawe225@gmail.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            user.PasswordHash=hasher.HashPassword(user,"123456");
            modelBuilder.Entity<IdentityUser>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = admin_id,
                UserId = user_id
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
