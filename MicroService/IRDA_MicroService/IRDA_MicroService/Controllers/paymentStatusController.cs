using IRDA_BAL.DTOs;
using IRDA_BAL.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IRDA_MicroService.Controllers
{
    [Route("irda")]
    [ApiController]
    public class paymentStatusController : ControllerBase
    {
        private readonly IIRDAServices _service;
        private readonly ILog _logger;

        public paymentStatusController(IIRDAServices service, ILog logger)
        {
            _service = service;
            _logger = logger;
        }

    

        // GET: irda/<paymentStatusController>/report/{month}/{year}
        [HttpGet("paymentStatus/report/{month}/{year}")]
        public IActionResult GetPaymentStatusReport(string month, int year)
        {
            try
            {
                DateTime monthDate = DateTime.ParseExact(month, "MMM", System.Globalization.CultureInfo.InvariantCulture);
                DateTime currentDate = DateTime.Today;
                DateTime inputDate = new DateTime(year, monthDate.Month, 1).AddMonths(1).AddDays(-1);
                if (currentDate < inputDate)
                {
                    throw new Exception("Enter a valid Date");
                }

                List<ClaimPaymentReportDTO>? payments = _service.PaymentStatusByMonthAndYear(month, year);

                if (payments.Count < 1) throw new Exception("No Records found. Try Updating the Database Once!");

                return Ok(payments);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST irda/<paymentStatusController>/pull/{month}/{year}
        [HttpPost("paymentStatus/pull/{month}/{year}")]
        public IActionResult PostPaymentStatusReport(string month, int year)
        {
            try
            {
                DateTime monthDate = DateTime.ParseExact(month, "MMM", System.Globalization.CultureInfo.InvariantCulture);
                DateTime currentDate = DateTime.Today;
                DateTime inputDate = new DateTime(year, monthDate.Month, 1).AddMonths(1).AddDays(-1);
                if (currentDate < inputDate)
                {
                    throw new Exception("Enter a valid Date");
                }

                bool status = _service.PaymentStatusToDatabase(month, year);
                if (status) return Ok(status);
                else throw new Exception("Something gone wrong");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, new {message = ex.Message});
            }
        }
    }
}
