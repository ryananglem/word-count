using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace wordcount.textfilerepo.tests
{
    [TestClass]
    public class RepoTests
    {
        [TestMethod]
        public void construct_repo()
        {
            // arrange
            var chunkSize = 2000;
            var filename = "text.txt";

            // act
            var repo = new Repo(chunkSize, filename);

            // assert
            Assert.IsNotNull(repo);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void fail_to_construct_repo_for_empty_chunk()
        {
            // arrange
            var chunkSize = 0;
            var filename = "text.txt";

            // act
            var repo = new Repo(chunkSize, filename);

            // assert
            Assert.Fail("should produce exception");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void fail_to_construct_repo_for_empty_filename()
        {
            // arrange
            var chunkSize = 2000;
            var filename = "";

            // act
            var repo = new Repo(chunkSize, filename);

            // assert
            Assert.Fail("should produce exception");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")] // because you cant mock filestreams
        [DeploymentItem("text.txt")]
        public void get_chunk_of_text_from_actual_file()
        {
            // arrange
            var repo = new Repo(100, "text.txt");

            // act
            var result = repo.GetChunkOfText(0);

            // assert
            Assert.AreEqual(100, result.Length);
            var text = "Download React v15.3.1";
            Assert.AreEqual(text, result.Substring(0, text.Length));
        }

        [TestMethod]
        [TestCategory("IntegrationTest")] // because you cant mock filestreams
        [DeploymentItem("text.txt")]
        public void dont_read_past_end_of_file()
        {
            // arrange
            var repo = new Repo(100, "text.txt");

            // act
            var result = repo.GetChunkOfText(1100);

            // assert
            Assert.AreEqual(67, result.Length);
        }


    }
}
