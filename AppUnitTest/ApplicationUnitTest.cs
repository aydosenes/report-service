using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;

namespace AppUnitTest
{
    public class ApplicationUnitTest 
    {
        private readonly ReportController _reportController;
        private readonly Mock<IMediator> _mediatorMoq;
        public ApplicationUnitTest()
        {
            _mediatorMoq = new Mock<IMediator>();
            _reportController = new ReportController(_mediatorMoq.Object);
        }
        [Fact]
        public void Get_Report_Test()
        {
            var result = _reportController.GetReport();
            Assert.NotNull(result);
            Assert.IsType<Task<IActionResult>>(result);
        }
    }
}
