using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CareerCloud.WebAPI
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _logic;

        // Default constructor for unit tests
        public SecurityLoginController()
        {
            var repository = new EFGenericRepository<SecurityLoginPoco>();
            _logic = new SecurityLoginLogic(repository);
        }

        public SecurityLoginController(SecurityLoginLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific SecurityLogin by ID.
        /// </summary>
        /// <param name="id">GUID of the SecurityLogin</param>
        /// <returns>A single SecurityLoginPoco</returns>
        [HttpGet]
        [Route("login/{id}")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLogin(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco); // Ok() works correctly with IActionResult
        }


        /// <summary>
        /// Gets all SecurityLogin records.
        /// </summary>
        /// <returns>List of SecurityLoginPoco</returns>
        [HttpGet]
        [Route("logins")]
        [ProducesResponseType(typeof(List<SecurityLoginPoco>), 200)]
        public ActionResult<List<SecurityLoginPoco>> GetAllSecurityLogins()
        {
            return Ok(_logic.GetAll());
        }

        /// <summary>
        /// Creates new SecurityLogin records.
        /// </summary>
        /// <param name="pocos">Array of SecurityLoginPoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            return Ok();
        }

        /// <summary>
        /// Updates existing SecurityLogin records.
        /// </summary>
        /// <param name="pocos">Array of SecurityLoginPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpPut]
        [Route("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        /// <summary>
        /// Deletes SecurityLogin records.
        /// </summary>
        /// <param name="pocos">Array of SecurityLoginPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
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
