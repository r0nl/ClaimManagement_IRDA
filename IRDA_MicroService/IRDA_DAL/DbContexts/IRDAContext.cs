using IRDA_DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRDA_DAL.DbContexts
{
    public class IRDAContext : DbContext
    {
        //public IRDAContext() { }
        public IRDAContext(DbContextOptions<IRDAContext> option) : base(option) { }

        public virtual DbSet<pendingStatusReport>pendingStatusReports { get; set; }
        public virtual DbSet<paymentOfClaim>paymentOfClaims {  get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<Surveyor> Surveyors { get; set; }
        public virtual DbSet<ClaimDetail> ClaimDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>().HasKey(p => p.PolicyNo);
            modelBuilder.Entity<Policy>().HasMany(p => p.claimDetails).WithOne(p => p.Policy).IsRequired().HasForeignKey(c => c.PolicyNo);
            modelBuilder.Entity<Surveyor>().HasKey(s => s.SurveyorId);
            modelBuilder.Entity<Surveyor>().HasMany(s => s.claimDetails).WithOne(c => c.Surveyor).HasForeignKey(c => c.SurveyorID);
            modelBuilder.Entity<Surveyor>().Property(s => s.SurveyorId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Surveyor>().Property(s => s.TimesAllocated).HasDefaultValue(0);
            modelBuilder.Entity<ClaimDetail>().HasKey(f => f.ClaimId);
            modelBuilder.Entity<ClaimDetail>().Property(c => c.WithdrawClaim).HasDefaultValue(false);
            modelBuilder.Entity<ClaimDetail>().Property(c => c.InsuranceCompanyApproval).HasDefaultValue(false);
            modelBuilder.Entity<ClaimDetail>().Property(c => c.ClaimStatus).HasDefaultValue("Open");

            modelBuilder.Entity<pendingStatusReport>().Property(p => p.reportId).ValueGeneratedOnAdd();
            modelBuilder.Entity<paymentOfClaim>().Property(p => p.reportId).ValueGeneratedOnAdd();

            modelBuilder.Entity<pendingStatusReport>().HasKey(p => new { p.reportId, p.stage });

            modelBuilder.Entity<Policy>().HasData(
                new Policy { PolicyNo = "CS12345", InsuredFirstName = "Abcde", InsuredLastName = "Zyxwv", DateOfInsurance = new DateOnly(2024, 1, 1), EmailId = "p1@gmail.com", VehicleNo = "1234", status = false },
                new Policy { PolicyNo = "CS23456", InsuredFirstName = "Bcdef", InsuredLastName = "Yxwvu", DateOfInsurance = new DateOnly(2022, 10, 11), EmailId = "p2@gmail.com", VehicleNo = "2345", status = true },
                new Policy { PolicyNo = "CS34567", InsuredFirstName = "Cdefg", InsuredLastName = "Xwvut", DateOfInsurance = new DateOnly(2023, 11, 12), EmailId = "p3@gmail.com", VehicleNo = "3456", status = false },
                new Policy { PolicyNo = "CS45678", InsuredFirstName = "Defgh", InsuredLastName = "Wvuts", DateOfInsurance = new DateOnly(2024, 1, 21), EmailId = "p4@gmail.com", VehicleNo = "4567", status = true },
                new Policy { PolicyNo = "CS56789", InsuredFirstName = "Efghi", InsuredLastName = "Vutsr", DateOfInsurance = new DateOnly(2024, 1, 19), EmailId = "p5@gmail.com", VehicleNo = "5678", status = true }
            );

            modelBuilder.Entity<Surveyor>().HasData(
                new Surveyor { SurveyorId = 12345, FirstName = "Abcde", LastName = "Zyxwv", EstimateLimit = 20000, TimesAllocated = 4 },
                new Surveyor { SurveyorId = 23456, FirstName = "Bcdef", LastName = "Yxwvu", EstimateLimit = 21000, TimesAllocated = 5 },
                new Surveyor { SurveyorId = 34567, FirstName = "Cdefg", LastName = "Xwvut", EstimateLimit = 22000, TimesAllocated = 6 },
                new Surveyor { SurveyorId = 45678, FirstName = "Defgh", LastName = "Wvuts", EstimateLimit = 23000, TimesAllocated = 7 },
                new Surveyor { SurveyorId = 56789, FirstName = "Efghi", LastName = "Vutsr", EstimateLimit = 24000, TimesAllocated = 8 }
            );

            modelBuilder.Entity<ClaimDetail>().HasData(
                new ClaimDetail { ClaimId = "QWERTYUIOP", PolicyNo = "CS12345", EstimatedLoss = 10000, DateOfAccident = new DateOnly(2024, 2, 3), ClaimStatus = "Closed", SurveyorID = 12345, AmtApprovedBySurveyor = 9000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1500 },
                new ClaimDetail { ClaimId = "WERTYUIOPQ", PolicyNo = "CS23456", EstimatedLoss = 11000, DateOfAccident = new DateOnly(2023, 12, 13), ClaimStatus = "Open", SurveyorID = 23456, AmtApprovedBySurveyor = 10000, InsuranceCompanyApproval = true, WithdrawClaim = false, SurveyorFees = 1700 },
                new ClaimDetail { ClaimId = "ERTYUIOPQW", PolicyNo = "CS34567", EstimatedLoss = 12000, DateOfAccident = new DateOnly(2023, 11, 30), ClaimStatus = "Open", SurveyorID = 34567, AmtApprovedBySurveyor = 11000, InsuranceCompanyApproval = false, WithdrawClaim = true, SurveyorFees = 2000 },
                new ClaimDetail { ClaimId = "RTYUIOPQWE", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 31), ClaimStatus = "Closed", SurveyorID = 45678, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 },
                new ClaimDetail { ClaimId = "TYUIOPQWER", PolicyNo = "CS56789", EstimatedLoss = 14000, DateOfAccident = new DateOnly(2024, 2, 13), ClaimStatus = "Closed", SurveyorID = 56789, AmtApprovedBySurveyor = 13000, InsuranceCompanyApproval = false, WithdrawClaim = false, SurveyorFees = 1800 },
                new ClaimDetail { ClaimId = "1234567890", PolicyNo = "CS12345", EstimatedLoss = 10000, DateOfAccident = new DateOnly(2024, 2, 23), ClaimStatus = "Open", SurveyorID = null, AmtApprovedBySurveyor = 9000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1500 },
                new ClaimDetail { ClaimId = "2345678901", PolicyNo = "CS45678", EstimatedLoss = 13000, DateOfAccident = new DateOnly(2024, 1, 30), ClaimStatus = "Open", SurveyorID = null, AmtApprovedBySurveyor = 12000, InsuranceCompanyApproval = true, WithdrawClaim = true, SurveyorFees = 1900 }
            );
        }
    }
}
