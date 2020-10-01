using System;
using System.Linq;
using WellsFargo.Libraries.CRMSystem.Validations.Interfaces;

namespace WellsFargo.Libraries.CRMSystem.Validations.Impl
{
    public class SearchStringValidation : IDomainValidation<string>
    {
        public bool Validate(string domainType)
        {
            var badKeywords = new string[] { "bad", "worse", "not good" };
            var minLength = 3;

            var validationStatus = !string.IsNullOrEmpty(domainType) &&
                !badKeywords.Contains(domainType) &&
                domainType.Length >= minLength;

            return validationStatus;
        }
    }
}
