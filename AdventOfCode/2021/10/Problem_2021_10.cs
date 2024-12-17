using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2021_10 : Problem
    {
        public override int Year => 2021;
        public override int Number => 10;

        Character[] characters;
        Stack<char> stack = new Stack<char>();

        protected override void InitInternal()
        {
            characters = new Character[]
            {
                new Character('(', ')', 3, 1),
                new Character('[', ']', 57, 2),
                new Character('{', '}', 1197, 3),
                new Character('<', '>', 25137, 4)
            };

            base.InitInternal();
        }

        public override void Run()
        {
            int totalScore = 0;

            var autocompleteScores = new List<long>();

            foreach (string line in Lines)
            {
                stack.Clear();
                bool lineIsCorrupted = false;

                foreach (char c in line)
                {
                    Character character = GetOpeningCharacter(c);

                    if (character != null) // it's an opening character
                    {
                        stack.Push(c);
                    }
                    else // it's a closing character
                    {
                        character = GetClosingCharacter(c);

                        if (stack.Peek() == character.Open)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            totalScore += character.IllegalScore;
                            lineIsCorrupted = true;
                            break;
                        }
                    }
                }

                if (!lineIsCorrupted)
                {
                    autocompleteScores.Add(GetAutoCompleteScore(GetAutoCompleteString(stack)));
                }
            }

            Console.WriteLine($"First star: {totalScore}");

            autocompleteScores.Sort();
            Console.WriteLine($"Second star: {autocompleteScores[autocompleteScores.Count / 2]}");
        }

        #region Methods

        Character GetOpeningCharacter(char c)
        {
            return characters.FirstOrDefault(ch => ch.Open == c);
        }

        Character GetClosingCharacter(char c)
        {
            return characters.FirstOrDefault(ch => ch.Close == c);
        }

        string GetAutoCompleteString(Stack<char> stack)
        {
            string result = "";

            while (stack.Count > 0)
            {
                Character character = GetOpeningCharacter(stack.Pop());
                result += character.Close;
            }

            return result;
        }

        long GetAutoCompleteScore(string str)
        {
            long score = 0;

            foreach (char c in str)
            {
                Character character = GetClosingCharacter(c);
                score *= 5;
                score += character.AutocompleteScore;
            }

            return score;
        }

        #endregion

        class Character
        {
            public char Open { get; }
            public char Close { get; }
            public int IllegalScore { get; }
            public int AutocompleteScore { get; }

            public Character(char open, char close, int illegalScore, int autocompleteScore)
            {
                Open = open;
                Close = close;
                IllegalScore = illegalScore;
                AutocompleteScore = autocompleteScore;
            }
        }
    }
}
