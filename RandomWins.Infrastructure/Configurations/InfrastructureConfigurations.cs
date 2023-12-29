using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomWins.Infrastructure.IRepository;
using RandomWins.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Infrastructure.Configurations
{
    public static class InfrastructureConfigurations
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<RandomWinsDBContext>((opts) =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("RandomWinsConnectionString"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IGameSessionUserRepository,GameSessionUserRepository>();
        }
    }
}

