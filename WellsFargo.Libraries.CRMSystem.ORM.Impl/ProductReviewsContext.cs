using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WellsFargo.Libraries.CRMSystem.ORM.Interfaces;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.ORM.Impl
{
    public class ProductReviewsContext : IProductReviewsContext
    {
        private IProductReviewDatabaseSettings productReviewDatabaseSettings = default(IProductReviewDatabaseSettings);
        private IMongoCollection<ProductReview> productReviews = default(IMongoCollection<ProductReview>);

        public ProductReviewsContext(IProductReviewDatabaseSettings productReviewDatabaseSettings)
        {
            if (productReviewDatabaseSettings == default(IProductReviewDatabaseSettings))
            {
                throw new ArgumentNullException(nameof(productReviewDatabaseSettings));
            }

            this.productReviewDatabaseSettings = productReviewDatabaseSettings;

            var mongoClient = new MongoClient(this.productReviewDatabaseSettings.ConnectionString);
            var database = mongoClient.GetDatabase(this.productReviewDatabaseSettings.DatabaseName);

            this.productReviews = database.GetCollection<ProductReview>(this.productReviewDatabaseSettings.CollectionName);
        }

        public IMongoCollection<ProductReview> ProductReviews
        {
            get
            {
                return this.productReviews;
            }
        }
    }
}
