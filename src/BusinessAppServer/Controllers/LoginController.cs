using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessAppServer.ViewModel;
using BusinessAppServer.Manager;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessAppServer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class LoginController : Controller
    {
        IConfigManager _IConfig;

        public LoginController(IConfigManager _config)
        {
            _IConfig = _config;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("email")]
        [Route("ForgotPassword")]
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [AllowAnonymous]
        public IActionResult ForgotPassword([FromQuery] string email)
        {
            return Ok(_IConfig.ForgotPassword(email));
        }

        // POST api/values
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseVm),200)]
        public IActionResult Login([FromBody]LoginRequestVm loginRequest)
        {            
            return Ok(_IConfig.Login(loginRequest));
        }
        [HttpPost]
        [Route("Registration")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseResult),200)]
        public IActionResult Registration([FromBody]RegistrationVm registrationRequest)
        {
            return Ok(_IConfig.Registration(registrationRequest));
        }

        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseResult), 200)]
        public IActionResult ResetPassword([FromBody]ResetPasswordVm resetVm)
        {
            return Ok(_IConfig.ResetPassword(resetVm));
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
