using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        [Test]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            var math=new Fundamentals.Math();

            var result = math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }
    }
}
