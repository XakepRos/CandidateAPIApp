using CandidateApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CandidateDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Handle exceptions here or log them
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                }
            }
        }
    }
}
