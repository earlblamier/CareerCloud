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
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        // Default constructor for unit tests
        public SecurityLoginsLogController()
        {
            var repository = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repository);
        }

        public SecurityLoginsLogController(SecurityLoginsLogLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific SecurityLoginsLog by ID.
        /// </summary>
        /// <param name="id">GUID of the SecurityLoginsLog</param>
        /// <returns>A single SecurityLoginsLogPoco</returns>
        [HttpGet]
        [Route("loginslog/{id}")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginLog(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        /// <summary>
        /// Gets all SecurityLoginsLog records.
        /// </summary>
        /// <returns>List of SecurityLoginsLogPoco</returns>
        [HttpGet]
        [Route("loginslogs")]
        [ProducesResponseType(typeof(List<SecurityLoginsLogPoco>), 200)]
        public ActionResult<List<SecurityLoginsLogPoco>> GetAllSecurityLoginsLogs()
        {
            return Ok(_logic.GetAll());
        }

        /// <summary>
        /// Creates new SecurityLoginsLog records.
        /// </summary>
        /// <param name="pocos">Array of SecurityLoginsLogPoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("loginslog")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            return Ok();
        }

        /// <summary>
        /// Updates existing SecurityLoginsLog records.
        /// </summary>
        /// <param name="pocos">Array of SecurityLoginsLogPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpPut]
        [Route("loginslog")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        /// <summary>
        /// Deletes SecurityLoginsLog records.
        /// </summary>
        /// <param name="pocos">Array of SecurityLoginsLogPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("loginslog")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Delete(pocos);
            return Ok(); // Changed from Ok() to NoContent()
        }
    }
}

