using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using RoboBank.Customer.Application.Ports;

namespace RoboBank.Customer.Application.Adapters
{
    public class MongoCustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Domain.Customer> _customerCollection;

        public MongoCustomerRepository(IMongoCollection<Domain.Customer> customerCollection)
        {
            _customerCollection = customerCollection;
        }

        public async Task UpdateAsync(Domain.Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Id))
            {
               throw new ArgumentException("Customer Id is missing.");
            }

            await _customerCollection.ReplaceOneAsync(cst => cst.Id == customer.Id, customer, new UpdateOptions {IsUpsert = false});
        }

        public async Task<Domain.Customer> GetByIdAsync(string customerId)
        {
            return await _customerCollection.Find(cst => cst.Id == customerId).FirstOrDefaultAsync();
        }

        public async Task<Domain.Customer> GetByExternalIdAsync(string externalId)
        {
            return await _customerCollection.Find(cst => cst.ExternalId == externalId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Domain.Customer>> SearchAsync(IEnumerable<KeyValuePair<string, string>> criteria, int limit)
        {
            var result = new List<Domain.Customer>();

            var bsonFilter = GetBsonSearchFilter(criteria);

            using (var asyncCursor = await _customerCollection.FindAsync(bsonFilter, new FindOptions<Domain.Customer> { Limit = limit }))
            {
                while (await asyncCursor.MoveNextAsync())
                {
                    result.AddRange(asyncCursor.Current);
                }
            }

            return result;
        }

        private BsonDocument GetBsonSearchFilter(IEnumerable<KeyValuePair<string, string>> criteria)
        {
            var result = new BsonDocument {new BsonElement("AllowsSearch", BsonValue.Create(true))};

            if (criteria != null)
            {
                foreach (var entry in criteria)
                {
                    result.Add(new BsonElement($"Profile.{entry.Key}", BsonValue.Create(entry.Value)));
                }
            }

            return result;
        }
    }
}
