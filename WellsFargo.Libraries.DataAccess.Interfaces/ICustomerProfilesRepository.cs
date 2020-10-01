using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Libraries.CRMSystem.ORM.Interfaces;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.DataAccess.Interfaces
{
    public interface ICustomerProfilesRepository : IRepository<CustomerProfile, int>
    {
        IEnumerable<CustomerProfile> FindCustomerProfiles(string customerName);
        Task<IEnumerable<ProductReview>> GetProductReviewsByCustomer(int customerProfileId);
        Task<IEnumerable<ProductReview>> GetProductReviewsByProduct(int productId);
    }
}
