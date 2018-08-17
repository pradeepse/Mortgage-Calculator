using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MortgageCalculator.Api.Services;

namespace MortgageCalculator.Api.Controllers
{
    public class MortgageController : ApiController
    {
        private IMortgageService mortgageService;

        public MortgageController(IMortgageService service)
        {
            this.mortgageService = service;
        }

        // GET: api/Mortgage
        public IEnumerable<Dto.Mortgage> Get()
        {
            return mortgageService.GetAllMortgages();
        }

        // GET: api/Mortgage/5
        public Dto.Mortgage Get(int id)
        {
            return mortgageService.GetAllMortgages().FirstOrDefault(x => x.MortgageId == id);
        }

        [HttpPost]
        public Dto.MortgageDetails MortgageCalc(Dto.MortgageDetails det)
        {
            Dto.MortgageDetails updmodel = mortgageService.MortgageCalculate(det);
            return updmodel;
        }
    }
}
