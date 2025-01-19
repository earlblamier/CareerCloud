/*using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository) { }

        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                // Rule: CompanyName cannot be empty or less than 3 characters
                if (string.IsNullOrEmpty(poco.CompanyName) || poco.CompanyName.Length < 3)
                {
                    exceptions.Add(new ValidationException(105, "CompanyName cannot be empty or less than 3 characters."));
                }

                // Rule: StartMonth must be between 1 and 12
                if (poco.StartMonth < 1 || poco.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(106, "StartMonth must be between 1 and 12."));
                }

                // Rule: EndMonth must be between 1 and 12
                if (poco.EndMonth < 1 || poco.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(107, "EndMonth must be between 1 and 12."));
                }

                // Rule: StartYear cannot be less than 1900
                if (poco.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(108, "StartYear cannot be less than 1900."));
                }

                // Rule: EndYear cannot be less than StartYear
                if (poco.EndYear < poco.StartYear)
                {
                    exceptions.Add(new ValidationException(109, "EndYear cannot be less than StartYear."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}*/


using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyName) || poco.CompanyName.Length <= 2)
                {
                    exceptions.Add(new ValidationException(105, "CompanyName must be greater than 2 characters."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}