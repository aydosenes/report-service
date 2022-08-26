using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repository
{
    public interface IReportRepository : IBaseMongoRepository<Report>
    {
    }
}
