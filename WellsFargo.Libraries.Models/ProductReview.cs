using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WellsFargo.Libraries.Models
{
    public class ProductReview
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ReviewId { get; set; }
        public int CustomerProfileId { get; set; }
        public int ProductId { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rank { get; set; }
        public string Remarks { get; set; }
    }
}
