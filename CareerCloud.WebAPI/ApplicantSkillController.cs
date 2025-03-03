using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic _logic;

        // Default constructor for unit tests
        public ApplicantSkillController()
        {
            var repository = new EFGenericRepository<ApplicantSkillPoco>();
            _logic = new ApplicantSkillLogic(repository);
        }

        public ApplicantSkillController(ApplicantSkillLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route("skill/{id}")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantSkill(Guid id)
        {
            ApplicantSkillPoco poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [Route("skill")]
        [ProducesResponseType(typeof(List<ApplicantSkillPoco>), 200)]
        public ActionResult<List<ApplicantSkillPoco>> GetAllApplicantSkill()
        {
            List<ApplicantSkillPoco> pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("skill")]
        [ProducesResponseType(200)]
        public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            _logic.Add(pocos);
            return Ok();
        }

        [HttpPut]
        [Route("skill")]
        [ProducesResponseType(200)]
        public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            _logic.Update(pocos);
            return Ok();
        }

        [HttpDelete]
        [Route("skill")]
        [ProducesResponseType(200)]
        public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            _logic.Delete(pocos);
            return Ok();
        }
    }
}


