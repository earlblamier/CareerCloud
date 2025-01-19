using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                // Validate Major: Cannot be empty or less than 3 characters (Code: 107)
                if (string.IsNullOrEmpty(poco.Major) || poco.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, "Major cannot be empty or less than 3 characters."));
                }

                // Validate StartDate: Cannot be greater than today (Code: 108)
                if (poco.StartDate > DateTime.Now)
                {
                    exceptions.Add(new ValidationException(108, "StartDate cannot be greater than today."));
                }

                // Validate CompletionDate: Cannot be earlier than StartDate (Code: 109)
                if (poco.CompletionDate < poco.StartDate)
                {
                    exceptions.Add(new ValidationException(109, "CompletionDate cannot be earlier than StartDate."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
