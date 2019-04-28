using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModulSchool.BusinessLogic;
using ModulSchool.Services;
using ModulSchool.Services.Interfaces;
using MassTransit;
using ModulSchool.Commands;
using ModulSchool.Consumers;
using Microsoft.Extensions.Hosting;


namespace ModulSchool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<GetUsersInfoRequestHandler>();
            services.AddScoped<AppendUsersRequestHandler>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            
            // Обработчики событий MassTransit ----начало
            services.AddScoped<AppendUserConsumer>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AppendUserConsumer>();
                x.AddBus(provider => MassTransit.Bus.Factory.CreateUsingInMemory(cfg =>
                {
                    cfg.ReceiveEndpoint("append-user-queue", ep =>
                    {
                        ep.ConfigureConsumer<AppendUserConsumer>(provider);
                        EndpointConvention.Map<AppendUserCommand>(ep.InputAddress);
                    });
                }));

                x.AddRequestClient<AppendUserCommand>();
            });
            // Обработчики событий MassTransit ----конец

            services.AddSingleton<IHostedService, BusService>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
