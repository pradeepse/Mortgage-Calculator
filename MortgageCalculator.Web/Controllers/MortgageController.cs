using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Api =MortgageCalculator.Api;
using Dto = MortgageCalculator.Dto;


namespace MortgageCalculator.Web.Controllers
{
    public class MortgageController : Controller
    {
        private readonly HttpClient _httpClient;

        private string Baseurl;

        public MortgageController(): this(new HttpClient())
        {

        }

        public MortgageController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            Baseurl = "http://localhost:49608/";
        }

        public ActionResult MortgageDetails()
        {
            return View();
        }

        
        public async Task<JsonResult> GetMortgageDetails()
        {
            List<Dto.Mortgage> lstMortgage = new List<Dto.Mortgage>();
            _httpClient.BaseAddress = new Uri(Baseurl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                HttpResponseMessage Res = await _httpClient.GetAsync("api/Mortgage");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lstMortgage = JsonConvert.DeserializeObject<List<Dto.Mortgage>>(EmpResponse);
                }
                return Json(lstMortgage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MortgageCalculate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> MortgageCalculate(Dto.MortgageDetails model)
        {
            if(ModelState.IsValid)
            { 
                _httpClient.BaseAddress = new Uri(Baseurl);
                    HttpResponseMessage result = await _httpClient.PostAsJsonAsync<Dto.MortgageDetails>("api/Mortgage", model);
                    if (result.IsSuccessStatusCode)
                    {
                        var EmpResponse = result.Content.ReadAsStringAsync().Result;
                        model = JsonConvert.DeserializeObject<Dto.MortgageDetails>(EmpResponse); ;
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
            return View();
        }

    }
}