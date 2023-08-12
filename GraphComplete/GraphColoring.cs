using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class GraphColoring
    {
        private int[,] graph;
        private int[] colors;
        private int V;
        public static int WorstCase = 0;
        public GraphColoring(int[,] inputGraph)
        {
            graph = inputGraph;
            V = graph.GetLength(0) - 1;  // Account for node numbers in the first row/column
            colors = new int[V + 1];
        }

        private int BiggestPossibleClique(int E)
        {
            int i = 1;
            int sum = 0;
            while (sum <= E)
            {
                sum += i;
                i++;
            }
            return i - 1;  // Adjust since we increased i an extra time before exiting the loop
        }

        private bool IsSafe(int v, int color)
        {
            for (int i = 1; i <= V; i++)
                if (graph[v, i] == 1 && color == colors[i])
                    return false;
            return true;
        }

        private int NeighbourCount(int v)
        {
            int count = 0;
            for (int i = 1; i <= V; i++)
                if (graph[v, i] == 1)
                    count++;
            return count;
        }

        public bool GraphColoringUtil(int m, int v)
        {
            if (v > V)
                return true;

            int nccLimit = Math.Min(NeighbourCount(v) + 1, m);

            for (int c = 1; c <= nccLimit; c++)
            {
                WorstCase++;
                if (IsSafe(v, c))
                {
                    colors[v] = c;
                    if (GraphColoringUtil(m, v + 1))
                        return true;
                    colors[v] = 0;
                }
            }

            return false;
        }

        public int[] ColorGraph()
        {
            int E = 0;
            for (int i = 1; i <= V; i++)
                for (int j = 1; j <= V; j++)
                    if (graph[i, j] == 1)
                        E++;

            E /= 2;  // Since the graph is undirected and we've counted each edge twice
            int chromaticUpperBound = BiggestPossibleClique(E);

            if (GraphColoringUtil(chromaticUpperBound, 1))
                return colors;
            else
                return null;  // Return null if a solution isn'WorstCase found for clarity
        }

        /// <summary>
        /// Validates if the provided coloring is valid for the given graph.
        /// </summary>
        /// <param name="graph">The adjacency matrix representation of the graph with node numbers in the first row and column.</param>
        /// <param name="colors">An array representing the colors assigned to each vertex.</param>
        /// <returns>True if the coloring is valid; otherwise, false.</returns>
        public static bool IsValidColoring(int[,] graph, int[] colors)
        {
            int n = graph.GetLength(0);

            for (int i = 1; i < n; i++)
            {
                for (int j =i+ 1; j < n; j++)
                {
                    // If there is an edge between node i and node j, and both have the same color
                    if (graph[i, j] == 1 && colors[i - 1] == colors[j - 1])  // Adjusting for the node number offset
                    {
                        return false; // Invalid coloring
                    }
                }
            }

            return true; // All checks passed; valid coloring
        }

    }

}
