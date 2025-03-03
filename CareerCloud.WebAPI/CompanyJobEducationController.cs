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
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _logic;

        // Default constructor for unit tests
        public CompanyJobEducationController()
        {
            var repository = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repository);
        }

        public CompanyJobEducationController(CompanyJobEducationLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific CompanyJobEducation by ID.
        /// </summary>
        /// <param name="id">GUID of the CompanyJobEducation</param>
        /// <returns>A single CompanyJobEducationPoco</returns>
        [HttpGet]
        [Route("jobeducation/{id}")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobEducation(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }


        /// <summary>
        /// Gets all CompanyJobEducation records.
        /// </summary>
        /// <returns>List of CompanyJobEducationPoco</returns>
        [HttpGet]
        [Route("jobeducations")]
        [ProducesResponseType(typeof(List<CompanyJobEducationPoco>), 200)]
        public ActionResult<List<CompanyJobEducationPoco>> GetAllCompanyJobEducations()
        {
            return Ok(_logic.GetAll());
        }

        /// <summary>
        /// Creates new CompanyJobEducation records.
        /// </summary>
        /// <param name="pocos">Array of CompanyJobEducationPoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("jobeducation")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            return Ok();
        }

        /// <summary>
        /// Updates existing CompanyJobEducation records.
        /// </summary>
        /// <param name="pocos">Array of CompanyJobEducationPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpPut]
        [Route("jobeducation")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        /// <summary>
        /// Deletes CompanyJobEducation records.
        /// </summary>
        /// <param name="pocos">Array of CompanyJobEducationPoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("jobeducation")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
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
