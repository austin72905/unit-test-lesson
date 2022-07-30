using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void Push_ArgIsNull_ThrowArgumentNullException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArg_AddTheObjectToTheStack()
        {
            var stack = new Fundamentals.Stack<string>();

            stack.Push("a");

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_StackIsEmpty_ReturnZero()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_StackIsEmpty_ThrowInvalidOperationException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithObject_ReturnObjectOnTheTop()
        {
            var stack = new Fundamentals.Stack<string>();

            stack.Push("a");
            stack.Push("B");
            Assert.That(stack.Pop(),Is.EqualTo("B"));

        }

        [Test]
        public void Pop_StackWithObject_RemoveObjectOnTheTop()
        {
            var stack = new Fundamentals.Stack<string>();

            stack.Push("a");
            stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Peek_StackIsEmpty_ThrowInvalidOperationException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithObject_ReturnObjectOnTheTop()
        {
            var stack = new Fundamentals.Stack<string>();

            stack.Push("a");
            stack.Push("B");
            Assert.That(stack.Peek(), Is.EqualTo("B"));

        }

        [Test]
        public void Peek_StackWithObject_DoesNotRemoveObjectOnTheTop()
        {
            var stack = new Fundamentals.Stack<string>();

            stack.Push("a");
            stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(1));
        }
    }
}
