using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2020_6 : Problem_2020
    {
        public override int Number => 6;

        public override void Run()
        {
            var combinedAnswers = "";
            var nbPeople = 0;
            var groups = new List<Group>();

            foreach (var line in Lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    groups.Add(new Group(combinedAnswers, nbPeople));
                    combinedAnswers = "";
                    nbPeople = 0;
                }
                else
                {
                    combinedAnswers += line;
                    nbPeople++;
                }
            }

            groups.Add(new Group(combinedAnswers, nbPeople));

            Console.WriteLine($"First star: {groups.Sum(g => g.GetNbAnswersToWhichAnyoneAnsweredYes())}");
            Console.WriteLine($"Second star: {groups.Sum(g => g.GetNbAnswersToWhichEveryoneAnsweredYes())}");
        }

        class Group
        {
            public string combinedAnswers;
            public int nbPeople;

            public Group(string combinedAnswers, int nbPeople)
            {
                this.combinedAnswers = combinedAnswers;
                this.nbPeople = nbPeople;
            }

            public int GetNbAnswersToWhichEveryoneAnsweredYes()
            {
                var couc = combinedAnswers.Distinct();
                var count = 0;
                foreach (var character in couc)
                {
                    if (CountChar(character) == nbPeople)
                        count++;
                }

                return count;
            }

            public int GetNbAnswersToWhichAnyoneAnsweredYes()
            {
                return combinedAnswers.Distinct().Count();
            }

            private int CountChar(char character)
            {
                return combinedAnswers.Where(c => c == character).Count();
            }
        }
    }
}
