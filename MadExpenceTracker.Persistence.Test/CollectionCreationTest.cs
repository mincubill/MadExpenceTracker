using MadExpenceTracker.Persistence.MongoDB.Provider;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadExpenceTracker.Persistence.MongoDB.Model;
using Moq;

namespace MadExpenceTracker.Persistence.Test
{
    public class CollectionCreationTest
    {
        private readonly List<string> _collections = new List<string>() { "expence", "income", "amounts", "configuration", "monthIndex" };


        Mock<MongoDBProvider> _provider;
        [SetUp]
        public void Setup()
        {
            _provider = new Mock<MongoDBProvider>();
            _provider.SetupAllProperties();
        }

        [Test]
        public void CreateCollectionsIfNotExistsTest()
        {
            MongoUrl url = new MongoUrl($"mongodb://localhost:27017/MadExpencesTracker");
            MongoClientSettings settings = MongoClientSettings.FromUrl(url);
            MongoClient client = new MongoClient(settings);

            Mock<IAsyncCursor<string>> cursor = new Mock<IAsyncCursor<string>>();
            cursor.Setup(x => x.Current).Returns(_collections);
            cursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);

            Mock<IMongoDatabase> databaseMock = new Mock<IMongoDatabase>();
            databaseMock.SetupGet(x => x.Client).Returns(client);
            databaseMock.Setup(x => x.ListCollectionNames(It.IsAny<ListCollectionNamesOptions>(), It.IsAny<CancellationToken>()))
                .Returns(cursor.Object);

            Mock<IMongoClient> clientMock = new Mock<IMongoClient>();
            clientMock.Setup(c => c.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>()))
                .Returns(databaseMock.Object);

            IMongoDBProvider provider = new MongoDBProvider("mongodb://localhost:27017", "MadExpencesTracker");
            provider.CreateCollectionsIfNotExists();
        }
    }
}
