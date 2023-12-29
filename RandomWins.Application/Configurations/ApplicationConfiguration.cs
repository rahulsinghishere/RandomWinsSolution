using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomWins.Application.IServices;
using RandomWins.Application.Services;
using RandomWins.Core.Domain;
using RandomWins.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Application.Configurations
{
    public static class ApplicationConfiguration
    {
        private static IConfiguration _configuration;
        
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;
            services.AddScoped<IGameSessionUserService, GameSessionUserService>();
            services.AddSingleton<IGameSessionService>(ImplementAbstractionsInScope);
        }

        private static GameSessionService ImplementAbstractionsInScope(IServiceProvider serviceProvider)
        {
            
            //using(IServiceScope scope = serviceProvider.CreateScope()) 
            //{
                return new GameSessionService(serviceProvider,Int32.Parse(_configuration.GetSection("GameSessionConfigurations:SessionTime").Value));
            //}
        }
    }
}

//Int32.Parse(configuration.GetSection("GameSessionConfigurations:SessionTime").Value))

