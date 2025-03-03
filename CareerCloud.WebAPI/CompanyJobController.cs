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
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic _logic;

        // Default constructor for unit tests
        public CompanyJobController()
        {
            var repository = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repository);
        }

        public CompanyJobController(CompanyJobLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific CompanyJob by ID.
        /// </summary>
        /// <param name="id">GUID of the CompanyJob</param>
        /// <returns>A single CompanyJobPoco</returns>
        [HttpGet]
        [Route("job/{id}")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]
        [ProducesResponseType(404)]
        //public IActionResult GetCompanyJob(Guid id)
        //public ActionResult<CompanyJobPoco> GetCompanyJob(Guid id)
        public ActionResult GetCompanyJob(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound(); // This returns a NotFoundResult
            }
            return Ok(poco); // This returns an OkObjectResult
        }



        /// <summary>
        /// Gets all CompanyJob records.
        /// </summary>
        /// <returns>List of CompanyJobPoco</returns>
        [HttpGet]
        [Route("jobs")]
        [ProducesResponseType(typeof(List<CompanyJobPoco>), 200)]
        public ActionResult<List<CompanyJobPoco>> GetAllCompanyJobs()
        {
            return Ok(_logic.GetAll());
        }

        /// <summary>
        /// Creates new CompanyJob records.
        /// </summary>
        /// <param name="pocos">Array of CompanyJobPoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("job")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            return Ok();
        }

        /// <summary>
        /// Updates existing CompanyJob records.
        /// </summary>
        /// <param name="pocos">Array of CompanyJobPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpPut]
        [Route("job")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        /// <summary>
        /// Deletes CompanyJob records.
        /// </summary>
        /// <param name="pocos">Array of CompanyJobPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("job")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] pocos)
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
