using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    /*
        寫測試時，not to be too general or too specific
    
        
    */
    [TestFixture]
    public  class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElemnent()
        {
            var formatter = new HtmlFormatter();

            var result=formatter.FormatAsBold("abc");

            //Specific
            Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

            //more general

            //如果只判斷開頭，測試太空泛
            Assert.That(result, Does.StartWith("<strong>"));

            Assert.That(result, Does.EndWith("</strong>"));

            Assert.That(result, Does.Contain("abc"));
        }
    }
}
