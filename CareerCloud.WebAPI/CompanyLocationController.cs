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
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic _logic;

        // Default constructor for unit tests
        public CompanyLocationController()
        {
            var repository = new EFGenericRepository<CompanyLocationPoco>();
            _logic = new CompanyLocationLogic(repository);
        }

        public CompanyLocationController(CompanyLocationLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route("location/{id}")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyLocation(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }


        [HttpGet]
        [Route("locations")]
        [ProducesResponseType(typeof(List<CompanyLocationPoco>), 200)]
        public ActionResult<List<CompanyLocationPoco>> GetAllCompanyLocations()
        {
            return Ok(_logic.GetAll());
        }

        [HttpPost]
        [Route("location")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);
            //return CreatedAtAction(nameof(GetAllCompanyLocations), null);
            return Ok();
        }

        [HttpPut]
        [Route("location")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("location")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
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