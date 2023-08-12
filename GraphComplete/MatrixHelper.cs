using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class MatrixHelper
    {

        public static int[,] TrimArray(int rowToRemove, int columnToRemove, int[,] originalArray)
        {
            int[,] result = new int[originalArray.GetLength(0) - 1, originalArray.GetLength(1) - 1];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                    continue;

                for (int k = 0, u = 0; k < originalArray.GetLength(1) && u < originalArray.GetLength(1) - 1 && j < originalArray.GetLength(1) - 1; k++)
                {
                    if (k == columnToRemove)
                        continue;

                    result[j, u] = originalArray[i, k];
                    
                    u++;
                }
                j++;
            }

            return result;
        }
        public static int[,] RemoveSingleLineAndColumn(int[,] matrix, int nodeNumber)
        {
            int originalSize = matrix.GetLength(0);
            int newSize = originalSize - 1;

            int[,] newMatrix = new int[newSize, newSize];

            // These will determine where we write data to the new matrix.
            int newRow = 1;
            int newCol;

            // Set node numbers for the new matrix first row and column
            for (int i = 1, j = 1; i < originalSize; i++)
            {
                if (matrix[i, 0] == nodeNumber) continue;

                newMatrix[j, 0] = matrix[i, 0];
                newMatrix[0, j] = matrix[0, i];
                j++;
            }

            for (int i = 1; i < originalSize; i++)
            {
                if (matrix[i, 0] == nodeNumber) continue; // Skip the row with the node number.

                newCol = 1;
                for (int j = 1; j < originalSize; j++)
                {
                    if (matrix[0, j] == nodeNumber) continue; // Skip the column with the node number.

                    newMatrix[newRow, newCol] = matrix[i, j];
                    newCol++;
                }
                newRow++;
            }

            return newMatrix;
        }
        public static int[,] RemoveLinesAndColumnsFromMatrix(int[,] matrix, List<int> nodesToRemove)
        {
            int[,] currentMatrix = (int[,])matrix.Clone();

            foreach (var node in nodesToRemove.Select((value, i) => new { i, value }))
            {
                currentMatrix = TrimArray(node.value - node.i, node.value - node.i, currentMatrix);
            }

            return currentMatrix;
        }
        public static int[,] GenerateRandomConnectivityMatrix(int numberOfNodes, double connectivityProb)
        {
            int[,] matrix = new int[numberOfNodes + 1, numberOfNodes + 1];  // +1 to account for node numbers
            var random = new Random();

            // Populate node numbers in the first row and column
            for (int i = 1; i <= numberOfNodes; i++)
            {
                matrix[0, i] = i;  // Node numbers in the first row
                matrix[i, 0] = i;  // Node numbers in the first column
            }

            // Populate the rest of the matrix based on connectivity probability
            for (int i = 1; i <= numberOfNodes; i++)
            {
                for (int j = 1; j <= numberOfNodes; j++) // Starting j from 1 to cover the diagonal as well
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1;
                    }
                    else if (j > i && connectivityProb > random.NextDouble())  // check probability and ensure we don't redo already done pairs
                    {
                        matrix[i, j] = 1;
                        matrix[j, i] = 1;  // because it's an undirected graph
                    }
                }
            }

            return matrix;
        }
public static List<int> GetZeroConnectivityNodeIndices(int[,] matrix, int rowNumber)
{
    List<int> zeroConnectivityNodes = new List<int>();

    int size = matrix.GetLength(0);
    
    for (int j = 1; j < size; j++)
    {
        if (matrix[rowNumber, j] == 0)
        {
            zeroConnectivityNodes.Add(matrix[0, j]);
        }
    }

    return zeroConnectivityNodes;
}
        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public static List<int> GetZeroConnectivityIndices(int[,] matrix, int lineNumber)
        {
            if (matrix == null || lineNumber >= matrix.GetLength(0) || lineNumber < 0)
            {
                throw new ArgumentException("Invalid matrix or line number.");
            }

            List<int> zeroIndices = new List<int>();

            // We start from j = 1, as j = 0 is the node number.
            for (int j = 1; j < matrix.GetLength(1); j++)
            {
                if (matrix[lineNumber, j] == 0)
                {
                    zeroIndices.Add(j);
                }
            }

            return zeroIndices;
        }

        public static int CountConnectionsInLine(int[,] matrix, int lineNumber)
        {
            if (matrix == null || lineNumber >= matrix.GetLength(0) || lineNumber < 0)
            {
                throw new ArgumentException("Invalid matrix or line number.");
            }

            // Subtract 1 because the first cell in the row is the node number.
            int count = 0;
            for (int j = 1; j < matrix.GetLength(1); j++)
            {
                if (matrix[lineNumber, j] == 1)
                {
                    count++;
                }
            }

            return count;
        }

        public static int CountTotalConnections(int[,] matrix)
        {
            int totalConnections = 0;

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                totalConnections += CountConnectionsInLine(matrix, i);
            }

            return totalConnections;
        }

        public static List<int> GetNodeIndices(int[,] matrix, List<int> nodeNumbers)
        {
            if (matrix == null || nodeNumbers == null)
            {
                throw new ArgumentException("Invalid matrix or node numbers.");
            }

            Dictionary<int, int> nodeNumberToIndex = new Dictionary<int, int>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                nodeNumberToIndex[matrix[i, 0]] = i;
            }

            List<int> indices = new List<int>();
            foreach (var nodeNumber in nodeNumbers)
            {
                if (nodeNumberToIndex.TryGetValue(nodeNumber, out int index))
                {
                    indices.Add(index);
                }
                else
                {
                    indices.Add(-1); // or throw an exception if a node number is not found
                }
            }

            return indices;
        }
    }
}
