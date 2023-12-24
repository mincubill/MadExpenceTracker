using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.Test
{
    public class CollectionCreationTest
    {
        private Connection _mongoConnection;
        private MongoClient _mongoClient;

        [SetUp]
        public void Setup()
        {
            _mongoConnection = new Connection();
            _mongoClient = _mongoConnection.GetClient();
        }

        [Test]
        public void CreateCollection()
        {
            _mongoConnection.CreateCollectionsIfNotExists(_mongoClient);
        }
    }
}
