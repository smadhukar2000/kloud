using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DACLayer.Interfaces;
using Kloud.Models;
using KloudAZFunctions.Implementations;
using KloudAZFunctions.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace KloudAZFunctionsTests
{
    [TestClass]
    public class TransactionBLTests
    {
        private Mock<IDACLayer> _mock = new Mock<IDACLayer>();
        private Task<string> GetJsonFromModels()
        {
            List<CarOwner> carOwners = this.Setup();
            Task<string>  task = Task.Factory.StartNew(() => JsonConvert.SerializeObject(carOwners));
            task.Wait();
            return task;
        }
        private List<CarOwner> Setup()
        {
            List<CarOwner> carOwners = new List<CarOwner>();
            carOwners.Add(new CarOwner{Name = "Will",
                Cars = new List<Car>
                {
                    new Car { Brand = "BMW", Colour = "Red" },
                    new Car { Brand = "Toyota", Colour = "Black" }
                }});
            carOwners.Add(new CarOwner
            {
                Name = "James",
                Cars = new List<Car>
                {
                    new Car { Brand = "Mercedes", Colour = "Green" },
                    new Car { Brand = "BMW", Colour = "Black" }
                }
            });
            carOwners.Add(new CarOwner
            {
                Name = "Oscar",
                Cars = new List<Car>
                {
                    new Car { Brand = "Toyota", Colour = "Green" },
                    new Car { Brand = "BMW", Colour = "White" }
                }
            });

            return carOwners;
        }
        [TestMethod]
        public void GetGroupedAndOrderedDataTest()
        {
            // arrange  
            int keyCount = 3;
            List<string> brands = new List<string> {"BMW", "Mercedes", "Toyota" };
            Dictionary<string, List<string>> orderedExpected = new Dictionary<string, List<string>>();
            orderedExpected.Add("BMW", new List<string> { "James", "Will", "Oscar" });
            orderedExpected.Add("Mercedes", new List<string> { "James"});
            orderedExpected.Add("Toyota", new List<string> { "Will", "Oscar" });

            List <CarOwner>  carOwners = this.Setup();
            ITransactionBL transaction = new TransactionBL(_mock.Object);

            //act
            IDictionary<string, List<string>> results = transaction.GetGroupedAndOrderedData(carOwners);

            // assert
            Assert.AreEqual(keyCount, results.Keys.Count);
            Assert.IsTrue(brands.SequenceEqual(results.Keys));

            // check if all sequence are same
            foreach(var result in results)
            {               
                Assert.IsTrue(orderedExpected[result.Key].SequenceEqual(result.Value));
            }
        }

        [TestMethod]
        public void GetListOfModelsFromUriTest()
        {
            // Arrange
            List<CarOwner> expected = this.Setup();           
            _mock.Setup(x => x.GetDataFromUriAsync(It.IsAny<string>())).Returns(this.GetJsonFromModels());
            ITransactionBL transaction = new TransactionBL(_mock.Object);

            //Act
            List<CarOwner>  actual = transaction.GetListOfModelsFromUri<CarOwner>(string.Empty);

            //Assert
            for(int i = 0; i < actual.Count; ++i)
            {
                Assert.AreEqual(expected[i].Name, actual[i].Name);
                for (int x = 0; x < actual[i].Cars.Count; ++x)
                {
                    Assert.IsTrue(new CarComparer().Equals(actual[i].Cars[x], expected[i].Cars[x]));
                }
            }
        }
    }
}
