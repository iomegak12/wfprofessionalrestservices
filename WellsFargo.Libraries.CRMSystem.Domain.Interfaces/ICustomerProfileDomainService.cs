using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.Domain.Interfaces
{
    public interface ICustomerProfileDomainService : IDisposable
    {
        IEnumerable<CustomerProfile> GetCustomerProfiles(string searchString);
        CustomerProfile GetCustomerProfile(int customerProfileId);
        int GetLoyaltyPoints(int customerProfileId);
        bool AddNewCustomerProfile(CustomerProfile customerProfile);
        Task<IEnumerable<ProductReview>> GetProductReviews(int productId = default(int), int customerId = default(int));
    }
}
