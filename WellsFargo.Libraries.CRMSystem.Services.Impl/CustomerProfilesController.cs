using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using WellsFargo.Libraries.CRMSystem.Domain.Interfaces;
using WellsFargo.Libraries.CRMSystem.Services.Interfaces;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.Services.Impl
{
    /// <summary>
    /// Customers API Controller
    /// </summary>
    [Route("api/customer-profiles")]
    [ApiController]
    //[Authorize]
    public class CustomerProfilesController : ControllerBase, ICustomerProfilesController
    {
        private ICustomerProfileDomainService customerProfileDomainService = default;

        /// <summary>
        /// Primary Constructor which depends on Customer Profile Domain Services
        /// </summary>
        /// <param name="customerProfileDomainService"></param>
        public CustomerProfilesController(ICustomerProfileDomainService customerProfileDomainService)
        {
            if (customerProfileDomainService == default(ICustomerProfileDomainService))
                throw new ArgumentNullException(nameof(customerProfileDomainService));

            this.customerProfileDomainService = customerProfileDomainService;
        }


        /// <summary>
        /// Adds a New Customer Profile to the CRM System
        /// </summary>
        /// <param name="newCustomerProfile">New Customer Profile</param>
        /// <returns>Added Customer Record</returns>
        [HttpPost]
        [Route("")]
        public IActionResult AddNewCustomerProfile([FromBody] CustomerProfile newCustomerProfile)
        {
            var status = default(bool);
            var addedCustomerRecord = default(CustomerProfile);

            try
            {
                status = this.customerProfileDomainService.AddNewCustomerProfile(newCustomerProfile);

                if (!status)
                    return new BadRequestResult();

                addedCustomerRecord = newCustomerProfile;
            }
            catch (Exception exceptionObject)
            {
                EventLog.WriteEntry("Application", "Error Occurred ... " + exceptionObject.Message, EventLogEntryType.Error);

                throw;
            }

            return Ok(addedCustomerRecord);
        }

        /// <summary>
        /// Get Customers' Loyalty Points by Customer Id
        /// </summary>
        /// <param name="customerProfileId">Customer Profile Id</param>
        /// <returns>Loyalty Points</returns>
        [HttpGet]
        [Route("loyalty-points/{customerProfileId}")]
        public IActionResult GetCustomerLoyaltyPoint(int customerProfileId)
        {
            var loyaltyPoints = default(int);

            try
            {
                loyaltyPoints = this.customerProfileDomainService.GetLoyaltyPoints(customerProfileId);
            }
            catch (Exception exceptionObject)
            {
                EventLog.WriteEntry("Application", "Error Occurred ... " + exceptionObject.Message, EventLogEntryType.Error);

                throw;
            }

            return Ok(loyaltyPoints);
        }

        /// <summary>
        /// Gets Complete Customer Profile by Customer ID
        /// </summary>
        /// <param name="customerProfileId">Customer Profile ID</param>
        /// <returns>Complete Customer Profile</returns>
        [HttpGet]
        [Route("details/{customerProfileId}")]
        public IActionResult GetCustomerProfile(int customerProfileId)
        {
            var filteredCustomerProfile = default(CustomerProfile);

            try
            {
                filteredCustomerProfile = this.customerProfileDomainService.GetCustomerProfile(customerProfileId);

                if (filteredCustomerProfile == default(CustomerProfile))
                    return NotFound();
            }
            catch (Exception exceptionObject)
            {
                EventLog.WriteEntry("Application", "Error Occurred ... " + exceptionObject.Message, EventLogEntryType.Error);

                throw;
            }

            return Ok(filteredCustomerProfile);
        }

        /// <summary>
        /// Searches Customer Profiles by Customer Name
        /// </summary>
        /// <param name="searchString">Search String</param>
        /// <returns>Filtered Customer Profiles</returns>
        [HttpGet]
        [Route("search/{searchString}")]
        public IActionResult GetCustomerProfiles(string searchString)
        {
            var customerProfiles = default(IEnumerable<CustomerProfile>);

            try
            {
                customerProfiles = this.customerProfileDomainService.GetCustomerProfiles(searchString);

            }
            catch (Exception exceptionObject)
            {
                EventLog.WriteEntry("Application", "Error Occurred ... " + exceptionObject.Message, EventLogEntryType.Error);

                throw;
            }

            return Ok(customerProfiles);
        }

        /// <summary>
        /// Gets Product Reviews by Customer ID
        /// </summary>
        /// <param name="customerProfileId">Customer Profile ID</param>
        /// <returns>List of Product Reviews</returns>
        [HttpGet]
        [Route("reviews/customer/{customerProfileId}")]
        public async Task<IActionResult> GetReviewsByCustomer(int customerProfileId)
        {
            var productReviews = default(IEnumerable<ProductReview>);
            var result = default(IActionResult);

            try
            {
                productReviews = await this.customerProfileDomainService.GetProductReviews(customerId: customerProfileId);

                result = Ok(productReviews);
            }
            catch (Exception exceptionObject)
            {
                EventLog.WriteEntry("Application", "Error Occurred ... " + exceptionObject.Message, EventLogEntryType.Error);

                throw;
            }

            return result;
        }

        /// <summary>
        /// Gets Product Reviews by Product ID
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns>List of Product Reviews</returns>
        [HttpGet]
        [Route("reviews/product/{productId}")]
        public async Task<IActionResult> GetReviewsByProduct(int productId)
        {
            var productReviews = default(IEnumerable<ProductReview>);
            var result = default(IActionResult);

            try
            {
                productReviews = await this.customerProfileDomainService.GetProductReviews(productId: productId);

                result = Ok(productReviews);
            }
            catch (Exception exceptionObject)
            {
                EventLog.WriteEntry("Application", "Error Occurred ... " + exceptionObject.Message, EventLogEntryType.Error);

                throw;
            }

            return result;
        }
    }
}
