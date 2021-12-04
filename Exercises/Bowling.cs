using System;
using System.IO;


namespace Exercises
{
    /// <summary>
    /// https://codingdojo.org/kata/Bowling/
    /// </summary>
    public class Bowling
    {


        enum Frame { None, Normal, Spare, Strike, SparePlus }

        public void Start()
        {
            TestCalculateScore("X X X X X X X X X X X X");
            TestCalculateScore("43 12 81 45 11 34 51 62 31 32");
            TestCalculateScore("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-");
            TestCalculateScore("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5");
            Console.WriteLine();
        }


        public int CalculateScore(string s)
        {
            int score = 0;
            List<Frame> frames = new List<Frame>();
            List<int> scores = new List<int>();

            // Start by looking at each frame, and log what kind of throw it was and how many pins were hit (excluding a spare on the second throw)
            Frame currentFrame;
            int start = 0;
            for (int i = 0; i <= s.Length; i++)
            {
                if (i == s.Length || char.IsWhiteSpace(s[i]))
                {
                    switch (i - start)
                    {
                        case 1:
                            {
                                currentFrame = Frame.Strike; scores.Add(10);
                                break;
                            }
                        case 2:
                            {                                
                                int x = 0;
                                int pins = 0;
                                foreach (char c in s.Substring(start, 2))
                                {
                                    if (int.TryParse(c.ToString(), out x)) { pins += x; }
                                }

                                if (s[i - 1] == '/') { currentFrame = Frame.Spare; }
                                else if (pins > 0) { currentFrame = Frame.Normal; }
                                else { currentFrame = Frame.None; }

                                scores.Add(pins);
                                break;
                            }
                        case 3:
                            {
                                currentFrame = Frame.SparePlus;
                                int x = 0;
                                int pins = 0;
                                if (int.TryParse(s.Last().ToString(), out x)) { pins += x; }
                                scores.Add(pins);
                                break;
                            }
                        default: { currentFrame = Frame.None; break; }
                    }

                    frames.Add(currentFrame);

                    if (i != s.Length)
                    {
                        i++;
                        start = i;
                    }
                }
            }


            // Analyse the last throw(s), and find the correct position of the 'tenth' frame
            int lastFrame = frames.Count - 1;
            switch (s.Length - start)
            {
                // Single throw, must be a strike
                case 1:
                    {
                        // If the previous frame was also a strike, this frame is part of a three-strike 'tenth' frame, adjust 'lastFrame' accordingly
                        if (frames[frames.Count - 2] == Frame.Strike) { lastFrame = frames.Count - 3; }
                        break;
                    }

                // Two throws, must be a 'regular' throw, no spare or strike
                case 2:
                    {
                        // Frame contains two throws, and no strikes or spares, so this is the 'tenth' frame, adjust 'lastFrame' accordingly
                        break;
                    }

                // Three throws, player must have thrown a spare on the second throw
                case 3:
                    {
                        // The 'tenth' frame was a spare, adjust 'lastFrame' accordingly
                        break;
                    }
            }
            // The above is the same as, can be replaced by, the below. (the method above shows more of the thought process)
            /*
            if (s.Length - start == 1 && frames[frames.Count - 2] == Frame.Strike) { lastFrame = frames.Count - 3; }
            */


            // Calculate the score
            for (int j = 0; j <= lastFrame; j++)
            {
                switch (frames[j])
                {
                    case Frame.Strike:
                        {
                            if (frames.Count - j >= 3) { score += scores[j + 2]; }
                            goto case Frame.Spare;
                        }
                    case Frame.SparePlus:
                    case Frame.Spare:
                        {
                            if (frames.Count - j >= 2) { score += scores[j + 1]; }
                            goto default;
                        }
                    default:
                        {
                            if (frames[j] == Frame.Spare) { score += 10; }
                            else
                            {
                                if (frames[j] == Frame.SparePlus) { score += 10; }
                                score += scores[j];                                
                            }
                            break;
                        }
                }
            }

            return score;
        }




        private void TestCalculateScore(string s)
        {
            Console.WriteLine("\n");
            Console.WriteLine("---- Testing with \'" + s + "\'");
            string score = CalculateScore(s).ToString();
            Console.WriteLine("Score = " + score);
        }
    }

}