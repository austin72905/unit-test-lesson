using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private HousekeeperService _service;

        private Mock<IStatementGenerator> _statementGenerator;

        private Mock<IEmailSender> _emailSender;

        private Mock<IXtraMessageBox> _xtraMessageBox;

        private DateTime _statementDate = new DateTime(2022, 12, 17);

        private Housekeeper _housekeeper;

        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            // 這些希望每次測試時，都能有一個新的_housekeeper，避免有些情況修改了_housekeeper內容而忽略，所以不像_statementDate直接寫在上面
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper

            }.AsQueryable());

            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);
            //_statementFileName 這邊需要寫成 lamda ，他才會有lazy evaluatin 的功能，否則在各函式覆寫的 _statementFileName不會發生作用

            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();


            _service = new HousekeeperService(
                unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _xtraMessageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);
            //測試SaveStatement這個函式是否有被呼叫
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = null;

            _service.SendStatementEmails(_statementDate);
            //他第二個參數可以測試，裡面的某個函數被呼叫了幾次
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsWhitespace_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = " ";

            _service.SendStatementEmails(_statementDate);
            //他第二個參數可以測試，裡面的某個函數被呼叫了幾次
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsEmpty_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = "";

            _service.SendStatementEmails(_statementDate);
            //他第二個參數可以測試，裡面的某個函數被呼叫了幾次
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                Times.Never);
        }


        [Test]
        public void SendStatementEmails_WhenCalled_EmailStatement()
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(_statementFileName);

            _service.SendStatementEmails(_statementDate);

            VerfityEmailSend();
        }


        [Test]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailStatement()
        {
            _statementFileName = null;

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSend();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsEmptyString_ShouldNotEmailStatement()
        {
            _statementFileName = "";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSend();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsWhitespace_ShouldNotEmailStatement()
        {
            _statementFileName = " ";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSend();
        }

        private void VerifyEmailNotSend()
        {
            //這邊只關注 EmailFile 不該被呼叫，所以傳進去的值就不重要了，故改成It.IsAny<string>()
            _emailSender.Verify(es => es.EmailFile(
                 It.IsAny<string>(),
                 It.IsAny<string>(),
                 It.IsAny<string>(),
                 It.IsAny<string>()), Times.Never);
        }

        private void VerfityEmailSend()
        {
            //這邊使用It.IsAny<string>()，如果你把特定的字串傳進去，因為這是實現細節，之後很可能修改，這樣會導致寫的測試很脆弱
            _emailSender.Verify(es => es.EmailFile(
                _housekeeper.Email,
                _housekeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }
    }
}
