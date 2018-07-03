using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Sample.API.Controllers
{
    /// <summary>
    /// THIS IS A SECURED CONTROLLER
    /// It is using JWT Simple Server as Authorisation mechanism internally.
    /// Only authenticated users can use it.
    /// To get a valid token use Postman and send a POST to http:YourHost:YourPort/token
    /// With this HEADER: [{"key":"Content-Type","value":"application/x-www-form-urlencoded"}]
    /// And with a RAW BODY with this text: 'grant_type=password&amp;username=sampleapi&amp;password=sampliapipassword' 
    /// </summary>
    /// <seealso cref="Controller" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]    
    public class SecuredController : Controller
    {
        private readonly ILogger<SecuredController> _logger;

        public SecuredController(ILogger<SecuredController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// THIS IS A SECURED METHOD
        /// </summary>
        /// <remarks>
        /// It is using JWT Simple Server as Authorisation mechanism internally.
        /// Only authenticated users can use it.
        /// To get a valid token use Postman and send a POST to http:YourHost:YourPort/token
        /// With this HEADER: [{"key":"Content-Type","value":"application/x-www-form-urlencoded"}]
        /// And with a RAW BODY with this text: 'grant_type=password&amp;username=sampleapi&amp;password=sampliapipassword' 
        /// </remarks>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Secured Value", "Secured Value" };
        }

        /// <summary>
        /// THIS IS A SECURED METHOD
        /// </summary>
        /// <remarks>
        /// Use same token as before...
        /// </remarks>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Secured Value {id}";
        }

        /// <summary>
        /// THIS IS A SECURED METHOD
        /// </summary>
        /// <remarks>
        /// Use same token as before...
        /// </remarks>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// THIS IS A SECURED METHOD
        /// </summary>
        /// <remarks>
        /// Use same token as before...
        /// </remarks>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// THIS IS A SECURED METHOD
        /// </summary>
        /// <remarks>
        /// Use same token as before...
        /// </remarks>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
