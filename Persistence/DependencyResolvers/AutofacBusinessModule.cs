using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Autofac;
using Persistence.Repositories;
using Persistence.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReportRepository>().As<IReportRepository>()
                    .WithParameter("connectionString", "mongodb://localhost:27017")
                    .WithParameter("databaseName", "ReportDb")
                    .WithParameter("collectionName", "Reports")
                    .SingleInstance();

            builder.RegisterType<RestService>().As<IRestService>().SingleInstance();
        }
    }
}
