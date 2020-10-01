using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.ORM.Interfaces
{
    public interface IProductReviewsContext
    {
        IMongoCollection<ProductReview> ProductReviews { get; }
    }
}
