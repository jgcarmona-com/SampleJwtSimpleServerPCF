using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Sample.API.Controllers
   
{
    /// <summary>
    /// THIS IS AN INSECURE CONTROLLER
    /// There is no Authorisation mechanism here and it allow every request.
    /// </summary>
    /// <seealso cref="Controller" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InsecuredController : Controller
    {
        private readonly ILogger<SecuredController> _logger;

        public InsecuredController(ILogger<SecuredController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// EVERYONE CAN CALL THIS METHOD
        /// </summary>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Insecure Value", "Insecure Value" };
        }

        /// <summary>
        /// EVERYONE CAN CALL THIS METHOD
        /// </summary>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Insecure Value {id}";
        }
        
        /// <summary>
        /// EVERYONE CAN CALL THIS METHOD
        /// </summary>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// EVERYONE CAN CALL THIS METHOD
        /// </summary>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// EVERYONE CAN CALL THIS METHOD
        /// </summary>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

