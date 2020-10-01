using System;
using System.Collections.Generic;
using System.Text;

namespace WellsFargo.Libraries.CRMSystem.ORM.Interfaces
{
    public interface IProductReviewDatabaseSettings
    {
        public string ConnectionString { get; }
        public string DatabaseName { get; }
        public string CollectionName { get; }
    }
}
