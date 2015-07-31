using System;
using System.Collections.Generic;
using MongoDB.Driver;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class MongoRepository<TModel> : IRepository<TModel> where TModel: class, IModel
    {
        private IMongoCollection<TModel> collection;
        private IMongoDatabase database;

        public MongoRepository()
        {
            this.setup(typeof(TModel).Name.Substring(0,  typeof(TModel).Name.LastIndexOf("Model")));
        }

        private void setup(string collection)
        {
            var client = new MongoClient("mongodb://localhost");
            database = client.GetDatabase("uniritter");
            this.collection = database.GetCollection<TModel>(collection);
        }

        public TModel Add(TModel model)
        {
            if (!model.id.HasValue)
            {
                model.id = Guid.NewGuid();
            }
            collection.InsertOneAsync(model).Wait();
            return model;
        }

        public bool Delete(Guid modelId)
        {
            var result = collection.DeleteOneAsync(
                p => p.id == modelId).Result;

            return result.DeletedCount > 0;
        }

        public TModel Update(Guid id, TModel model)
        {
            collection.ReplaceOneAsync(p => p.id == id, model).Wait();

            return model;
        }

        public IEnumerable<TModel> GetAll()
        {
            var data = collection.Find(
                p => true).ToListAsync();
            return data.Result;
        }

        public TModel GetById(Guid id)
        {
            var data = collection.Find(
                p => p.id == id).FirstOrDefaultAsync();
            return data.Result;
        }

        public void Upsert(IEnumerable<TModel> peopleList)
        {
            //collection.UpdateManyAsync()
            /*
             _collection.Update(
    Query.EQ("UUID", thing.UUID),
    Update.Replace(thing),
    UpsertFlags.Upsert
);
             */
            var options = new UpdateOptions {IsUpsert = true};
            foreach (var person in peopleList)
            {
                collection.ReplaceOneAsync(model => model.id == person.id, person, options);
            }
        }
    }
}