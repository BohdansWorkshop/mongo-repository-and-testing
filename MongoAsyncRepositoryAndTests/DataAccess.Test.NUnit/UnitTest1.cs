using DataAccess.Models.Collections;
using DataAccess.Repository;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DataAccess.Test.NUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //var textModelRepository = new MongoAsyncRepository<TextDataModel>(new DataAccess.Models.MongoDbConfiguration() { ConnectionString = "mongodb://localhost:27017", Database = "TestDataStorage" });
            var mock = new Mock<MongoAsyncRepository<TextDataModel>> (new DataAccess.Models.MongoDbConfiguration() { ConnectionString = "mongodb://localhost:27017", Database = "TestDataStorage" });

            mock.Setup(rep => rep.GetAll().Result);
            Assert.AreEqual(mock.Object, true);
        }
    }
}