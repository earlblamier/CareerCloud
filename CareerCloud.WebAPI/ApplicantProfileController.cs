using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantProfileLogic _logic;

        // Default constructor for unit tests
        public ApplicantProfileController()
        {
            var repository = new EFGenericRepository<ApplicantProfilePoco>();
            _logic = new ApplicantProfileLogic(repository);
        }

        public ApplicantProfileController(ApplicantProfileLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route("profile/{id}")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantProfile(Guid id)
        {
            ApplicantProfilePoco poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(typeof(List<ApplicantProfilePoco>), 200)]
        public ActionResult<List<ApplicantProfilePoco>> GetAllApplicantProfile()
        {
            List<ApplicantProfilePoco> pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("profile")]
        [ProducesResponseType(200)]
        public ActionResult PostApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("profile")]
        [ProducesResponseType(200)]
        public ActionResult PutApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("profile")]
        [ProducesResponseType(200)]
        public ActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}