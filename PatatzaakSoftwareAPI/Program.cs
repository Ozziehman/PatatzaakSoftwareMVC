
using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.DataAccessLayer;

namespace PatatzaakSoftwareAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("MainDbConnectionPC");
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // Register DbContext with its options
            builder.Services.AddDbContext<MainDb>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Register other services...
            builder.Services.AddScoped<ItemRepository>();
            builder.Services.AddScoped<OrderRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<VoucherRepository>();
            builder.Services.AddScoped<OrderedItemRepository>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}