using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DueDateService;
using DueDateService.Calculator;
using DueDateService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DueDateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DueDatesCalculatorController : ControllerBase
    {
        // GET: api/DueDatesCalculator
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] 
            {
                "Hello, please input the following query parameters in /calculate to calculate the missed payments",
                "DueDate, CurrentDate, Frequency (monthly, bi-weekly, semi-monthly), DueDay1(required for semi-monthly), DueDay2(required for semi-Monthly)",
                "ex: api/DueDatesCalculator/calculate?DueDate=2019-01-01&CurrentDate=2019-02-13&Frequency=bi-weekly",
                "ex: api/DueDatesCalculator/calculate?DueDate=2019-02-15&CurrentDate=2019-07-15&Frequency=semi-monthly&DueDay1=15&DueDay2=30"
            };
        }

        // GET: api/DueDatesCalculator/calculate?DueDate=2019-01-01&CurrentDate=2019-02-13&Frequency=bi-weekly
        // api/DueDatesCalculator/calculate?DueDate=2019-02-15&CurrentDate=2019-07-15&Frequency=semi-monthly&DueDay1=15&DueDay2=30
        [HttpGet, Route("calculate")]
        public ActionResult<DueDateQueryResponse> Get([FromQuery] DueDateQueryRequest request)
        {            
            var calculator = new MissedDateCalculator();

            var serviceRequest = new DueDateRequest
            {
                DueDate = DateTime.Parse(request.DueDate),
                CurrentDate = DateTime.Parse(request.CurrentDate),
                Frequency = request.Frequency,
                DueDay1 = Convert.ToInt32(request.DueDay1),
                DueDay2 = Convert.ToInt32(request.DueDay2)
            };

            var response = new DueDateQueryResponse
            {
                Request = serviceRequest,
                Response = calculator.Calculate(serviceRequest)
            };

            return response;
        }

        // POST: api/DueDatesCalculator
        [HttpPost]
        public void Post([FromBody] string value)
        {


        }

        // PUT: api/DueDatesCalculator/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
