using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_5 : Problem_2024
    {
        public override int Number => 5;

        Dictionary<int, Order> orders = new();

        public override void Run()
        {
            //UseTestInput();
            int secondPartIndex = -1;

            for (int i = 0; i < Lines.Length; i++)
            {
                if (string.IsNullOrEmpty(Lines[i]))
                {
                    secondPartIndex = i + 1;
                    break;
                }

                int[] ints = Lines[i].Split('|').Select(int.Parse).ToArray();
                int n1 = ints[0];
                int n2 = ints[1];

                if (!orders.TryGetValue(n1, out Order order1))
                    order1 = new Order();
                if (!orders.TryGetValue(n2, out Order order2))
                    order2 = new Order();
                order1.SetBefore(n2);
                order2.SetAfter(n1);
                orders[n1] = order1;
                orders[n2] = order2;
            }

            int firstStar = 0;
            int secondStar = 0;
            for (int i = secondPartIndex; i < Lines.Length; i++)
            {
                int[] update = Lines[i].Split(',').Select(int.Parse).ToArray();
                if (IsCorrect(update))
                {
                    firstStar += update[update.Length / 2];
                }
                else
                {
                    int[] correctedOrder = TopologicalSort(update);
                    secondStar += correctedOrder[correctedOrder.Length / 2];
                }
            }

            Console.WriteLine($"First star: {firstStar}");
            Console.WriteLine($"Second star: {secondStar}");
        }

        int[] TopologicalSort(int[] update)
        {
            Dictionary<int, int> inDegree = new();
            Dictionary<int, List<int>> graph = new();

            // Initialize graph and in-degree counts
            foreach (int page in update)
            {
                inDegree[page] = 0;
                graph[page] = new List<int>();
            }

            foreach (int page in update)
            {
                foreach (int afterPage in orders[page].After)
                {
                    if (update.Contains(afterPage))
                    {
                        graph[page].Add(afterPage);
                        inDegree[afterPage]++;
                    }
                }
            }

            // Find all pages with no dependencies (in-degree 0)
            Queue<int> queue = new();
            foreach (KeyValuePair<int, int> kvp in inDegree)
            {
                if (kvp.Value == 0)
                {
                    queue.Enqueue(kvp.Key);
                }
            }

            // Perform topological sort
            List<int> sortedPages = new();
            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                sortedPages.Add(current);

                foreach (int neighbor in graph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return sortedPages.ToArray();
        }

        bool IsCorrect(int[] update)
        {
            for (int i = 0; i < update.Length; i++)
            {
                // Check before
                for (int j = 0; j < i; j++)
                {
                    if (orders[update[j]].IsAfter(update[i]))
                        return false;
                }
                // Check after
                for (int j = i + 1; j < update.Length; j++)
                {
                    if (orders[update[j]].IsBefore(update[i]))
                        return false;
                }
            }
            return true;
        }

        class Order
        {
            public void SetAfter(int n)
            {
                _before.Add(n);
            }

            public void SetBefore(int n)
            {
                _after.Add(n);
            }

            public bool IsBefore(int n)
            {
                return _after.Contains(n);
            }

            public bool IsAfter(int n)
            {
                return _before.Contains(n);
            }

            public IReadOnlyList<int> After => _after;

            private readonly List<int> _before = new();
            private readonly List<int> _after = new();
        }
    }
}
