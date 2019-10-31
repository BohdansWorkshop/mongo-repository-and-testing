using DataAccess.Interfaces;
using DataAccess.Models.Collections;
using DataAccess.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Test.NUnit
{
    public class RepositoryTests
    {
        List<TextDataModel> _mockData;
        public Mock<IMongoRepository<TextDataModel>> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockData = GenerateFakeData();

            _mockRepository = new Mock<IMongoRepository<TextDataModel>>();

            _mockRepository.Setup(x => x.FindAll()).Returns(_mockData);

            _mockRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Guid id) => { return _mockData.Where(x => x.Id == id).FirstOrDefault(); });
            _mockRepository.Setup(x => x.GetPage(It.IsAny<Guid>(), It.IsAny<int>())).Returns((Guid id, int takeCount) => {
                var currentItem = _mockData.FirstOrDefault(x => x.Id == id);
                var indexOfItem = _mockData.IndexOf(currentItem);
                var pagedList = _mockData.Where(x => _mockData.IndexOf(x) >= indexOfItem).Take(takeCount).ToList();
                return pagedList;
            });
        }

        [Test]
        public void DoesReturnRightCollection()
        {
            Assert.AreEqual(_mockRepository.Object.FindAll().ToList(), _mockData);
        }

        [Test]
        public void IsGetByIdWorks()
        {
            Assert.AreEqual(_mockRepository.Object.GetById(_mockData.FirstOrDefault().Id), _mockData.FirstOrDefault());
        }

        [Test]
        public void IsPagingWorks()
        {
            var resultsList = _mockData.Skip(1).Take(2).ToList();
            Assert.AreEqual(_mockRepository.Object.GetPage(_mockData[1].Id, 2), resultsList);
        }

        public List<TextDataModel> GenerateFakeData()
        {
            return new List<TextDataModel>
        {
            new TextDataModel(){Id = Guid.NewGuid(), Name = "Test1", Value = "Test1Value"},
            new TextDataModel(){Id = Guid.NewGuid(), Name = "Test2", Value = "Test2Value"},
            new TextDataModel(){Id = Guid.NewGuid(), Name = "Test3", Value = "Test3Value"},
            new TextDataModel(){Id = Guid.NewGuid(), Name = "Test4", Value = "Test4Value"},
            new TextDataModel(){Id = Guid.NewGuid(), Name = "Test5", Value = "Test5Value"}
        };
        }
    }
}