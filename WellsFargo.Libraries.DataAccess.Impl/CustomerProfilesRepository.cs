using Microsoft.EntityFrameworkCore.ChangeTracking;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellsFargo.Libraries.CRMSystem.ORM.Interfaces;
using WellsFargo.Libraries.DataAccess.Interfaces;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.DataAccess.Impl
{
    public class CustomerProfilesRepository : ICustomerProfilesRepository
    {
        private const string INVALID_ENTITY_KEY = "Invalid Customer Profile ID Specified!";
        private const string INVALID_SEARCH_STRING = "Invalid Customer Search String Specified!";
        private const string INVALID_ENTITY = "Invalid Customer Profile Details Specified!";

        private readonly ICustomerProfilesContext customerProfilesContext = default(ICustomerProfilesContext);
        private readonly IProductReviewsContext productReviewsContext = default(IProductReviewsContext);
        public CustomerProfilesRepository(ICustomerProfilesContext customerProfilesContext, IProductReviewsContext productReviewsContext)
        {
            if (customerProfilesContext == default(ICustomerProfilesContext))
                throw new ArgumentNullException(nameof(customerProfilesContext));

            if (productReviewsContext == default(IProductReviewsContext))
                throw new ArgumentNullException(nameof(productReviewsContext));

            this.customerProfilesContext = customerProfilesContext;
            this.productReviewsContext = productReviewsContext;
        }

        public bool AddNewEntity(CustomerProfile entityType)
        {
            var status = default(bool);
            var isParameterValid = entityType != default(CustomerProfile) &&
                entityType.CustomerProfileId != default(int);

            if (!isParameterValid)
                throw new ArgumentException(INVALID_ENTITY);

            var newEntity = this.customerProfilesContext.CustomerProfiles.Add(entityType);

            status = this.customerProfilesContext.CommitChanges() && newEntity != default(EntityEntry<CustomerProfile>);

            return status;
        }

        public bool DeleteEntity(int entityKey)
        {
            var isParameterValid = entityKey != default(int);
            var status = default(bool);

            var filteredEntity = this.GetEntityByKey(entityKey);
            this.customerProfilesContext.CustomerProfiles.Remove(filteredEntity);
            status = this.customerProfilesContext.CommitChanges();

            return status;
        }

        public void Dispose()
        {
            if (this.customerProfilesContext != default(ICustomerProfilesContext))
                this.customerProfilesContext.Dispose();
        }

        public IEnumerable<CustomerProfile> FindCustomerProfiles(string customerName)
        {
            var isParmaterValid = !string.IsNullOrEmpty(customerName);

            if (!isParmaterValid)
                throw new ArgumentException(INVALID_SEARCH_STRING);

            var filteredCustomerProfiles =
                this
                    .customerProfilesContext
                    .CustomerProfiles
                    .Where(customerProfile => customerProfile.FullName.Contains(customerName))
                    .ToList();

            return filteredCustomerProfiles;
        }

        public IEnumerable<CustomerProfile> GetAllEntities()
        {
            var customerProfiles = this.customerProfilesContext.CustomerProfiles.ToList();

            return customerProfiles;
        }

        public CustomerProfile GetEntityByKey(int entityKey)
        {
            if (entityKey == default(int))
                throw new ArgumentException(INVALID_ENTITY_KEY);

            var filteredCustomerProfile =
                this
                    .customerProfilesContext
                    .CustomerProfiles
                    .Where(customerProfile => customerProfile.CustomerProfileId == entityKey)
                    .FirstOrDefault();

            return filteredCustomerProfile;
        }

        public async Task<IEnumerable<ProductReview>> GetProductReviewsByCustomer(int customerProfileId)
        {
            var result = await this.productReviewsContext
                                   .ProductReviews
                                   .FindAsync(productReview => productReview.CustomerProfileId == customerProfileId);

            var productReviews = await result.ToListAsync();

            return productReviews;
        }

        public async Task<IEnumerable<ProductReview>> GetProductReviewsByProduct(int productId)
        {
            var result = await this.productReviewsContext
                                   .ProductReviews
                                   .FindAsync<ProductReview>(review => review.ProductId == productId);

            var productReviews = await result.ToListAsync();

            return productReviews;
        }

        public CustomerProfile UpdateEntity(CustomerProfile existingEntityType)
        {
            var isParameterValid = existingEntityType != default(CustomerProfile) && existingEntityType.CustomerProfileId != default(int);

            if (!isParameterValid)
                throw new ArgumentException(INVALID_ENTITY);

            var updatedEntityTracking = this.customerProfilesContext.CustomerProfiles.Update(existingEntityType);
            this.customerProfilesContext.CommitChanges();

            return updatedEntityTracking.Entity;
        }
    }
}
