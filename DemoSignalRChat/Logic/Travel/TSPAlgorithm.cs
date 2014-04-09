using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Logic.Travel
{
    public class TSPAlgorithm
    {
        static public List<int> GTS(int[,] cities, int SIZE)
        {
            List<int> visited = new List<int>();
            visited.Add(0);

            while (visited.Count < SIZE)
            {
                int minCost = int.MaxValue;
                int colSelect = 0;

                for (int col = 0; col < SIZE; col++)
                {
                    int row = visited.Last();
                    if (!visited.Contains(col))
                    {
                        if (cities[row, col] < minCost)
                        {
                            minCost = cities[row, col];
                            colSelect = col;
                        }
                    }// end if
                }// end for

                visited.Add(colSelect);
            }// end while
            return visited;
        }// end GTS
    }
}