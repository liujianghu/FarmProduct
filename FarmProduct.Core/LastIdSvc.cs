using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

using FarmProduct.Model;

namespace FarmProduct.Core
{
    public class LastIdSvc  
    {
        public static int GetNextTableId(string collectionName)
        {
            return GetNextTableId(collectionName, 1);
        }

        public static int GetNextTableId(string collectionName, int increment)
        {
            int tableId = 1;
            var server = DataBaseHelper.CreateServer();
            MongoDatabase db = server.GetDatabase(Constants.DataBaseName);
            MongoCollection col = db.GetCollection(Constants.LastIdCollectionName);

            var query =Query.EQ("CollectionName", collectionName);
            var sortBy = SortBy.Descending("CollectionName");
            var update = Update
                .Inc("CollectionId", 1)
                .Set("UpdateDate", DateTime.Now);
            var result = col.FindAndModify(
                query,
                sortBy,
                update,
                true
            ).GetModifiedDocumentAs<LastId>();

            if (result == null)
            {
                col.Insert(new LastId
                {
                    CollectionName = collectionName,
                    CollectionId = 1,
                    UpdateDate = DateTime.Now
                });
            }
            else
            {
                tableId = result.CollectionId;
            }

            return tableId;
        }


    }
}
