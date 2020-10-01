using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WellsFargo.Libraries.CRMSystem.Domain.Interfaces;
using WellsFargo.Libraries.CRMSystem.Validations.Interfaces;
using WellsFargo.Libraries.DataAccess.Interfaces;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.Domain.Impl
{
    public class CustomerProfileDomainService : ICustomerProfileDomainService
    {
        private ICustomerProfilesRepository customerProfilesRepository = default(ICustomerProfilesRepository);
        private IDomainValidation<string> searchStringValidation = default(IDomainValidation<string>);

        private const string INVALID_SEARCH_STRING = "Invalid Search String Specified!";
        private const string INVALID_CUSTOMER_PROFILE_ID = "Invalid Customer Profile ID Specified!";
        private const string INVALID_CUSTOMER_PROFILE = "Invalid Customer Profile Details Specified for a New Entry!";
        private const string INVALID_CUSTOMER_PRODUCT_ID = "Invalid Customer (or) Product ID Specified!";

        public CustomerProfileDomainService(ICustomerProfilesRepository customerProfilesRepository, IDomainValidation<string> searchStringValidation)
        {
            if (customerProfilesRepository == default(ICustomerProfilesRepository))
                throw new ArgumentNullException(nameof(customerProfilesRepository));

            if (searchStringValidation == default(IDomainValidation<string>))
                throw new ArgumentNullException(nameof(searchStringValidation));

            this.customerProfilesRepository = customerProfilesRepository;
            this.searchStringValidation = searchStringValidation;
        }

        public bool AddNewCustomerProfile(CustomerProfile customerProfile)
        {
            var isParameterValid = customerProfile != default(CustomerProfile);

            if (!isParameterValid)
                throw new ApplicationException(INVALID_CUSTOMER_PROFILE);

            var status = this.customerProfilesRepository.AddNewEntity(customerProfile);

            return status;
        }

        public void Dispose()
        {
            if (this.customerProfilesRepository != default(ICustomerProfilesRepository))
                this.customerProfilesRepository.Dispose();
        }

        public CustomerProfile GetCustomerProfile(int customerProfileId)
        {
            var isParameterValid = customerProfileId != default(int);

            if (!isParameterValid)
                throw new ApplicationException(INVALID_CUSTOMER_PROFILE_ID);

            var filteredCustomerProfile = this.customerProfilesRepository.GetEntityByKey(customerProfileId);

            return filteredCustomerProfile;
        }

        public IEnumerable<CustomerProfile> GetCustomerProfiles(string searchString)
        {
            var validationStatus = this.searchStringValidation.Validate(searchString);

            if (!validationStatus)
                throw new ApplicationException(INVALID_SEARCH_STRING);

            var filteredCustomerProfiles = this.customerProfilesRepository.FindCustomerProfiles(searchString);

            return filteredCustomerProfiles;
        }

        public int GetLoyaltyPoints(int customerProfileId)
        {
            var isParameterValid = customerProfileId != default(int);

            if (!isParameterValid)
                throw new ApplicationException(INVALID_CUSTOMER_PROFILE_ID);

            var filteredCustomerProfile = this.customerProfilesRepository.GetEntityByKey(customerProfileId);
            var loyaltyPoints = default(int);

            if (filteredCustomerProfile == default(CustomerProfile))
                loyaltyPoints = filteredCustomerProfile.LoyaltyPoint;

            return loyaltyPoints;
        }

        public async Task<IEnumerable<ProductReview>> GetProductReviews(int productId = 0, int customerId = 0)
        {
            var isParameterValid = productId != default(int) || customerId != default(int);

            if (!isParameterValid)
            {
                throw new ArgumentException(INVALID_CUSTOMER_PRODUCT_ID);
            }

            var productReviews = default(IEnumerable<ProductReview>);

            if (productId != default(int))
            {
                productReviews = await this.customerProfilesRepository.GetProductReviewsByProduct(productId);
            }
            else if (customerId != default(int))
            {
                productReviews = await this.customerProfilesRepository.GetProductReviewsByCustomer(customerId);
            }

            return productReviews;
        }
    }
}
