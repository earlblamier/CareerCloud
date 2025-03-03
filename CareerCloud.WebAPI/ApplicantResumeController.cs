using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic _logic;

        // Default constructor for unit tests
        public ApplicantResumeController()
        {
            var repository = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repository);
        }

        public ApplicantResumeController(ApplicantResumeLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route("resume/{id}")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantResume(Guid id)
        {
            ApplicantResumePoco poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [Route("resume")]
        [ProducesResponseType(typeof(List<ApplicantResumePoco>), 200)]
        public ActionResult<List<ApplicantResumePoco>> GetAllApplicantResume()
        {
            List<ApplicantResumePoco> pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("resume")]
        [ProducesResponseType(200)]
        public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("resume")]
        [ProducesResponseType(200)]
        public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("resume")]
        [ProducesResponseType(200)]
        public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}


