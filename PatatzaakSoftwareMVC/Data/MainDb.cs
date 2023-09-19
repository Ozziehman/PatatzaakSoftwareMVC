using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Models;

namespace PatatzaakSoftwareMVC.Data
{
    public class MainDb : DbContext
    {
        public DbSet<Item> item { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<Voucher> voucher { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderedItem> orderedItem { get; set; }


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



