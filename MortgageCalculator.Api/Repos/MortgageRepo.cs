using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MortgageCalculator.Dto;

namespace MortgageCalculator.Api.Repos
{
    public interface IMortgageRepo
    {
        List<Mortgage> GetAllMortgages();

        MortgageDetails MortgageCalculate(MortgageDetails det);
    }

    public class MortgageRepo : IMortgageRepo
    {
        public List<Mortgage> GetAllMortgages()
        {
            using (var context = new MortgageData.MortgageDataContext())
            {
                var mortgages = context.Mortgages.ToList();
                List<Mortgage> result = new List<Mortgage>();
                foreach (var mortgage in mortgages)
                {
                    result.Add(new Mortgage()
                        {
                            Name = mortgage.Name,
                            EffectiveStartDate = mortgage.EffectiveStartDate,
                            EffectiveEndDate = mortgage.EffectiveEndDate,
                            CancellationFee = mortgage.CancellationFee,
                            EstablishmentFee = mortgage.CancellationFee,
                            InterestRepayment = (InterestRepayment)Enum.Parse(typeof(InterestRepayment), mortgage.InterestRepayment.ToString()),
                            MortgageId = mortgage.MortgageId,
                           MortgageType = (MortgageType)Enum.Parse(typeof(MortgageType), mortgage.MortgageType.ToString())
                           //TermsInMonths = mortgage.TermsInMonths
                    }
                    );
                }
                return result;
            }
        }

        public MortgageDetails MortgageCalculate(MortgageDetails mortgageDet)
        {
            mortgageDet.LoanDurationM = (mortgageDet.LoanDurationY * 12);
            mortgageDet.InterestRateM = ((mortgageDet.InterestRateY / 100) / 12);
            mortgageDet.MonthlyPayment = Math.Round(mortgageDet.LoanAmount * (mortgageDet.InterestRateM*(Math.Pow((1 + mortgageDet.InterestRateM),mortgageDet.LoanDurationM)) / (Math.Pow((1 + mortgageDet.InterestRateM) , mortgageDet.LoanDurationM) - 1)),2);
            mortgageDet.TotalRepayment = Math.Round(Convert.ToDecimal(mortgageDet.MonthlyPayment * mortgageDet.LoanDurationM),2);
            mortgageDet.TotalInterestPaid = Math.Round(mortgageDet.TotalRepayment - mortgageDet.LoanAmount,2);
            return mortgageDet;
        }


    }
}