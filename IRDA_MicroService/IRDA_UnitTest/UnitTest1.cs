using IRDA_BAL.DTOs;
using IRDA_BAL.Services;
using IRDA_DAL.Model;
using IRDA_DAL.Repositories;
using Moq;

namespace IRDA_UnitTest
{
    [TestFixture]
    public class TestsClaimService
    {

        private IRDAServices _irdaService;
        private Mock<IIRDARepository> _mockRepo;


        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IIRDARepository> ();
            _irdaService = new IRDAServices (_mockRepo.Object);
        }

        [Test]
        public void ClaimStatusToDatabase_WithValidData_ReturnsTrue()
        {

            string month = "Jan";
            int year = 2024;

            var mockClaimDetails = new List<ClaimDetail> {
                new ClaimDetail { ClaimId = "RTYUIOPQWE", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 31), ClaimStatus = "Closed", SurveyorID = 45678, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 },
                new ClaimDetail { ClaimId = "2345678901", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 30), ClaimStatus = "Open", SurveyorID = null, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 }
            };
            
            _mockRepo.Setup(repo => repo.GetClaimDetails(month, year)).Returns(mockClaimDetails);

            string newReportId = "newId";
            _mockRepo.Setup(repo => repo.GetReportIdsClaims()).Returns(new string[] { });
            _mockRepo.Setup(repo => repo.AddClaimReport(It.IsAny<pendingStatusReport>())).Returns(true);


            bool result = _irdaService.ClaimStatusToDatabase(month, year);


            Assert.IsTrue(result);
            _mockRepo.Verify(repo => repo.AddClaimReport(It.IsAny<pendingStatusReport>()), Times.AtLeastOnce);
        }

        [Test]
        public void ClaimStatusToDatabase_WithNoClaimDetails_ThrowsException()
        {

            string month = "Jan";
            int year = 2024;

            _mockRepo.Setup(repo => repo.GetClaimDetails(month, year)).Returns(new List<ClaimDetail>());

            var ex = Assert.Throws<Exception>(() => _irdaService.ClaimStatusToDatabase(month, year));
            Assert.That(ex.Message, Is.EqualTo("No Claim Found!"));
        }

        [Test]
        public void ClaimStatusToDatabase_WithExistingReportId_ReturnsTrue()
        {

            string month = "Jan";
            int year = 2024;

            var mockClaimDetails = new List<ClaimDetail> {
                new ClaimDetail { ClaimId = "RTYUIOPQWE", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 31), ClaimStatus = "Closed", SurveyorID = 45678, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 },
                new ClaimDetail { ClaimId = "2345678901", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 30), ClaimStatus = "Open", SurveyorID = null, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 }
            };
            string existingReportId = "PSJan24";

            _mockRepo.Setup(repo => repo.GetClaimDetails(month, year)).Returns(mockClaimDetails);
            _mockRepo.Setup(repo => repo.GetReportIdsClaims()).Returns(new[] { existingReportId });
            _mockRepo.Setup(repo => repo.DeleteClaimRecord(existingReportId)).Returns(true);
            _mockRepo.Setup(repo => repo.AddClaimReport(It.IsAny<pendingStatusReport>())).Returns(true);


            bool result = _irdaService.ClaimStatusToDatabase(month, year);


            Assert.IsTrue(result);
            _mockRepo.Verify(repo => repo.AddClaimReport(It.IsAny<pendingStatusReport>()), Times.AtLeastOnce);
        }

        [Test]
        public void ClaimStatusByMonthAndYear_WithData_ReturnsExpectedResult()
        {
            string month = "Jan";
            int year = 2024;
            List<pendingStatusReport> ClaimReports = new List<pendingStatusReport>
            {
                new pendingStatusReport {reportId="PSJan24", month=month, year=year, count=1, stage="New Claims"},
                new pendingStatusReport {reportId="PSJan24", month=month, year=year, count=2, stage="Pending Claims"},
                new pendingStatusReport {reportId="PSJan24", month=month, year=year, count=3, stage="Finalized Claims"}
            };
            _mockRepo.Setup(repo => repo.GetClaimStatusByMonthAndYear(month, year)).Returns(ClaimReports);

            var result = _irdaService.ClaimStatusByMonthAndYear(month, year);

            Assert.That(result, Is.Not.Empty );
        }

        [Test]
        public void ClaimStatusByMonthAndYear_WithNoData_ReturnsNothing()
        {
            string month = "Jan";
            int year = 2024;
            List<pendingStatusReport> ClaimReports = new List<pendingStatusReport>
            {
            };
            _mockRepo.Setup(repo => repo.GetClaimStatusByMonthAndYear(month, year)).Returns(ClaimReports);

            var result = _irdaService.ClaimStatusByMonthAndYear(month, year);

            Assert.That(result, Is.Empty);
        }
    }
}