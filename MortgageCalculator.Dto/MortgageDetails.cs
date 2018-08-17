using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.Dto
{
    public class MortgageDetails
    {
        [Required]
        public Int64 LoanAmount { get; set; }

        [Required]
        public double LoanDurationY { get; set; }

        public double LoanDurationM { get; set; }

        [Required]
        public double InterestRateY { get; set; }

        public double InterestRateM { get; set; }

        public double MonthlyPayment { get; set; }

        public decimal TotalRepayment { get; set; } 

        public decimal TotalInterestPaid { get; set; }
    }
}
