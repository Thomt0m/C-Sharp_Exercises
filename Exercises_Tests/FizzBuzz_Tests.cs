using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exercises;



namespace Exercises.Tests
{

    [TestClass()]
    public class FizzBuzz_Tests
    {
        FizzBuzz FizzBuzz = new FizzBuzz();

        [TestMethod()]
        public void GetFizzBuzz_Test()
        {
            Assert.AreEqual("0", FizzBuzz.GetFizzBuzz(0));
            Assert.AreEqual("2", FizzBuzz.GetFizzBuzz(2));
            Assert.AreEqual("Fizz", FizzBuzz.GetFizzBuzz(3));
            Assert.AreEqual("Buzz", FizzBuzz.GetFizzBuzz(5));
            Assert.AreEqual("FizzBuzz", FizzBuzz.GetFizzBuzz(15));
        }
    }
}
