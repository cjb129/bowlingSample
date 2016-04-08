using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bowlingScore
{
    class Game
    {

        int score = 0;
        public int rolls = 0;

        int current = 0;
        int prev1 = 0;
        int prev2 = 0;


        public int[] frames = new int[26];

        public void Roll(int pins)
        {

            //bool isSpare = false;
            //bool isStrike = false;

            //if (rolls >= 2)
            //{
            //    prev1 = frames[rolls - 2];
            //    prev2 = frames[rolls - 1];
            //}

            //if (prev1 == 10) isStrike = true;

            //if (prev1 + prev2 == 10)
            //{
            //    isSpare = true;
            //    Console.WriteLine("ahh");
            //}

            //if (isSpare)
            //{
            //    score += pins;
            //}

            //else if (isStrike)
            //{
            //    Console.WriteLine("foo");
            //    score += 1 + prev2 + pins;
            //}

            
            //Console.WriteLine(score);


            if (pins == -1) return;
            score += pins;

            

        }

        public int Score()
        {
            for (int i = 0; i < 20; i += 2)
            {
                int first = frames[i];
                int second = frames[i + 1];

                //spare
                if (first + second == 10 && i < 18)
                {
                    if (frames[i + 2] != -1) score += frames[i + 2];
                    //next frame is strike
                    else if (frames[i + 2] == -1) score += frames[i + 3];
                    else Console.WriteLine("notstrike?");
                }

                //strike
                if (first == -1)
                {
                    //next frame not a strike
                    if (frames[i + 2] != -1)
                        score += frames[i + 2] + frames[i + 3];
                    else
                    {
                        if (frames[i + 4] != -1)
                            score += frames[i + 3] + frames[i + 4];
                        //next 2 frames are strikes
                        else
                            score += frames[i + 3] + frames[i + 5];
                    }
                }

                ////strikes on last frame
                //if (first == 10)
                //{
                //    Console.WriteLine("first==10");
                //    score += frames[i + 1] + frames[i + 2];
                //}
                //if (second == 10 && first==10)
                //{
                //    Console.WriteLine("second==10");
                //    score += frames[i + 2];
                //}
                Console.WriteLine(score);
            }

            return score;
        }

    }

    class Play
    {
        static void Main(string[] args)
        {
            Game play = new Game();

            String input = Console.ReadLine();
            String[] inputSplit = input.Split();

            int f = 0;

            for (int i = 0; i < inputSplit.Length; i++)
            {
                int r = Convert.ToInt32(inputSplit[i]);
                //Console.WriteLine(r);
                if (r == 10)
                {
                    if (f < 18)
                    {
                        //placeholder for strikes
                        play.frames[f] = -1;
                        f++;
                    }
                }
                //Console.WriteLine(r);
                play.frames[f] = r;
                f++;
            }

            while (play.rolls < 21)
            {
                Console.WriteLine(play.frames[play.rolls]);
                play.Roll(play.frames[play.rolls]);
                play.rolls++;
            }

            Console.Write(play.Score());
        }
    }
}
