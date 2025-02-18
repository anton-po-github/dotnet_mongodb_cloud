using mongodb_dotnet_example.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

namespace mongodb_dotnet_example.Services
{
    public class GamesService
    {
        private readonly IMongoCollection<Game> _games;

        public GamesService(IGamesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _games = database.GetCollection<Game>(settings.GamesCollectionName);
        }

        public List<Game> Get() => _games.Find(game => true).ToList();

        public Game Get(string id) => _games.Find(game => game.Id == id).FirstOrDefault();

        public Game Create(Game game)
        {
            _games.InsertOne(game);
            return game;
        }

        public void Update(Game oldGame, Game updatedGame)
        {
            // Creates a filter for all documents with a "name" of "Bagels N Buns"
            var filter = Builders<Game>.Filter
                .Eq(game => game.Id, oldGame.Id);
            // Creates instructions to update the "name" field of the first document
            // that matches the filter
            var update = Builders<Game>.Update
                .Set(game => game.Name, updatedGame.Name)
                .Set(game => game.Price, updatedGame.Price)
                .Set(game => game.Category, updatedGame.Category);

            _games.UpdateOne(filter, update);
        }

        public void Delete(Game gameForDeletion) => _games.DeleteOne(game => game.Id == gameForDeletion.Id);

        public void Delete(string id) => _games.DeleteOne(game => game.Id == id);
    }
}