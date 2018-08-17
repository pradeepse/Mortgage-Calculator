using System.Collections.Generic;
using MortgageCalculator.Api.Repos;
using MortgageCalculator.Dto;

namespace MortgageCalculator.Api.Services
{
    public interface IMortgageService
    {
        List<Mortgage> GetAllMortgages();

        MortgageDetails MortgageCalculate(MortgageDetails det);
    }

    public class MortgageService : IMortgageService
    {

        private readonly IMortgageRepo _mortgageRepo;

        public MortgageService(IMortgageRepo mortgageRepo)
        {
            this._mortgageRepo = mortgageRepo;
        }

        public List<Mortgage> GetAllMortgages()
        {
            return _mortgageRepo.GetAllMortgages();
        }

        public MortgageDetails MortgageCalculate(MortgageDetails model)
        {
            MortgageDetails updmodel = _mortgageRepo.MortgageCalculate(model);
            return updmodel;
        }
    }
}