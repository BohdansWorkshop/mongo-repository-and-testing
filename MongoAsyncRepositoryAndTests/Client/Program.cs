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
            var textModelRepository = new MongoAsyncRepository<TextDataModel>(new DataAccess.Models.MongoDbConfiguration() { ConnectionString= "mongodb://localhost:27017", Database = "TestDataStorage"});

            //for(int i = 0; i<100; i++)
            //{
            //    var testModel = new TextDataModel()
            //    {
            //        Name = "MyNameIs",
            //        Value = "Banksy"
            //    };

            //    textModelRepository.Save(testModel).Wait();
            //}

            var model = textModelRepository.GetById(Guid.Parse("7a724c04-5b8d-4e20-bf46-f0579bb14ad8")).Result;
            if(model == null)
            {
                model = new TextDataModel()
                {
                    Id = Guid.Parse("7a724c04-5b8d-4e20-bf46-f0579bb14ad8"),
                    Name = "Hello",
                    Value = "I'm new here"
                };
            }
            else
            {
                model.Name = "Ok, go next";
            }

            textModelRepository.Upsert(model).Wait();

            var nextpage = textModelRepository.GetPage(Guid.Parse("7a724c04-5b8d-4e20-bf46-f0579bb14ad8"), 10).Result.ToList();
   

        }
    }
}
