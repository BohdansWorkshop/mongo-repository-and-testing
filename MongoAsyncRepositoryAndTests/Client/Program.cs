using DataAccess.Models.Collections;
using DataAccess.Repository;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var textModelRepository = new MongoRepository<TextDataModel>(new DataAccess.Models.MongoDbConfiguration() { ConnectionString= "mongodb://localhost:27017", Database = "TestDataStorage"});

            for(int i = 0; i<100; i++)
            {
                var testModel = new TextDataModel()
                {
                    Name = "MyNameIs",
                    Value = "Banksy"
                };

                textModelRepository.New(testModel).Wait();
            }

        }
    }
}
