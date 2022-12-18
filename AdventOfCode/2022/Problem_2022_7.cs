using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2022_7 : Problem_2022
    {
        public override int Number => 7;

        private Node root;
        private Node currentNode;
        private HashSet<Node> directories = new HashSet<Node>();

        protected override void InitInternal()
        {
            string rootName = "/";
            root = new Node(name: rootName);
            directories.Add(root);
            currentNode = root;

            foreach (string line in Lines)
            {
                if (line == "$ ls")
                    continue;

                if (line.StartsWith("$ cd"))
                {
                    string dirname = line.Split("$ cd ")[1];
                    if (dirname == rootName)
                    {
                        currentNode = root;
                    }
                    else if (dirname == "..")
                    {
                        currentNode = currentNode.Parent;
                    }
                    else
                    {
                        currentNode = currentNode.Children[dirname];
                    }
                }
                else
                {
                    if (line.StartsWith("dir"))
                    {
                        string dirName = line.Split("dir ")[1];
                        var dir = new Node(name: dirName);
                        currentNode.AddChild(dir);
                        directories.Add(dir); // NOTE: different directories can have the same name when at different locations
                    }
                    else
                    {
                        string[] args = line.Split(" ");
                        currentNode.AddChild(new Node(name: args[1], fileSize: int.Parse(args[0])));
                    }
                }
            }

            base.InitInternal();
        }

        public override void Run()
        {
            //Console.WriteLine(root.GetHierarchyAsString()); // DEBUG

            Console.WriteLine($"First star: {directories.Select(d => d.Size).Where(s => s <= 100000).Sum()}");

            int totalSpace = 70000000;
            int requiredSpace = 30000000;
            int unusedSpace = totalSpace - root.Size;
            int toDeleteSpace = requiredSpace - unusedSpace;
            int min = int.MaxValue;

            foreach (var dir in directories)
            {
                int delta = toDeleteSpace - dir.Size;
                if (delta < 0 && -delta < min)
                {
                    min = -delta;
                }
            }
            
            Console.WriteLine($"Second star: {min + toDeleteSpace}");
        }

        class Node
        {
            public Node(string name, int fileSize = 0)
            {
                Name = name;
                FileSize = fileSize;
                Children = new Dictionary<string, Node>();
            }

            public string Name { get; }
            public int FileSize { get; private set; }
            public int Size
            {
                get
                {
                    if (FileSize != 0)
                        return FileSize;

                    return Children.Values.Sum(c => c.Size);
                }
            }

            public Node Parent = null;
            public Dictionary<string, Node> Children { get; }

            public void AddChild(Node child)
            {
                if (Children.ContainsKey(child.Name))
                    return;

                Children.Add(child.Name, child);
                child.Parent = this;
            }

            #region Debug

            public string GetHierarchyAsString()
            {
                return GetHierarchyAsStringRecur(level: 0);
            }

            private string GetHierarchyAsStringRecur(int level)
            {
                if (FileSize != 0)
                    return GetIndentedString(level, $"- {Name} (file, size={FileSize})\n");

                string result = GetIndentedString(level, $"- {Name} (dir)\n");
                foreach (Node child in Children.Values)
                {
                    result += child.GetHierarchyAsStringRecur(level + 1);
                }
                return result;
            }

            private string GetIndentedString(int level, string str)
            {
                string indent = "";
                for (int i = 0; i < level; i++)
                {
                    indent += "  ";
                }
                return indent + str;
            }

            #endregion
        }
    }
}
