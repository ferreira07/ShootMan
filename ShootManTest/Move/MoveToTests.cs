using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine.Move;

namespace ShootManTest.Move

{
    [TestClass]
    public class MoveToTests
    {
        [TestMethod]
        public void MoveToOnRepositionAxis_NormalPositive_Equal()
        {
            //Arrange
            float v = 0.5f;
            int p1 = 0;
            int s1 = 1;
            int p2 = 2;
            int s2 = 1;
            float expect = v;

            //Act
            float result = MoveTo.RepositionAxis(v, p1, s1, p2, s2);

            //Assert
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void MoveToOnRepositionAxis_NormalNegative_Equal()
        {
            //Arrange
            float v = -0.5f;
            int p1 = 2;
            int s1 = 1;
            int p2 = 0;
            int s2 = 1;
            float expect = v;

            //Act
            float result = MoveTo.RepositionAxis(v, p1, s1, p2, s2);

            //Assert
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void MoveToOnRepositionAxis_BlockedNegative_ReduceVelocity()
        {
            //Arrange
            float v = -2;
            int p1 = 2;
            int s1 = 1;
            int p2 = 0;
            int s2 = 1;
            float expect = -1;

            //Act
            float result = MoveTo.RepositionAxis(v, p1, s1, p2, s2);

            //Assert
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void MoveToOnRepositionAxis_BlockedPositive_ReduceVelocity()
        {
            //Arrange
            float v = 1.5f;
            int p1 = 0;
            int s1 = 1;
            int p2 = 2;
            int s2 = 1;
            float expect = 1;

            //Act
            float result = MoveTo.RepositionAxis(v, p1, s1, p2, s2);

            //Assert
            Assert.AreEqual(expect, result);
        }
    }
}
