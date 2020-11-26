using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.UnitTests
{
    [TestFixture]
    public class AIPlayerTest
    {
        [Test, TestCaseSource("LastHitCases")]
        public void AIShootsFromLastHitTestTest(ref List<int[]> lastHit, ref List<int[]> prevShots, int[,] expectedShoot)
        {
            AIPlayer test = new AIPlayer();
            Assert.That(test.AIShoots(ref lastHit, ref prevShots), Is.SubsetOf(expectedShoot));
            
        }

        static object[] LastHitCases =
        {
            new object[] { new List<int[]>() { new int[3] { 6, 4, 4 } }, 
                           new List<int[]>() { new int[2] { 6, 3 }, new int[2] { 5, 4 }, new int[2] { 7, 4 } }, 
                           new int[,] { { 6, 5 } } },

            new object[] { new List<int[]>() { new int[3] { 6, 4, 4 } }, 
                           new List<int[]>() {}, 
                           new int[,] { { 6, 5 }, { 5, 4 }, { 6, 3 }, { 7, 4 } } },

            new object[] { new List<int[]>() { new int[3] { 6, 4, 4 }, new int[3] { 6, 5, 3 }  },
                           new List<int[]>() { new int[2] { 6, 3 }, new int[2] { 5, 4 }, new int[2] { 7, 4 }, new int[2] { 6, 4 }, new int[2] { 6, 5 } },
                           new int[,] { { 5, 5 }, { 6, 6 }, { 7, 5 } } },

            new object[] { new List<int[]>() { new int[3] { 6, 5, 2 }, new int[3] { 6, 6, 3 } },
                           new List<int[]>() { new int[2] { 6, 3 }, new int[2] { 5, 4 }, new int[2] { 7, 4 }, new int[2] { 5, 6 }, new int[2] { 6, 4 }, new int[2] { 6, 7 }, new int[2] { 7, 6 }, new int[2] { 6, 5 }, new int[2] { 6, 6 } }, 
                           new int[,] { { 7, 5 }, { 5, 5 } } }
        };
    }
}
