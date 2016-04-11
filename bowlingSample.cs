//Cory Boveington
//Bowling score calculator

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bowlingScore
{

    public class Frame
    {
        public bool strike = false;
        public bool spare = false;
        public int score2 = 0;
        public int score1 = 0;
        //score for current frame added up from previous frames
        public int total = 0;
    }

    class Game
    {

        int score = 0;
        bool firstRoll = true;

        List<Frame> bowls = new List<Frame>();

        //count up the final score
        //uses strike/spare flags for score modifications
        public int Score()
        {
            //add two emptys on the end so to avoid going out of range in main loop
            bowls.Add(new bowlingScore.Frame());
            bowls.Add(new bowlingScore.Frame());

            for (int i = 0; i < bowls.Count; i++)
            {
                //strikes
                if (bowls[i].strike)
                {
                    //if the next bowl is also a strike
                    if(bowls[i+1].strike)
                    {
                        score += bowls[i + 1].score1 + bowls[i + 2].score1;
                    }
                    //if the next bowl is not a strike
                    else
                    {
                        score += bowls[i + 1].score1 + bowls[i + 1].score2;
                    }
                }
                //spare
                else if(bowls[i].spare)
                {
                    score += bowls[i + 1].score1;
                }

                //score calculated first...may not always have a score2 (in the case of strikes)
                score += bowls[i].score1 + bowls[i].score2;
                bowls[i].total += score;
            }

            //output score for 10 frames. If less than 10 frames are input, an exception will be thrown.
            for (int i=0; i < 10;i++) Console.Write("{0} ", bowls[i].total);

            return score;
        }

        //pins: number of pins knocked down
        //Swaps between first and second bowl, sets strike/spare flags for later score modification.
        public void Roll(int pins)
        {
            //swap between first/second roll
            if (firstRoll)
            {
                //always add a new frame on first bowl
                Frame current = new bowlingScore.Frame();
                firstRoll = false;
                current.score1 = pins;
                //strike
                if (pins == 10)
                {
                    current.strike = true;
                    firstRoll = true;
                }
                //add frame to list
                bowls.Add(current);
            }
            else
            {
                firstRoll = true;
                //add score to frame from first bowl
                Frame current = bowls[bowls.Count-1];
                current.score2 = pins;
                //spare
                if(current.score1+current.score2==10 && !current.strike)
                {
                    current.spare = true;
                }
            }
        }


    }

    class Play
    {
        static void Main(string[] args)
        {
            Game play = new Game();

            //take in input (all on one line, space separated)
            String input = Console.ReadLine();
            String[] inputSplit = input.Split();

            //call Roll on input
            for (int i = 0; i < inputSplit.Length; i++)
            {
                int r = Convert.ToInt32(inputSplit[i]);
                play.Roll(r);
            }

            play.Score();
        }
    }
}
