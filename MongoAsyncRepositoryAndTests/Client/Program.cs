using DataAccess.Models.Collections;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var textModelRepository = new MongoRepository<TextDataModel>(new DataAccess.Models.MongoDbConfiguration() { ConnectionString= "mongodb://localhost:27017", Database = "TestDataStorage"});

            //for(int i = 0; i<100; i++)
            //{
            //    var testModel = new TextDataModel()
            //    {
            //        Name = "MyNameIs",
            //        Value = "Banksy"
            //    };

            //    textModelRepository.Save(testModel).Wait();
            //}


            var nextpage = textModelRepository.GetPage(Guid.Parse("7a724c04-5b8d-4e20-bf46-f0579bb14ad8"), 10).Result.ToList();
   

        }
    }
}
