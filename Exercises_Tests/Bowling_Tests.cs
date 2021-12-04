using Microsoft.VisualStudio.TestTools.UnitTesting;
using exercises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace exercises.Tests
{

    [TestClass()]
    public class Bowling_Tests
    {
        Bowling Bowling = new Bowling();

        [TestMethod()]
        public void CalculateScore_Test()
        {
            string input;

            // 12 rolls: 12 strikes
            input = "X X X X X X X X X X X X";
            Assert.AreEqual(300, Bowling.CalculateScore(input), "12 rolls, 12 strikes = 10 frames * 30 points = 300");

            // 20 rolls: 10 pairs of varying values, but no strikes or spares
            input = "43 12 81 45 11 34 51 62 31 32";
            Assert.AreEqual(60, Bowling.CalculateScore(input), "20 rolls: 10 pairs of varying values, points = 60");

            // 20 rolls: 10 pairs of 9 and miss
            input = "9- 9- 9- 9- 9- 9- 9- 9- 9- 9-";
            Assert.AreEqual(90, Bowling.CalculateScore(input), "20 rolls, 10 pairs of 9 and miss = 10 frames * 9 points = 90");

            // 21 rolls: 10 pairs of 5 and spare, with a final 5
            input = "5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5";
            Assert.AreEqual(150, Bowling.CalculateScore(input), "21 rolls, 10 pairs of 5 and spare, with a final 5 = 10 frames * 15 points = 150");
        }
    }
}