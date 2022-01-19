using System;
using System.Collections.Generic;
using AdventOfCode.Core;

namespace AdventOfCode
{
    class Problem_2021_11 : Problem_2021
    {
        public override int Number => 11;

        const int Size = 10;
        Octopus[,] grid;

        protected override void InitInternal()
        {
            grid = new Octopus[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    grid[i, j] = new Octopus(new Vector2Int(i, j), Lines[i][j] - '0');
                }
            }

            ApplyOnOctopuses((octopus) => octopus.SetNeighbours(grid, Size, Size));

            base.InitInternal();
        }

        public override void Run()
        {
            int totalFlashes = 0;
            int step = 0;
            bool allOctopusesFlashed = false;

            while (!allOctopusesFlashed)
            {
                int nbFlashesThisStep = DoOneStep();

                if (step < 100)
                {
                    totalFlashes += nbFlashesThisStep;
                }

                allOctopusesFlashed = DidAllOctopusesFlash();

                step++;
            }

            Console.WriteLine($"First star: {totalFlashes}");
            Console.WriteLine($"Second star: {step}");
        }

        #region Methods

        /// <returns>
        /// The number of flashes this step.
        /// </returns>
        private int DoOneStep()
        {
            ApplyOnOctopuses((octopus) => octopus.IncreaseEnergy());

            int nbFlashes = 0;

            while (true)
            {
                int nbFlashesThisTime = 0;

                ApplyOnOctopuses((octopus) =>
                {
                    if (octopus.CanFlash() && octopus.Flash())
                    {
                        nbFlashesThisTime++;
                    }
                });

                if (nbFlashesThisTime == 0)
                    break;

                nbFlashes += nbFlashesThisTime;
            }

            ApplyOnOctopuses((octopus) =>
            {
                if (octopus.HasFlashedThisStep)
                {
                    octopus.Reset();
                }
            });

            return nbFlashes;
        }

        private void ApplyOnOctopuses(Action<Octopus> actionOnOctopus)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    actionOnOctopus(grid[i, j]);
                }
            }
        }

        private bool DidAllOctopusesFlash()
        {
            int nbOctopusesAtZeroEnergy = 0;

            ApplyOnOctopuses((octopus) =>
            {
                if (octopus.Energy == 0)
                {
                    nbOctopusesAtZeroEnergy++;
                }
            });

            return nbOctopusesAtZeroEnergy == grid.Length;
        }

        private void DisplayOctopuses()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(grid[i, j].Energy);
                }

                Console.WriteLine();
            }
        }

        #endregion

        class Octopus
        {
            public Vector2Int Pos { get; }
            public bool HasFlashedThisStep { get; private set; }
            public int Energy { get; private set; }

            private const int MaxEnergy = 9;
            private List<Octopus> neighbours;

            public Octopus(Vector2Int pos, int energy)
            {
                Pos = pos;
                Energy = energy;
                neighbours = new List<Octopus>();
            }

            public void SetNeighbours(Octopus[,] grid, int width, int height)
            {
                neighbours.Clear();

                if (Pos.x > 0)
                {
                    if (Pos.y > 0) neighbours.Add(grid[Pos.x - 1, Pos.y - 1]); // top left
                    if (Pos.y < width - 1) neighbours.Add(grid[Pos.x - 1, Pos.y + 1]); // top right
                    neighbours.Add(grid[Pos.x - 1, Pos.y]); // top middle
                }

                if (Pos.y > 0) neighbours.Add(grid[Pos.x, Pos.y - 1]); // middle left
                if (Pos.y < width - 1) neighbours.Add(grid[Pos.x, Pos.y + 1]); // middle right;

                if (Pos.x < height - 1)
                {
                    if (Pos.y > 0) neighbours.Add(grid[Pos.x + 1, Pos.y - 1]); // bottom left
                    if (Pos.y < width - 1) neighbours.Add(grid[Pos.x + 1, Pos.y + 1]); // bottom right
                    neighbours.Add(grid[Pos.x + 1, Pos.y]); // bottom middle
                }
            }

            public void IncreaseEnergy()
            {
                Energy++;
            }

            public bool CanFlash()
            {
                return Energy > MaxEnergy;
            }

            /// <summary>
            /// Returns whether it has flashed.
            /// </summary>
            public bool Flash()
            {
                if (HasFlashedThisStep)
                    return false;

                foreach (Octopus neighbour in neighbours)
                {
                    neighbour.IncreaseEnergy();
                }

                HasFlashedThisStep = true;
                return true;
            }

            public void Reset()
            {
                Energy = 0;
                HasFlashedThisStep = false;
            }
        }
    }
}
