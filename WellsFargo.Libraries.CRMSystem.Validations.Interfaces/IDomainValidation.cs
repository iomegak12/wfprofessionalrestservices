using System;

namespace WellsFargo.Libraries.CRMSystem.Validations.Interfaces
{
    public interface IDomainValidation<DomainType>
    {
        bool Validate(DomainType domainType);
    }
}
