using Application.Mapping;
using Application.Middlewares;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Persistence.Consumers;
using System;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ContactMessageConsumer>();
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(Configuration["RabbitMQUrl"], "/", host => {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    config.ReceiveEndpoint("queue:report-service", e => {
                        e.ConfigureConsumer<ContactMessageConsumer>(context);
                    });
                });
            });

            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    // if specified, waits until the bus is started before
                    // returning from IHostedService.StartAsync
                    // default is false
                    options.WaitUntilStarted = true;

                    // if specified, limits the wait time when starting the bus
                    options.StartTimeout = TimeSpan.FromSeconds(10);

                    // if specified, limits the wait time when stopping the bus
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });

            //services.Configure<MassTransitHostOptions>(options =>
            //{
            //    options.WaitUntilStarted = true;
            //    options.StartTimeout = TimeSpan.FromSeconds(30);
            //    options.StopTimeout = TimeSpan.FromMinutes(1);
            //});

            services.AddAutoMapper(typeof(BaseMapper));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Report Microservice", Version = "v1" });
            });
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReportService.API v1"));
            }
            app.UseBaseMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
