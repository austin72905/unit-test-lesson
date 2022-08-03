using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloaderInstaller_DownloadFails_ReturnFalse()
        {
            //你外部傳的一定要是"","" 才會讓他跑Throws WebException
            //_fileDownloader.Setup(fd => fd.DownloadFile("", "")).Throws<WebException>();

            // 所以要這樣寫，測試才會符合預期，但是這樣寫太死了
            //_fileDownloader.Setup(fd => fd.DownloadFile("http://example.com/customer/installer", null)).Throws<WebException>();

            _fileDownloader.Setup(fd => 
                fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result=_installerHelper.DownloadInstaller("customer","installer");

            Assert.That(result, Is.False);
        }


        [Test]
        public void DownloaderInstaller_DownloadCompletes_ReturnTrue()
        {
           
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }
    }
}
