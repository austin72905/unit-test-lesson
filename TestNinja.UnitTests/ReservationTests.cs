using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        /*
            TDD �}�o�Ҧ�
            
            ���g���աA�A�}�l�gcode

            �B�J
            1. ���g�@�ӵ��G���Ѫ�����
            2. �g�@��²�檺�{���X�A�����G�q�L
            3. �v�B��g���A�������Ҫ��{���X
         
        */
        [SetUp]
        public void Setup()
        {
        }

        /*
            �R�W�W�h
            �n���ժ���ƦW_�����ܦ]_�w�����G

            �ϥ�3A ���g �椸���դ��e
    
            1. Arrange  :  ���ժ��󪺪�l�ơB�w�q�ݭn�ϥΪ��Ѽ�
            2. Act  :  �I�s�Q���ժ���k
            3. Assert  :  ���ҵ��G


            �M��
            �p�G���@�ӱM�ץsTestNinja  ���n���Ӵ��ձM�ץs TestNinja.UnitTests

            ���W          Reservation          ReservationTests

            �p�G�@�Ӥ�k�ܦ]�ܦh�]�i�H��Ӥ�k�W�ߤ@���� (���W_��k�W)
        */
        [Test]
        public void CanBeCancelledBy_UserAdmin_ReturnsTrue()
        {
            //Arrange
            var reservation = new Reservation();
            //Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            //Assert
            /*
                ���h�ؼg�k�A�D�@�ӳ��w���g�k�N�n
            */
            Assert.That(result, Is.True);
            //Assert.IsTrue(result);
            //Assert.That(result == true);
        }
        [Test]
        public void CanBeCancelledBy_SameUserIsCancellingTheReservation_ReturnsTrue()
        {
            var user=new User();
            var reservation = new Reservation() {MadeBy=user };

            var result = reservation.CanBeCancelledBy(user);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserIsCancellingTheReservation_ReturnsFalse()
        {
            var user = new User();
            var reservation = new Reservation() { MadeBy = user };

            var result = reservation.CanBeCancelledBy(new User());

            Assert.IsFalse(result);
        }

        /*
            �}�n���椸����
            1. ���n�A���ո̭��g�޿�P�_(if else)

            ���Ǥ��n����
            1. �T��w���N�X
            2. c#�y���������\��
        */
    }
}