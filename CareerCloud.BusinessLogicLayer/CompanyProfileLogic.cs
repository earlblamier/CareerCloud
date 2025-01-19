using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// solved - hard error issue

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                // Validate ContactPhone
                if (string.IsNullOrEmpty(poco.ContactPhone)) // Ensured this check for null or empty phone number remains
                {
                    exceptions.Add(new ValidationException(601, "Phone number cannot be empty.")); // Confirmed code 601 for empty phone
                }
                else
                {
                    // Ensure the phone number matches the required format
                    string phonePattern = @"^\d{3}-\d{3}-\d{4}$"; // Adjusted pattern to validate strictly (123-456-7890 format)
                    if (!Regex.IsMatch(poco.ContactPhone, phonePattern))
                    {
                        exceptions.Add(new ValidationException(601, $"Phone number {poco.ContactPhone} is not in the required format.")); // Ensured the code 601 is consistent for invalid format
                    }
                }

                // Validate CompanyWebsite
                if (string.IsNullOrEmpty(poco.CompanyWebsite)) // Ensured this check for null or empty website remains
                {
                    exceptions.Add(new ValidationException(600, "Company website cannot be empty.")); // Confirmed code 600 for empty website
                }
                else
                {
                    string[] validExtensions = { ".ca", ".com", ".biz" }; // Declared valid extensions to check
                    bool isValidExtension = validExtensions.Any(ext => poco.CompanyWebsite.EndsWith(ext, StringComparison.OrdinalIgnoreCase)); // Changed to explicitly validate extensions

                    if (!isValidExtension)
                    {
                        exceptions.Add(new ValidationException(600, $"Company website {poco.CompanyWebsite} must end with one of the following extensions: .ca, .com, .biz")); // Confirmed code 600 for invalid extension
                    }
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions); // Aggregates all validation exceptions and throws them
            }
        }


    }
}
