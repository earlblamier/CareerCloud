using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CareerCloud.WebAPI
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _logic;

        // Default constructor for unit tests
        public CompanyProfileController()
        {
            var repository = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repository);
        }

        public CompanyProfileController(CompanyProfileLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific CompanyProfile by ID.
        /// </summary>
        /// <param name="id">GUID of the CompanyProfile</param>
        /// <returns>A single CompanyProfilePoco</returns>
        [HttpGet]
        [Route("profile/{id}")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyProfile(Guid id) // Change the return type to ActionResult
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);  // This will still return OkObjectResult, but the return type is ActionResult
        }



        /// <summary>
        /// Gets all CompanyProfile records.
        /// </summary>
        /// <returns>List of CompanyProfilePoco</returns>
        [HttpGet]
        [Route("profiles")]
        [ProducesResponseType(typeof(List<CompanyProfilePoco>), 200)]
        public ActionResult<List<CompanyProfilePoco>> GetAllCompanyProfiles()
        {
            return Ok(_logic.GetAll());
        }

        /// <summary>
        /// Creates new CompanyProfile records.
        /// </summary>
        /// <param name="pocos">Array of CompanyProfilePoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("profile")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            //return CreatedAtAction(nameof(GetAllCompanyProfiles), null);
            return Ok();
        }

        /// <summary>
        /// Updates existing CompanyProfile records.
        /// </summary>
        /// <param name="pocos">Array of CompanyProfilePoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpPut]
        [Route("profile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        /// <summary>
        /// Deletes CompanyProfile records.
        /// </summary>
        /// <param name="pocos">Array of CompanyProfilePoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("profile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
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
