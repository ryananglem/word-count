using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace wordcount.services.tests
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public void construct_service()
        {
            // arrange
            var repo = new Mock<IRepo>();

            // act
            var service = new Service(repo.Object);

            // assert
            Assert.IsNotNull(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fail_to_construct_service()
        {
            // arrange   
            // act
            var service = new Service(null);

            // assert
            Assert.Fail("repo is null");
        }

        [TestMethod]
        public void get_words_from_text()
        {
            // arrange
            var repo = new Mock<IRepo>();
            var service = new Service(repo.Object);
            repo.Setup(x => x.GetChunkOfText(It.Is<int>(s => s == 0)))
                .Returns("the quick brown fox is the text to use when testing brown things");
            repo.Setup(x => x.GetChunkOfText(It.Is<int>(s => s == 57)))
                .Returns("things");

            // act
            var result = service.GetWordList();

            // assert
            Assert.AreEqual(11, result.Count);
            Assert.AreEqual(2, result["the"]);
            Assert.AreEqual(2, result["brown"]);
            Assert.AreEqual(1, result["quick"]);

        }
    }
}
