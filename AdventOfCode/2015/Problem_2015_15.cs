using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2015_15 : Problem_2015
    {
        public override int Number => 15;

        int[] quantities;

        public override void Run()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            foreach (string line in Lines)
            {
                string[] inputs = line.Split(' ');
                string name = inputs[0].Split(':')[0];
                int capacity = int.Parse(inputs[2].Split(',')[0]);
                int durability = int.Parse(inputs[4].Split(',')[0]);
                int flavor = int.Parse(inputs[6].Split(',')[0]);
                int texture = int.Parse(inputs[8].Split(',')[0]);
                int calories = int.Parse(inputs[10]);

                ingredients.Add(new Ingredient
                {
                    name = name,
                    capacity = capacity,
                    durability = durability,
                    flavor = flavor,
                    texture = texture,
                    calories = calories
                });
            }

            quantities = new int[ingredients.Count];

            for (int i = 0; i < ingredients.Count; i++)
            {
                quantities[i] = 1;
            }

            int maxScoreFirstStar = int.MinValue;
            int maxScoreSecondStar = int.MinValue;

            while (!NextQuantities())
            {
                int sumQuantities = quantities.Sum();

                if (sumQuantities == 100)
                {
                    int capacity = 0;
                    int durability = 0;
                    int flavor = 0;
                    int texture = 0;
                    int calories = 0;

                    for (int i = 0; i < quantities.Length; i++)
                    {
                        capacity += quantities[i] * ingredients[i].capacity;
                        durability += quantities[i] * ingredients[i].durability;
                        flavor += quantities[i] * ingredients[i].flavor;
                        texture += quantities[i] * ingredients[i].texture;
                        calories += quantities[i] * ingredients[i].calories;
                    }

                    if (capacity > 0 && durability > 0 && flavor > 0 && texture > 0)
                    {
                        int score = capacity * durability * flavor * texture;

                        if (score > maxScoreFirstStar)
                        {
                            maxScoreFirstStar = score;
                        }

                        if (calories == 500)
                        {
                            if (score > maxScoreSecondStar)
                            {
                                maxScoreSecondStar = score;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"First star: {maxScoreFirstStar}");
            Console.WriteLine($"Second star: {maxScoreSecondStar}");
        }

        // Returns whether to stop or not.
        private bool NextQuantities()
        {
            int idx = quantities.Length - 1;
            bool retenue = false;

            do
            {
                if (quantities[idx] == 99)
                {
                    quantities[idx] = 1;
                    retenue = true;
                    idx--;

                    if (idx == -1)
                    {
                        return true;
                    }
                }
                else
                {
                    quantities[idx]++;
                    retenue = false;
                }
            }
            while (retenue);

            return false;
        }

        class Ingredient
        {
            public string name;
            public int capacity;
            public int durability;
            public int flavor;
            public int texture;
            public int calories;
        }
    }
}
