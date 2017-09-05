using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BusinessAppServer.Sms;
using static BusinessAppServer.Sms.ViaNettSMS;
using BusinessAppServer.ViewModel;
using BusinessAppServer.Repostory;
using BusinessAppServer.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessAppServer.CustomAttributes;
using Microsoft.AspNetCore.Http;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessAppServer.Controllers
{
    [Route("api/[controller]")]
   
    public class OrderController :Controller
    {
        IConfigManager _IConfig;        

        public OrderController(IConfigManager _config, IHttpContextAccessor contextAccessor)
        {
            _IConfig = _config;           

        }

        // GET: api/values
        
        [HttpGet]
        //[SessionAttribute]
        [ProducesResponseType(typeof(IEnumerable<Filters>),200)]
        public IActionResult Get()
        {
            var res = _IConfig.GetFilters();
            return Ok(res);
        }

        //  [HttpGet("{phoneNumber}")]  GenerateOTP(string phoneNumber)
        //accept only parameter with phone number like http://localhost:52793/api/order/GenerateOTP/
        //  [HttpGet("GenerateOTP")]  GenerateOTP([FromQuery] string phoneNumber)
        // 'http://localhost:52793/api/order/GenerateOTP?phoneNumber=' + phoneNumber

        // GET api/values/5

        [HttpGet("FilterValues")]
       // [ServiceFilter(typeof(SessionAttribute))]
        [ProducesResponseType(typeof(IEnumerable<Medicines>), 200)]
        public IActionResult FilterValues([FromQuery] string filterValues)
        {
            var res = _IConfig.GetFiltersValues(filterValues);
            return Ok(res);
        }
        // GET api/values/5
        [HttpGet("Search")]
        //[ServiceFilter(typeof(SessionAttribute))]
        [ProducesResponseType(typeof(IEnumerable<Medicines>), 200)]
        public IActionResult Search([FromQuery] string filterId, string searchItem,bool fromOrder)
        {
            var res = _IConfig.Search(filterId,searchItem, fromOrder);
            return Ok(res);
        }


        [HttpGet("FetchOwnersData")]
        //[ServiceFilter(typeof(SessionAttribute))]
        [ProducesResponseType(typeof(IEnumerable<OwnersDetails>), 200)]
        public IActionResult FetchOwnersData()
        {
            var res = _IConfig.FetchOwnersData();
            return Ok(res);
        }
        

        [HttpGet("FetchUpdatedOwnersData")]
       // [SessionAttribute]
        [ProducesResponseType(typeof(IEnumerable<OwnersDetails>), 200)]
        public IActionResult FetchUpdatedOwnersData(string orderId)
        {
            var res = _IConfig.FetchUpdatedOwnersData(orderId);
            return Ok(res);
        }

        // GET api/values/5
        [HttpGet("GenerateOTP")]
      //  [SessionAttribute]
        [ProducesResponseType(typeof(OTPResponse), 200)]
        public IActionResult GenerateOTP([FromQuery] string phoneNumber)
        {
            OTPResponse res = new OTPResponse();
            SmsVerfication sms = new SmsVerfication();
            //res = sms.SendSMS();
            res.OTP = "123456";
            res.OTPtime = DateTime.Now;
            res.Status = true;
            return Ok(res);
        }

        // POST api/values
        [HttpPost]
       // [SessionAttribute]
        [Route("orderdetails")]
        [ProducesResponseType(typeof(ResponseResult), 200)]
        public IActionResult Post([FromBody]OrderDetailVm orderDetails)
        {
            var result = _IConfig.SubmitOrder(orderDetails);
            return  Ok(result);
        }
        [HttpPost]
        [Route("GenerateReport")]
        [ProducesResponseType(typeof(List<Report>), 200)]
        public IActionResult GenerateReport([FromBody]ReportVm reportDetails)
        {
            var result = _IConfig.ReportGeneration(reportDetails);
            return Ok(result);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
