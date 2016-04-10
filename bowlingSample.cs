using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bowlingScore
{

    public class Roll
    {
        public bool strike = false;
        public bool spare = false;
        public int score2 = 0;
        public int score1 = 0;
        public int total = 0;
    }

    class Game
    {

        int score = 0;
        public int rolls = 0;
        bool firstRoll = true;

        List<Roll> bowls = new List<Roll>();

        public int Score()
        {
            //add two emptys on the end so to avoid going out of range
            bowls.Add(new bowlingScore.Roll());
            bowls.Add(new bowlingScore.Roll());

            for (int i = 0; i < bowls.Count; i++)
            {
                if (bowls[i].strike)
                {
                    if(bowls[i+1].strike)
                    {
                        score += bowls[i + 1].score1 + bowls[i + 2].score1;
                    }
                    else
                    {
                        score += bowls[i + 1].score1 + bowls[i + 1].score2;
                    }
                }
                else if(bowls[i].spare)
                {
                    score += bowls[i + 1].score1;
                }

                //Console.Write("{0},{1} ", bowls[i].score1, bowls[i].score2);
                score += bowls[i].score1 + bowls[i].score2;
                bowls[i].total += score;
            }

            for (int i=0; i < 10;i++) Console.Write("{0} ", bowls[i].total);
            return score;
        }

        public void Roll(int pins)
        {
            if (firstRoll)
            {
                Roll current = new bowlingScore.Roll();
                firstRoll = false;
                current.score1 = pins;
                if (pins == 10)
                {
                    current.strike = true;
                    firstRoll = true;
                }
                bowls.Add(current);
            }
            else
            {
                firstRoll = true;
                Roll current = bowls[bowls.Count-1];
                current.score2 = pins;
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
            String input = Console.ReadLine();
            String[] inputSplit = input.Split();

            for (int i = 0; i < inputSplit.Length; i++)
            {
                int r = Convert.ToInt32(inputSplit[i]);
                play.Roll(r);
            }

            play.Score();
        }
    }
}
