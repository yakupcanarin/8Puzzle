using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSolution
{
    class Node
    {
        public List<Node> Children = new List<Node>();
        public Node parent;
        public int[,] Puzzle = new int[3, 3];
        public int[,] CopiedArray = new int[3, 3];
        public int[,] CurrentState = new int[3, 3];
        public int[,] FinalState = new int[3, 3] {  {1, 2, 3},
                                                    {4, 5, 6},
                                                    {7, 8, 0}};

        public Node(int[,] State)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Puzzle[i, j] = State[i, j];
                }
            }
        }

        public void BeginMove()
        {
            int m = 0;
            int n = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (CurrentState[i, j] == 0)
                    {
                        m = i;
                        n = j;
                    }
                }
            }

            MoveRight(CurrentState, m, n);
            MoveLeft(CurrentState, m, n);
            MoveUp(CurrentState, m, n);
            MoveDown(CurrentState, m, n);
        }

        public void MoveRight(int[,] array, int row, int column)
        {
            if (column != 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        CopiedArray[i, j] = array[i, j];
                    }
                }
                int empty = CopiedArray[row, column + 1];
                CopiedArray[row, column + 1] = CopiedArray[row, column];
                CopiedArray[row, column] = empty;

                Node child = new Node(CopiedArray);
                child.parent = this;
                Children.Add(child);
            }
        }

        public void MoveLeft(int[,] array, int row, int column)
        {
            if (column != 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        CopiedArray[i, j] = array[i, j];
                    }
                }
                int empty = CopiedArray[row, column - 1];
                CopiedArray[row, column - 1] = CopiedArray[row, column];
                CopiedArray[row, column] = empty;

                Node child = new Node(CopiedArray);
                child.parent = this;
                Children.Add(child);
            }
        }

        public void MoveUp(int[,] array, int row, int column)
        {
            if (row != 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        CopiedArray[i, j] = array[i, j];
                    }
                }
                int empty = CopiedArray[row - 1, column];
                CopiedArray[row - 1, column] = CopiedArray[row, column];
                CopiedArray[row, column] = empty;

                Node child = new Node(CopiedArray);
                child.parent = this;
                Children.Add(child);
                
            }
        }

        public void MoveDown(int[,] array, int row, int column)
        {
            if (row != 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        CopiedArray[i, j] = array[i, j];
                    }
                }
                int empty = CopiedArray[row + 1, column];
                CopiedArray[row + 1, column] = CopiedArray[row, column];
                CopiedArray[row, column] = empty;

                Node child = new Node(CopiedArray);
                child.parent = this;
                Children.Add(child);
            }
        }

        public bool isGoalState(Node node)
        {
            bool isGoal = false;
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (node.Puzzle[i,j]== FinalState[i,j])
                    {
                        count++;
                    }
                }
            }
            if (count == 9)
            {
                isGoal = true;
            }
            return isGoal;
        }

        public void PrintPuzzle(Node node)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(node.Puzzle[i, j] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }

    }
}
