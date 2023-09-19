using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Models;

namespace PatatzaakSoftwareMVC.Data
{
    public class MainDb : DbContext
    {
        public DbSet<Item> items { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Voucher> vouchers { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderedItem> orderedItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //Choose whether you are working on Oscar laptop or PC comment out the other one
            string connection = configuration.GetConnectionString("MainDbConnectionPC");
            //string connection = configuration.GetConnectionString("MainDbConnectionLaptop");
            optionsBuilder.UseSqlServer(connection);
        }
    }
}



