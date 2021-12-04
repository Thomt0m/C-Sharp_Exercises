using System;
using System.IO;


namespace exercises
{
    /// <summary>
    /// https://codingdojo.org/kata/Bowling/
    /// </summary>
    public class Bowling
    {


        enum Frame1 { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Strike, Spare, SparePlus }
        enum Frame { None, Normal, Spare, Strike, SparePlus }

        public void Start()
        {

            Console.WriteLine("Hello World");

            TestCalculateScore("X X X X X X X X X X X X");
            TestCalculateScore("43 12 81 45 11 34 51 62 31 32");
            TestCalculateScore("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-");
            TestCalculateScore("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5");
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


            // TODO remove, Debug
            Console.Write("Frames: ");
            foreach (Frame frame in frames)
            {
                Console.Write(frame.ToString() + " ");
            }
            Console.WriteLine();
            int pins_this_frame = 0;
            foreach (int p in scores)
            {
                pins_this_frame += p;
            }
            Console.Write("Total pins = " + pins_this_frame);


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








        public int CalculateScoreOLD(string s)
        {
            int score = 0;

            List<Frame1> frames = new List<Frame1>();
            List<int> scores = new List<int>();

            Frame1 currentFrame;
            int start = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsWhiteSpace(s[i]))
                {
                    currentFrame = Frame1.Zero;
                    // Act based on length of frame string
                    switch(i - start)
                    {
                        case 1: { currentFrame = Frame1.Strike; scores.Add(10); break; }
                        case 2:
                            {
                                if (s[i-1] == '/') { currentFrame = Frame1.Spare; }
                                else
                                {
                                    int x = 0;
                                    foreach (char c in s.Substring(start, 2))
                                    {
                                        if (int.TryParse(c.ToString(), out x)) { currentFrame += x; }
                                    }
                                }
                                break;
                            }
                        default: { currentFrame = Frame1.Zero; break; }
                    }
                    
                    frames.Add(currentFrame);
                    i++;
                    start = i;
                }
            }


            // Add the last frame
            currentFrame = Frame1.Zero;
            int lastFrame = frames.Count - 1;
            switch (s.Length - start)
            {
                // Single throw, must be a strike
                case 1:
                    {
                        currentFrame = Frame1.Strike;

                        // If the previous frame was also a strike, means this frame is part of the 'tenth' frame, adjust 'lastFrame' accordingly
                        if (frames.Last() == Frame1.Strike)
                        {
                            lastFrame--;
                        }

                        break;
                    }

                // Two throws, must be a 'regular' throw, no spare or strike
                case 2:
                    {
                        int x = 0;
                        foreach (char c in s.Substring(start))
                        {
                            if (int.TryParse(c.ToString(), out x)) { currentFrame += x; }
                        }

                        // Frame contains two throws, and no strikes or spares, so this is the 'tenth' frame, adjust 'lastFrame' accordingly
                        lastFrame++;

                        break;
                    }

                // Three throws, player must have thrown a spare on the second throw
                case 3:
                    {
                        frames.Add(Frame1.Spare);
                        // The 'tenth' frame was this spare, adjust 'lastFrame' accordingly
                        lastFrame++;

                        int x = 0;
                        int.TryParse(s.Last().ToString(), out x);
                        currentFrame = (Frame1)x;

                        break;
                    }
            }
            frames.Add(currentFrame);


            // TODO remove, Debug
            Console.Write("Frames: ");
            foreach (Frame1 frame in frames)
            {
                Console.Write(frame.ToString() + " ");
            }


            // Calculate the score            
            for (int j = 0; j <= lastFrame; j++)
            {
                switch (frames[j])
                {
                    case Frame1.Strike:
                        {
                            if (frames.Count - j >= 3) { score += (int)frames[j + 2]; }
                            // Add one, countering the minus one that will be incurred at 'case Frame.Space'
                            score++;
                            goto case Frame1.Spare;
                        }
                    case Frame1.Spare:
                        {
                            if (frames.Count - j >= 2) { score += (int)frames[j + 1]; }
                            // Minus one, since spare value in enum is 11 but should be 10 (would conflict with strike)
                            score--;
                            goto default;
                        }
                    default:
                        {
                            score += (int)frames[j];
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
            Console.WriteLine();
            string score = CalculateScore(s).ToString();
            Console.WriteLine();
            Console.WriteLine("Score = " + score);
        }
    }

}