using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2015_21 : Problem
    {
        public override int Year => 2015;
        public override int Number => 21;

        Item[] weapons;
        Item[] armors;
        Item[] rings;

        public override void Run()
        {
            BuildShop();

            Entity boss = new Entity
            (
                hitPoints: int.Parse(Lines[0].Split(": ")[1]),
                damage: int.Parse(Lines[1].Split(": ")[1]),
                armor: int.Parse(Lines[2].Split(": ")[1])
            );

            Entity player = new Entity(100);

            int minCost = int.MaxValue;
            int maxCost = 0;

            foreach (var weapon in weapons)
            {
                foreach (var armor in armors)
                {
                    foreach (var ring in rings)
                    {
                        foreach (var ring2 in rings)
                        {
                            if (ring.cost != ring2.cost) // We can't buy the same ring
                            {
                                Stuff stuff = new Stuff(new Item[] { weapon, armor, ring, ring2 });

                                if (stuff.cost < minCost)
                                {
                                    player.SetStuff(stuff);

                                    if (player.CanWinAgainst(boss))
                                    {
                                        minCost = stuff.cost;
                                    }
                                }

                                if (stuff.cost > maxCost)
                                {
                                    player.SetStuff(stuff);

                                    if (!player.CanWinAgainst(boss))
                                    {
                                        maxCost = stuff.cost;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"First star: {minCost}");
            Console.WriteLine($"Second star: {maxCost}");
        }

        private void BuildShop()
        {
            weapons = new Item[]
            {
            new Item("Dagger", 8, 4, 0),
            new Item("Shortsword", 10, 5, 0),
            new Item("Warhammer", 25, 6, 0),
            new Item("Longsword", 40, 7, 0),
            new Item("Greataxe", 74, 8, 0)
            };

            armors = new Item[]
            {
            new Item("Nothing", 0, 0, 0),
            new Item("Leather", 13, 0, 1),
            new Item("Chainmail", 31, 0, 2),
            new Item("Splintmail", 53, 0, 3),
            new Item("Bandedmail", 75, 0, 4),
            new Item("Platemail", 102, 0, 5)
            };

            rings = new Item[]
            {
            new Item("Nothing", 0, 0, 0),
            new Item("Damage + 1", 25, 1, 0),
            new Item("Damage + 2", 50, 2, 0),
            new Item("Damage + 3", 100, 3, 0),
            new Item("Defense + 1", 20, 0, 1),
            new Item("Defense + 2", 40, 0, 2),
            new Item("Defense + 3", 80, 0, 3)
            };
        }

        class Entity
        {
            public int hitPoints { get; private set; }
            public int damage { get; private set; }
            public int armor { get; private set; }

            public Entity(int hitPoints)
                : this(hitPoints, 0, 0) { }

            public Entity(int hitPoints, int damage, int armor)
            {
                this.hitPoints = hitPoints;
                this.damage = damage;
                this.armor = armor;
            }

            public void SetStuff(Stuff stuff)
            {
                this.damage = stuff.damage;
                this.armor = stuff.armor;
            }

            public bool CanWinAgainst(Entity other)
            {
                return GetTurnsToDieAgainst(other) >= other.GetTurnsToDieAgainst(this);
            }

            public int GetTurnsToDieAgainst(Entity other)
            {
                float damageTaken = (float)other.damage - armor;
                if (damageTaken < 1) damageTaken = 1;

                return (int)Math.Ceiling(hitPoints / damageTaken);
            }
        }

        class Stuff
        {
            public int cost { get; }
            public int damage { get; }
            public int armor { get; }

            public Stuff(Item[] items)
            {
                cost = items.Sum(i => i.cost);
                damage = items.Sum(i => i.damage);
                armor = items.Sum(i => i.armor);
            }
        }

        class Item
        {
            public string name { get; }
            public int cost { get; }
            public int damage { get; }
            public int armor { get; }

            public Item(string name, int cost, int damage, int armor)
            {
                this.name = name;
                this.cost = cost;
                this.damage = damage;
                this.armor = armor;
            }
        }
    }
}
