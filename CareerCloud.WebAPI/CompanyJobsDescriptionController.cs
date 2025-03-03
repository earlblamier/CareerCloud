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
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _logic;

        // Default constructor for unit tests
        public CompanyJobsDescriptionController()
        {
            var repository = new EFGenericRepository<CompanyJobDescriptionPoco>();
            _logic = new CompanyJobDescriptionLogic(repository);
        }

        public CompanyJobsDescriptionController(CompanyJobDescriptionLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific CompanyJobDescription by ID.
        /// </summary>
        /// <param name="id">GUID of the CompanyJobDescription</param>
        /// <returns>A single CompanyJobDescriptionPoco</returns>
        [HttpGet]
        [Route("jobdescription/{id}")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        //public IActionResult GetCompanyJobsDescription(Guid id)
        public ActionResult GetCompanyJobsDescription(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }


        /// <summary>
        /// Gets all CompanyJobDescription records.
        /// </summary>
        /// <returns>List of CompanyJobDescriptionPoco</returns>
        [HttpGet]
        [Route("jobdescriptions")]
        [ProducesResponseType(typeof(List<CompanyJobDescriptionPoco>), 200)]
        public ActionResult<List<CompanyJobDescriptionPoco>> GetAllCompanyJobDescriptions()
        {
            return Ok(_logic.GetAll());
        }

        /// <summary>
        /// Creates new CompanyJobDescription records.
        /// </summary>
        /// <param name="pocos">Array of CompanyJobDescriptionPoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("jobdescription")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            return Ok();
        }

        /// <summary>
        /// Updates existing CompanyJobDescription records.
        /// </summary>
        /// <param name="pocos">Array of CompanyJobDescriptionPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpPut]
        [Route("jobdescription")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        /// <summary>
        /// Deletes CompanyJobDescription records.
        /// </summary>
        /// <param name="pocos">Array of CompanyJobDescriptionPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("jobdescription")]
        [ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public ActionResult DeleteCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            /*if (pocos == null)
            {
                return BadRequest();
            }*/

            _logic.Delete(pocos);
            //return NoContent();
            return Ok();
        }
    }
}
