using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] root = new int[3, 3] { { 1,0,2},{4,5,3 },{7,8,6 } };
            int[,] finalState = new int[3, 3] {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 0}};

            List<int> usedNumbers = new List<int>();
            Random rnd = new Random();
            int rndNum;
            Console.WriteLine("----- Ana Puzzle -----");
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        rndNum = rnd.Next(0, 9);
            //        if (i == 0 && j == 0)
            //        {
            //            root[i, j] = rndNum;
            //            usedNumbers.Add(rndNum);
            //        }
            //        else
            //        {
            //            while (usedNumbers.Contains(rndNum))
            //            {
            //                rndNum = rnd.Next(0, 9);
            //            }
            //            root[i, j] = rndNum;
            //            usedNumbers.Add(rndNum);
            //        }
            //        Console.Write(root[i, j] + "  ");
            //    }
            //    Console.WriteLine();
            //}

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(root[i, j] + "  ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n\n");

            Node FirstState = new Node(root);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    FirstState.CurrentState[i, j] = root[i, j];
                }
            }

            List<Node> solution = Algorithm(FirstState);

            if (solution.Count > 0)
            {
                solution.Reverse();
                foreach (var item in solution)
                {
                   item.PrintPuzzle(item);
                }
            }
            else
            {
                Console.WriteLine("Çözüm getirilirken hata!");
            }
            Console.ReadLine();
        }

        public static List<Node> Algorithm(Node root)
        {
            List<Node> SolutionPath = new List<Node>();
            List<Node> OpenList = new List<Node>();
            List<Node> CloseList = new List<Node>();

            OpenList.Add(root);
            bool Solved = false;

            while (!Solved && OpenList.Count > 0)
            {
                Node currentNode = OpenList[0];
                CloseList.Add(currentNode);
                OpenList.RemoveAt(0);

                if (currentNode.CurrentState[0,0] == 0 && currentNode.CurrentState[0, 1] == 0 && currentNode.CurrentState[0, 2] == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            currentNode.CurrentState[i, j] = currentNode.Puzzle[i, j];
                        }
                    }
                }

                currentNode.BeginMove();
                
                for (int i = 0; i < currentNode.Children.Count; i++)
                {
                    Node currentChild = currentNode.Children[i];
                    if (currentChild.isGoalState(currentChild))
                    {
                        Console.WriteLine("Çözüm Bulundu....");
                        Solved = true;
                        CurrentPath(SolutionPath, currentChild);
                    }

                    if (!isListContains(OpenList, currentChild) && !isListContains(CloseList, currentChild))
                    {
                        OpenList.Add(currentChild);
                    }
                }
            }

            return SolutionPath;
        }

        public static void CurrentPath(List<Node> path, Node elem)
        {
            Console.WriteLine("Çözüm Yazdýrýlýyor...\n");
            Node current = elem;
            path.Add(current);
            while (current.parent != null)
            {
                current = current.parent;
                path.Add(current);
            }
        }

        public static bool isListContains(List<Node> list, Node elem)
        {
            bool isContains = false;
            if (list.Contains(elem))
            {
                isContains = true;
            }
            return isContains;
        }
    }
}
