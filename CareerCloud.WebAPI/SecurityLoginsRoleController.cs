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
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly SecurityLoginsRoleLogic _logic;

        // Default constructor for unit tests
        public SecurityLoginsRoleController()
        {
            var repository = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logic = new SecurityLoginsRoleLogic(repository);
        }

        public SecurityLoginsRoleController(SecurityLoginsRoleLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific SecurityLoginsRole by ID.
        /// </summary>
        /// <param name="id">GUID of the SecurityLoginsRole</param>
        /// <returns>A single SecurityLoginsRolePoco</returns>
        [HttpGet]
        [Route("loginsrole/{id}")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginsRole(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco); // This returns an OkObjectResult
        }




        /// <summary>
        /// Gets all SecurityLoginsRole records.
        /// </summary>
        /// <returns>List of SecurityLoginsRolePoco</returns>
        [HttpGet]
        [Route("loginsroles")]
        [ProducesResponseType(typeof(List<SecurityLoginsRolePoco>), 200)]
        public ActionResult<List<SecurityLoginsRolePoco>> GetAllSecurityLoginsRoles()
        {
            return Ok(_logic.GetAll());
        }

        /// <summary>
        /// Creates new SecurityLoginsRole records.
        /// </summary>
        /// <param name="pocos">Array of SecurityLoginsRolePoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("loginsrole")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            return Ok();
            //return CreatedAtAction(nameof(GetAllSecurityLoginsRoles), null);
        }

        /// <summary>
        /// Deletes SecurityLoginsRole records.
        /// </summary>
        /// <param name="pocos">Array of SecurityLoginsRolePoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("loginsrole")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] pocos)
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
