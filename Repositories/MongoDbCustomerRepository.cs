using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Propellerhead_Andy.Entities;

namespace Propellerhead_Andy.Repositories
{
    public class MongoDbCustomerRepository : ICustomerRepository
    {
        private const string databaseName = "Propeller";
        private const string collectionName = "customers";
        private readonly IMongoCollection<Customer> customerCollection;
        private readonly FilterDefinitionBuilder<Customer> filterBuilder = Builders<Customer>.Filter;
        public MongoDbCustomerRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            customerCollection = database.GetCollection<Customer>(collectionName);
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await customerCollection.InsertOneAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await customerCollection.DeleteOneAsync(filter);
        }

        public async Task<Customer> GetCustomerAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await customerCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomerByFieldAsync(Status status = Status.None, string name = null, string contactNumber = null)
        {
            FilterDefinition<Customer> filter = null;
            if (status != Status.None)
            {
                filter = filterBuilder.Eq(item => item.Status, status);
            }
            if (name != null)
            {
                filter = filter != null
                ? (filter & filterBuilder.Eq(item => item.Name, name))
                : filterBuilder.Eq(item => item.Name, name);
            }
            if (contactNumber != null)
            {
                filter = filter != null
                ? (filter & filterBuilder.Eq(item => item.ContactNumber, contactNumber))
                : filterBuilder.Eq(item => item.ContactNumber, contactNumber);
            }

            if (filter is null)
            {
                return Enumerable.Empty<Customer>();
            }

            return await customerCollection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(string orderBy = null)
        {
            if (orderBy is null)
            {
                return await customerCollection.Find(new BsonDocument()).ToListAsync();
            }
            else
            {
                var order = Builders<Customer>.Sort.Descending(orderBy);
                return await customerCollection.Find(new BsonDocument()).Sort(order).ToListAsync();
            }
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var filter = filterBuilder.Eq(item => item.Id, customer.Id);
            await customerCollection.ReplaceOneAsync(filter, customer);
        }
    }
}