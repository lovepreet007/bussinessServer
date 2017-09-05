using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessAppServer.Manager;
using BusinessAppServer.ViewModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessAppServer.Controllers
{
    [Route("api/[controller]")]
    public class FoodController : Controller
    {

        IConfigManager _IConfig;

        public FoodController(IConfigManager _config)
        {
            _IConfig = _config;

        }
        // GET: api/values
        [HttpGet]
        [ProducesResponseType(typeof(List<FoodDetails>),200)]
        public IActionResult  Get()
        {
            var res = _IConfig.GetFoodDetails();
            return Ok(res);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
