using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TestNinja.Mocking
{
    public class HousekeeperHelper
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStatementGenerator _statementGenerator;
        private readonly IEmailSender _emailSender;
        private readonly IXtraMessageBox _xtraMessageBox;

        public HousekeeperHelper(IUnitOfWork unitOfWork, IStatementGenerator statementGenerator, IEmailSender emailSender, IXtraMessageBox xtraMessageBox)
        {
            _unitOfWork=unitOfWork;
            _statementGenerator = statementGenerator;
            _emailSender = emailSender;
            _xtraMessageBox = xtraMessageBox;
        }

        // 這邊是用建構函式的方式做DI，但需要把static 去掉，如果很多地方會用到這個函式的話，要小心
        public  bool SendStatementEmails(DateTime statementDate)
        {
            //外部資源、類似資料庫
            // 這個查詢比較單純，可以使用介面就好，就不要再拉一個repository 了
            //var housekeepers = UnitOfWork.Query<Housekeeper>();


            var housekeepers = _unitOfWork.Query<Housekeeper>();

            foreach (var housekeeper in housekeepers)
            {
                if (housekeeper.Email == null)
                    continue;

                var statementFilename = _statementGenerator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                var emailAddress = housekeeper.Email;
                var emailBody = housekeeper.StatementEmailBody;

                try
                {

                    _emailSender.EmailFile(emailAddress, emailBody, statementFilename,
                        string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
                }
                catch (Exception e)
                {
                    _xtraMessageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
                        MessageBoxButtons.OK);
                }
            }

            return true;
        }

        

        
    }

    public enum MessageBoxButtons
    {
        OK
    }

    public interface IXtraMessageBox
    {
        void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
    }

    public class XtraMessageBox : IXtraMessageBox
    {
        public void Show(string s, string housekeeperStatements, MessageBoxButtons ok)
        {
        }
    }

    public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }
    }

    public class DateForm
    {
        public DateForm(string statementDate, object endOfLastMonth)
        {
        }

        public DateTime Date { get; set; }

        public DialogResult ShowDialog()
        {
            return DialogResult.Abort;
        }
    }

    public enum DialogResult
    {
        Abort,
        OK
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string EmailFromName { get; set; }
    }

    public class Housekeeper
    {
        public string Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; }
        public string StatementEmailBody { get; set; }
    }

    public class HousekeeperStatementReport
    {
        public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
        {
        }

        public bool HasData { get; set; }

        public void CreateDocument()
        {
        }

        public void ExportToPdf(string filename)
        {
        }
    }
}