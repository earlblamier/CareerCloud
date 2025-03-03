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
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic _logic;

        // Default constructor for unit tests
        public SecurityRoleController()
        {
            var repository = new EFGenericRepository<SecurityRolePoco>();
            _logic = new SecurityRoleLogic(repository);
        }

        public SecurityRoleController(SecurityRoleLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific SecurityRole by ID.
        /// </summary>
        /// <param name="id">GUID of the SecurityRole</param>
        /// <returns>A single SecurityRolePoco</returns>
        [HttpGet]
        [Route("role/{id}")]
        [ProducesResponseType(typeof(SecurityRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityRole(Guid id)
        //public ActionResult<SecurityRolePoco> GetSecurityRole(Guid id) // Cannot convert type 'Microsoft.AspNetCore.Mvc.ActionResult<CareerCloud.Pocos.SecurityRolePoco>' to 'Microsoft.AspNetCore.Mvc.OkObjectResult' via a reference conversion, boxing conversion, unboxing conversion, wrapping conversion, or null type conversion
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        /// <summary>
        /// Gets all SecurityRole records.
        /// </summary>
        /// <returns>List of SecurityRolePoco</returns>
        [HttpGet]
        [Route("roles")]
        [ProducesResponseType(typeof(List<SecurityRolePoco>), 200)]
        public ActionResult<List<SecurityRolePoco>> GetAllSecurityRoles()
        {
            var roles = _logic.GetAll();
            if (roles == null || roles.Count == 0)
            {
                return NotFound();
            }
            return Ok(roles);
        }

        /// <summary>
        /// Creates new SecurityRole records.
        /// </summary>
        /// <param name="pocos">Array of SecurityRolePoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("role")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            return Ok();
        }

        /// <summary>
        /// Updates existing SecurityRole records.
        /// </summary>
        /// <param name="pocos">Array of SecurityRolePoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpPut]
        [Route("role")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        /// <summary>
        /// Deletes SecurityRole records.
        /// </summary>
        /// <param name="pocos">Array of SecurityRolePoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("role")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] pocos)
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

