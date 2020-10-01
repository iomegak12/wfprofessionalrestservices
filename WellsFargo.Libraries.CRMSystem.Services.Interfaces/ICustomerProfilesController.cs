using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.Services.Interfaces
{
    public interface ICustomerProfilesController
    {
        IActionResult GetCustomerProfiles(string searchString);
        IActionResult GetCustomerProfile(int customerProfileId);
        IActionResult GetCustomerLoyaltyPoint(int customerProfileId);
        IActionResult AddNewCustomerProfile(CustomerProfile newCustomerProfile);
        Task<IActionResult> GetReviewsByCustomer(int customerProfileId);
        Task<IActionResult> GetReviewsByProduct(int productId);
    }
}
