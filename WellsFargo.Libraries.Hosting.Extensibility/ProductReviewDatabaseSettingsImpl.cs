using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellsFargo.Libraries.CRMSystem.ORM.Interfaces;

namespace WellsFargo.Libraries.Hosting.Extensibility
{
    public class ProductReviewDatabaseSettingsImpl : IProductReviewDatabaseSettings
    {
        private string connectionString;
        private string databaseName;
        private string collectionName;

        public ProductReviewDatabaseSettingsImpl()
        {
            this.connectionString = Environment.GetEnvironmentVariable("MongoDBConnectionString");
            this.databaseName = Environment.GetEnvironmentVariable("MongoDBName");
            this.collectionName = Environment.GetEnvironmentVariable("MongoDBCollectionName");
        }

        public string ConnectionString => this.connectionString;

        public string DatabaseName => this.databaseName;

        public string CollectionName => this.collectionName;
    }
}
