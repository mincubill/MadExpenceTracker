using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Test.Fixture;
using MadExpenceTracker.Core.UseCase;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Test
{
    public class MonthIndexingServiceTest
    {
        [SetUp]
        public void Setup()
        {
            Mock<Connection> mongoConnection = new Mock<Connection>();
            Mock<MongoClient> mongoClient = new Mock<MongoClient>("mongodb://localhost:27017");
            IMongoDatabase mongoDatabase = mongoConnection.Object.GetDatabase(mongoClient.Object);
        }

        [Test]
        public void Test()
        {
            
        }
    }
}
