﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Api = MortgageCalculator.Api;
using Dto = MortgageCalculator.Dto;
using MortgageCalculator.Api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MortgageCalculator.UnitTests
{
    [TestClass]
    public class MortgageController
    {
        private List<Dto.Mortgage> model = null;
        private Dto.MortgageDetails details = null;
        private Dto.MortgageDetails expdetails = null;

        [TestInitialize]
        public void Setup()
        {
            this.model = new List<Dto.Mortgage>();

            DateTime StartDate= DateTime.Now;
            StartDate = StartDate.AddDays(-10);

            model.Add(new Dto.Mortgage
            {
                Name = "Fixed Home Loan (Interest Only)",
                EffectiveStartDate = DateTime.Parse(StartDate.ToString()),
                EffectiveEndDate = DateTime.Parse(DateTime.Today.ToString()),
                CancellationFee = Convert.ToDecimal(259.99),
                EstablishmentFee = Convert.ToDecimal(259.99),
                InterestRepayment = (Dto.InterestRepayment)Enum.Parse(typeof(Dto.InterestRepayment), "InterestOnly"),
                MortgageId = 1,
                MortgageType = (Dto.MortgageType)Enum.Parse(typeof(Dto.MortgageType), "Variable")
            });

            details = new Dto.MortgageDetails();
            details.LoanAmount = 100000;
            details.LoanDurationY = 15;
            details.InterestRateY = 6;

            expdetails = new Dto.MortgageDetails();
            expdetails.LoanAmount = 100000;
            expdetails.LoanDurationY = 15;
            expdetails.InterestRateY = 6;
            expdetails.MonthlyPayment = 843.90;
            expdetails.TotalRepayment = 151902;
            expdetails.TotalInterestPaid = 51902;
        }

        [TestMethod]
        public void GetAllMortgageDetails()
        {
            //Arrange
            Mock<IMortgageService> mortgageService = new Mock<IMortgageService>();
            mortgageService.Setup(ur => ur.GetAllMortgages()).Returns(this.model);

            //Action
            Api.Controllers.MortgageController controller = new Api.Controllers.MortgageController(mortgageService.Object);
            IEnumerable<Dto.Mortgage> result = controller.Get();

            //Assert
            mortgageService.Verify(ur => ur.GetAllMortgages(), Times.Once);
            List<Dto.Mortgage> data = (List<Dto.Mortgage>)(result);
            Assert.AreSame(model, data);
        }

        [TestMethod]
        public void MortgageCalculate()
        {
            //Arrange
            Mock<IMortgageService> mortgageService = new Mock<IMortgageService>();
            mortgageService.Setup(ur => ur.MortgageCalculate(this.details)).Returns(this.expdetails);

            //Action
            Api.Controllers.MortgageController controller = new Api.Controllers.MortgageController(mortgageService.Object);
            Dto.MortgageDetails updModel = controller.MortgageCalc(this.details);

            //Assert
            mortgageService.Verify(ur => ur.MortgageCalculate(this.details), Times.Once);
            Assert.AreSame(expdetails, updModel);
        }

    }
}
