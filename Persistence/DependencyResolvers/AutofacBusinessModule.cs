using Application.Interfaces.Repository;
using Autofac;
using Persistence.Repositories;
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
        }
    }
}
