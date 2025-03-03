using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _logic;

        // Default constructor for unit tests 

        /*// from humber advice start
        public ApplicantEducationController(CareerCloudContext context)
        {
            var repository = new EFGenericRepository<ApplicantEducationPoco>(context);
           _logic = new ApplicantEducationLogic(repository);
        }
        // from humber end*/


        // default constructor for unit tests earl start
        public ApplicantEducationController()
        {
            var repository = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repository);
        }

        public ApplicantEducationController(ApplicantEducationLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route("education/{id}")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)] // Success - return 200 OK after creating
        [ProducesResponseType(404)] // Bad Request if data is invalid
        public ActionResult GetApplicantEducation(Guid id)
        {
            ApplicantEducationPoco poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco); // Return 200 OK with all records
        }

        [HttpGet]
        [Route("education")]
        [ProducesResponseType(typeof(List<ApplicantEducationPoco>), 200)]
        public ActionResult<List<ApplicantEducationPoco>> GetAllApplicantEducation()
        {
            List<ApplicantEducationPoco> pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("education")]
        [ProducesResponseType(200)]
        public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("education")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)] // Added NotFound()
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("education")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)] // Added NotFound()
        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}