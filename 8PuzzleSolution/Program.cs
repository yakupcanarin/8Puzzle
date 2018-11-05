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
            Console.WriteLine("  --- Ana Puzzle ---\n");
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
                    Console.Write("  "+root[i, j]);
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

            OpenList.Add(root); // kök düðümü iþlem yapýlmadý listesine ekle
            bool Solved = false;

            while (!Solved && OpenList.Count > 0)
            {
                Node currentNode = OpenList[0]; //  liste üzerinde iþlem yapýlmamýþ sýradaki düðümü çaðýr
                CloseList.Add(currentNode); // iþlem yapýlacak düðümü, iþlem yapýldý listesine yaz
                OpenList.RemoveAt(0); // iþlem yapýlmayan düðümler listesinden kaldýr

                if (currentNode.CurrentState[0,0] == 0 && currentNode.CurrentState[0, 1] == 0 && currentNode.CurrentState[0, 2] == 0)
                {
                    for (int i = 0; i < 3; i++) 
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            currentNode.CurrentState[i, j] = currentNode.Puzzle[i, j]; // iþlem yapýlacak düðümün matrisini Node sýnýfýndaki CurrentState matrisine eþitle
                        }
                    }
                }

                currentNode.BeginMove(); // Node sýnýfýndaki yapýlacak hareketleri kontrol et 
                    
                for (int i = 0; i < currentNode.Children.Count; i++) //hareketler sonrasý oluþan düðüm var mý ??
                {
                    Node currentChild = currentNode.Children[i]; // varsa oluþan ilk düðüm çözüm düðümü mü ?
                    if (currentChild.isGoalState(currentChild))
                    {
                        Console.WriteLine("Çözüm Bulundu....");
                        Solved = true;
                        CurrentPath(SolutionPath, currentChild);
                    }
                    // çözüm düðümü deðilse hareketleri kontrol edilecek iþlem yapýlmamýþ düðümler listesine ekle
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
            Node current = elem; // bulunan çözüm düðümünden baþlayarak
            path.Add(current);
            while (current.parent != null) //parent'ý boþ olana kadar
            {
                current = current.parent; // bir parent üste çýkarak her matrisi çözüm yolu listesine ekle
                path.Add(current);
            }
        }

        public static bool isListContains(List<Node> list, Node elem)
        {
            bool isContains = false; // iþlem yapýlmamýþ düðümler listesini kontrol et
            if (list.Contains(elem))
            {
                isContains = true;
            }
            return isContains;
        }
    }
}
