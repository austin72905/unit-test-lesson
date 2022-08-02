using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        /*
            Mock framework (Download from Nuget)
        
            使用假資料測試時，不再需要自已手動創造類

            只有再需要獨立測試 外部依賴時才使用 (因為測試外部依賴時常常需要手動創造很多Fake類)
        */
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;
        private VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);

        }


        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            //呼叫你要測試的方法還有返回類型
            _fileReader.Setup(fn => fn.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("Error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            //Mock object
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnProcessedVideos_ReturnAStringWithIdOfUnprocessedVideos()
        {
            //Mock object
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>()
            {
                new Video { Id=1 },
                new Video { Id=2 },
                new Video { Id=3 },
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
