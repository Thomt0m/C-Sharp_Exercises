using System;



namespace Exercises
{
    public class FizzBuzz
    {

        public void Start()
        {
            for (int i = 0; i < 20 + 1; i++)
            {
                Console.WriteLine(GetFizzBuzz(i));
            }
            Console.WriteLine();
        }


        public string GetFizzBuzz(int x)
        {
            string result = "";
            x = Math.Abs(x);

            if (x % 3 == 0) { result += "Fizz"; }
            if (x % 5 == 0) { result += "Buzz"; }
            if (result == "" || x == 0) { result = x.ToString(); }

            return result;
        }

    }
}
