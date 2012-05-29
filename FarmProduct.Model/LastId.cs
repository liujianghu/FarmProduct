using System;
using MongoDB.Bson;

namespace FarmProduct.Model
{
    public class LastId
    {
        public BsonObjectId Id { get; set; }

        public string CollectionName { get; set; }

        public int CollectionId { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
