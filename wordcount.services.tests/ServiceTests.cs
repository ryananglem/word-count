using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace wordcount.services.tests
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public void construct_service()
        {
            // arrange
            var mockDocumentRepo = new Mock<IDocumentRepo>();

            // act
            var service = new Service(mockDocumentRepo.Object);

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
            Assert.IsNull(service);
        }

        [TestMethod]
        public void get_words_from_text()
        {
            // arrange
            var mockDocumentRepo = new Mock<IDocumentRepo>();
            var service = new Service(mockDocumentRepo.Object);
            mockDocumentRepo.Setup(x => x.GetChunkOfText(It.Is<long>(s => s == 0)))
                .Returns("the quick brown fox is the text to use when testing brown things");
            mockDocumentRepo.Setup(x => x.GetChunkOfText(It.Is<long>(s => s == 57)))
                .Returns("things");

            // act
            var result = service.CountWordsInDocument();

            // assert
            Assert.AreEqual(11, result.Count);
            Assert.AreEqual(2, result["the"]);
            Assert.AreEqual(2, result["brown"]);
            Assert.AreEqual(1, result["quick"]);
        }
    }
}
