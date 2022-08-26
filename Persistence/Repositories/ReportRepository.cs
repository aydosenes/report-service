using Application.Interfaces.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ReportRepository : BaseMongoRepository<Report>, IReportRepository
    {
        public ReportRepository(string connectionString, string databaseName, string collectionName) : base(connectionString, databaseName, collectionName)
        {

        }
    }
}

