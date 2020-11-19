using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Bliss.Data.Provider
{
    public class MongoDataProvider : IDataProvider
    {
        readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        readonly IMongoCollection<SavedQuestion> _questions;


        public MongoDataProvider(IDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);

            _questions = _database.GetCollection<SavedQuestion>(settings.QuestionsCollectionName);
        }


        public async Task<IList<SavedQuestion>> Get(int limit, int offset, String filter)
        {
            var results = (string.IsNullOrWhiteSpace(filter) ?
                await _questions.FindAsync(q => true) :
                await _questions.FindAsync(q => q.QuestionText.Contains(filter) || q.Choices.Any(q => q.Choice.Contains(filter)))).ToList();

            if (offset > 0 && offset < results.Count)
            {
                results = results.Skip(offset).ToList();
            }
            if (limit > 0 && limit <= results.Count)
            {
                results = results.Take(limit).ToList();
            }

            return results;
        }

        public async Task DropCollection()
        {
            await _database.DropCollectionAsync(_questions.CollectionNamespace.CollectionName);
        }

        public async Task<SavedQuestion> GetById(string id)
        {
            return (await _questions.FindAsync(q => q.Id == id)).FirstOrDefault();
        }

        public async Task<string> Save(SavedQuestion question)
        {

            if (question.Id == null)
            {
                await _questions.InsertOneAsync(question);
            }
            else
            {
                var filterDefinition = new FilterDefinitionBuilder<SavedQuestion>().Eq("Id", question.Id);
                await _questions.ReplaceOneAsync(filterDefinition, question);
            }

            return question.Id;
        }
    }
}
