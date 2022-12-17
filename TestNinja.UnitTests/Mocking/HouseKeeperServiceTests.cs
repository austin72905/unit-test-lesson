using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private DateTime _statementDate= new DateTime(2022, 12, 17);

        private Housekeeper _housekeeper;

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

            _statementGenerator = new Mock<IStatementGenerator>();
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

            _statementGenerator.Verify(sg=>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }
    }
}
