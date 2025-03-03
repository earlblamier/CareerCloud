using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CareerCloud.WebAPI
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic _logic;

        // Default constructor for unit tests
        public SystemCountryCodeController()
        {
            var repository = new EFGenericRepository<SystemCountryCodePoco>();
            _logic = new SystemCountryCodeLogic(repository);
        }

        public SystemCountryCodeController(SystemCountryCodeLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific SystemCountryCode by Code.
        /// </summary>
        /// <param name="code">Code of the SystemCountryCode</param>
        /// <returns>A single SystemCountryCodePoco</returns>
        [HttpGet]
        [Route("countrycode/{code}")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSystemCountryCode(string code) // Change the return type to ActionResult
        {
            var poco = _logic.Get(code);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco); // This is still valid as it returns an OkObjectResult
        }



        /// <summary>
        /// Gets all SystemCountryCode records.
        /// </summary>
        /// <returns>List of SystemCountryCodePoco</returns>
        [HttpGet]
        [Route("countrycodes")]
        [ProducesResponseType(typeof(List<SystemCountryCodePoco>), 200)]
        public ActionResult<List<SystemCountryCodePoco>> GetAllSystemCountryCodes()
        {
            return Ok(_logic.GetAll());
        }

        /// <summary>
        /// Creates new SystemCountryCode records.
        /// </summary>
        /// <param name="pocos">Array of SystemCountryCodePoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("countrycode")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            //return CreatedAtAction(nameof(GetAllSystemCountryCodes), null);
            return Ok();
        }

        /// <summary>
        /// Updates existing SystemCountryCode records.
        /// </summary>
        /// <param name="pocos">Array of SystemCountryCodePoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpPut]
        [Route("countrycode")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        /// <summary>
        /// Deletes SystemCountryCode records.
        /// </summary>
        /// <param name="pocos">Array of SystemCountryCodePoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("countrycode")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Delete(pocos);
            return Ok();
        }
    }
}


