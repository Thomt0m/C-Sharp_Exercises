namespace Exercises
{
    class AAA_EntryPoint
    {
        public static void Main()
        {
            // Select which classes to run (true for run, false for not)
            bool doBowling = true;
            bool doFizzBuzz = true;




            if (doBowling)
            {
                Bowling bowling = new Bowling();
                bowling.Start();
                Console.WriteLine();
            }
            if (doFizzBuzz)
            {
                FizzBuzz fizzbuzz = new FizzBuzz();
                fizzbuzz.Start();
            }
            
        }
    }
}
