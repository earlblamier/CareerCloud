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
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        // Default constructor for unit tests
        public SystemLanguageCodeController()
        {
            var repository = new EFGenericRepository<SystemLanguageCodePoco>();
            _logic = new SystemLanguageCodeLogic(repository);
        }

        public SystemLanguageCodeController(SystemLanguageCodeLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Gets a specific SystemLanguageCode by Code.
        /// </summary>
        /// <param name="code">Code of the SystemLanguageCode</param>
        /// <returns>A single SystemLanguageCodePoco</returns>
        [HttpGet]
        [Route("languagecode/{code}")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        [ProducesResponseType(404)]
        //public ActionResult<SystemLanguageCodePoco> GetSystemLanguageCode(string code)
        public ActionResult GetSystemLanguageCode(string code)
        {
            var poco = _logic.Get(code);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        /// <summary>
        /// Gets all SystemLanguageCode records.
        /// </summary>
        /// <returns>List of SystemLanguageCodePoco</returns>
        [HttpGet]
        [Route("languagecodes")]
        [ProducesResponseType(typeof(List<SystemLanguageCodePoco>), 200)]
        public ActionResult<List<SystemLanguageCodePoco>> GetAllSystemLanguageCodes()
        {
            return Ok(_logic.GetAll());
        }

        /// <summary>
        /// Creates new SystemLanguageCode records.
        /// </summary>
        /// <param name="pocos">Array of SystemLanguageCodePoco objects</param>
        /// <returns>HTTP 201 Created</returns>
        [HttpPost]
        [Route("languagecode")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Add(pocos);          
            return Ok();
        }

        /// <summary>
        /// Updates existing SystemLanguageCode records.
        /// </summary>
        /// <param name="pocos">Array of SystemLanguageCodePoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpPut]
        [Route("languagecode")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Update(pocos);
            return Ok();
        }

        /// <summary>
        /// Deletes SystemLanguageCode records.
        /// </summary>
        /// <param name="pocos">Array of SystemLanguageCodePoco objects</param>
        /// <returns>HTTP 204 No Content</returns>
        [HttpDelete]
        [Route("languagecode")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
        {
            if (pocos == null)
            {
                return BadRequest();
            }

            _logic.Delete(pocos);
            return Ok();  // Ensures that the return type is ActionResult and compatible with unit test
        }

    }
}



