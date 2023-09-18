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
            //PC
            string connection = "Server=localhost\\MSSQLSERVER02;Database=master;Trusted_Connection=True;TrustServerCertificate=Yes";
            //_______
            //Laptop
            //string connection = "Data Source=LAPTOPOSCARTHEE\\MSSQLSERVER02;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=Yes";
            //_______
            optionsBuilder.UseSqlServer(connection);
        }
    }
}



