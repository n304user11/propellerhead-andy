using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Propellerhead_Andy.Entities;

namespace Propellerhead_Andy.Repositories
{
    public class MongoDbNoteRepository : INoteRepository
    {
        private const string databaseName = "Propeller";
        private const string collectionName = "notes";
        private readonly IMongoCollection<Note> noteCollection;
        private readonly FilterDefinitionBuilder<Note> filterBuilder = Builders<Note>.Filter;

        public MongoDbNoteRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            noteCollection = database.GetCollection<Note>(collectionName);
        }

        public async Task<Note> GetNoteAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await noteCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Note>> GetNotesByCustomerIdAsync(Guid customerId)
        {
            var filter = filterBuilder.Eq(item => item.CustomerId, customerId);
            return await noteCollection.Find(filter).ToListAsync();
        }

        public async Task CreateNoteAsync(Note note)
        {
            await noteCollection.InsertOneAsync(note);
        }

        public async Task UpdateNoteAsync(Note note)
        {
            var filter = filterBuilder.Eq(item => item.Id, note.Id);
            await noteCollection.ReplaceOneAsync(filter, note);
        }

        public async Task DeleteNoteAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await noteCollection.DeleteOneAsync(filter);
        }
    }
}