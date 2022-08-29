using Application.Dtos;
using Application.Features.Request.Commands;
using Application.Mapping;
using Application.Middlewares;
using Domain.Common;
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
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static AppConfig AppConfig { get; set; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ContactMessageConsumer>();
                x.AddBus(ConfigureBus);                
                x.AddRequestClient<AddRangeReportDto>();
            });
            services.AddHostedService<MessageBusMiddleware>();

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

        static IBusControl ConfigureBus(IServiceProvider provider)
        {
            AppConfig = provider.GetRequiredService<IOptions<AppConfig>>().Value;

            X509Certificate2 x509Certificate2 = null;

            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            try
            {
                X509Certificate2Collection certificatesInStore = store.Certificates;

                x509Certificate2 = certificatesInStore.OfType<X509Certificate2>()
                    .FirstOrDefault(cert => cert.Thumbprint?.ToLower() == AppConfig.SSLThumbprint?.ToLower());
            }
            finally
            {
                store.Close();
            }

            var result =  Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(AppConfig.Host, AppConfig.VirtualHost, h =>
                {
                    h.Username(AppConfig.Username);
                    h.Password(AppConfig.Password);

                    if (AppConfig.SSLActive)
                    {
                        h.UseSsl(ssl =>
                        {
                            ssl.ServerName = Dns.GetHostName();
                            ssl.AllowPolicyErrors(SslPolicyErrors.RemoteCertificateNameMismatch);
                            ssl.Certificate = x509Certificate2;
                            ssl.Protocol = SslProtocols.Tls12;
                            ssl.CertificateSelectionCallback = CertificateSelectionCallback;
                        });
                    }
                });
                cfg.ReceiveEndpoint("report-service", ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));
                    ep.ConfigureConsumer<ContactMessageConsumer>((IBusRegistrationContext)provider);
                });
                cfg.ConfigureEndpoints((IBusRegistrationContext)provider);
                cfg.ExchangeType = ExchangeType.Direct;
            });
            return result;
        }

        private static X509Certificate CertificateSelectionCallback(object sender, string targethost, X509CertificateCollection localcertificates, X509Certificate remotecertificate, string[] acceptableissuers)
        {
            var serverCertificate = localcertificates.OfType<X509Certificate2>()
                                    .FirstOrDefault(cert => cert.Thumbprint.ToLower() == AppConfig.SSLThumbprint.ToLower());

            return serverCertificate ?? throw new Exception("Wrong certificate");
        }
    }
}
