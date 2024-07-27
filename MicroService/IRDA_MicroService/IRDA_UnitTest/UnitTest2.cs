using IRDA_BAL.Services;
using IRDA_DAL.Model;
using IRDA_DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_UnitTest
{
    public class TestPaymentService
    {

        private IRDAServices _irdaService;
        private Mock<IIRDARepository> _mockRepo;


        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IIRDARepository>();
            _irdaService = new IRDAServices(_mockRepo.Object);
        }

        [Test]
        public void PaymentStatusToDatabase_WithValid_ReturnsTrue()
        {
            string month = "Jan";
            int year = 2024;

            var mockClaimDetails = new List<ClaimDetail> {
                new ClaimDetail { ClaimId = "RTYUIOPQWE", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 31), ClaimStatus = "Closed", SurveyorID = 45678, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 },
                new ClaimDetail { ClaimId = "2345678901", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 30), ClaimStatus = "Open", SurveyorID = null, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 }
            };

            _mockRepo.Setup(repo => repo.GetClaimDetails(month, year)).Returns(mockClaimDetails);

            _mockRepo.Setup(repo => repo.GetReportIdsPayments()).Returns(new string[] { });
            _mockRepo.Setup(repo => repo.AddPaymentReport(It.IsAny<paymentOfClaim>())).Returns(true);


            bool result = _irdaService.PaymentStatusToDatabase(month, year);


            Assert.IsTrue(result);
        }

        [Test]
        public void PaymentStatusToDatabase_WithNoClaimDetails_ReturnsTrue()
        {

            string month = "Jan";
            int year = 2024;

            _mockRepo.Setup(repo => repo.GetClaimDetails(month, year)).Returns(new List<ClaimDetail>());

            _mockRepo.Setup(repo => repo.GetReportIdsPayments()).Returns(new string[] { });
            _mockRepo.Setup(repo => repo.AddPaymentReport(It.IsAny<paymentOfClaim>())).Returns(true);


            bool result = _irdaService.PaymentStatusToDatabase(month, year);

            Assert.IsTrue(result);
        }

        [Test]
        public void PaymentStatusToDatabase_WithExistingReportId_ReturnsTrue()
        {

            string month = "Jan";
            int year = 2024;

            var mockClaimDetails = new List<ClaimDetail> {
                new ClaimDetail { ClaimId = "RTYUIOPQWE", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 31), ClaimStatus = "Closed", SurveyorID = 45678, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 },
                new ClaimDetail { ClaimId = "2345678901", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 30), ClaimStatus = "Open", SurveyorID = null, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 }
            };
            string existingReportId = "PSJan24";
            var obj = new[] { existingReportId };


            _mockRepo.Setup(repo => repo.GetClaimDetails(month, year)).Returns(mockClaimDetails);
            _mockRepo.Setup(repo => repo.GetReportIdsPayments()).Returns(obj);
            _mockRepo.Setup(repo => repo.DeleteClaimRecord(existingReportId)).Returns(true);
            _mockRepo.Setup(repo => repo.AddPaymentReport(It.IsAny<paymentOfClaim>())).Returns(true);


            var result = _irdaService.PaymentStatusToDatabase(month, year);

            Assert.That(result, Is.True);

        }


        [Test]
        public void PaymentStatusByMonthAndYear_WithData_ReturnsExpectedResult()
        {
            string month = "Jan";
            int year = 2024;

            List<paymentOfClaim> paymentReport = new List<paymentOfClaim> { new paymentOfClaim { reportId = "PSJan24", month = month, year = year, payment = 20000 } };

            _mockRepo.Setup(repo => repo.GetPaymentStatusByMonthAndYear(month, year)).Returns(paymentReport);

            var result = _irdaService.PaymentStatusByMonthAndYear(month, year);

            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void PaymentStatusByMonthAndYear_WithNoData_ReturnsNothing()
        {
            string month = "Jan";
            int year = 2024;
            List<paymentOfClaim> paymentReport = new List<paymentOfClaim>
            {
            };
            _mockRepo.Setup(repo => repo.GetPaymentStatusByMonthAndYear(month, year)).Returns(paymentReport);

            var result = _irdaService.PaymentStatusByMonthAndYear(month, year);

            Assert.That(result, Is.Empty);
        }
    }
}
