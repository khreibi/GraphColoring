


using Graph;
using System;


var rand = new Random();

int i = 0;
int[,] matrix;
int[] result;
int worstCase = 0;
do
{
    i++;
    var p = rand.NextDouble();
    matrix = MatrixHelper.GenerateRandomConnectivityMatrix(1000, p);
    //MatrixHelper.PrintMatrix(matrix);

    Console.WriteLine("==============================================================================================================");
    GraphColoring gc = new GraphColoring(matrix);
    result = gc.ColorGraph().Skip(1).ToArray();
    foreach (var item in result)
    {
        Console.Write($"{item}\t");
    }
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine($"WorstCase ={GraphColoring.WorstCase}| i={i}");
    if(GraphColoring.WorstCase > worstCase)
    {
        worstCase = GraphColoring.WorstCase;
    }
    GraphColoring.WorstCase = 0;
    Console.WriteLine($"worstCase{worstCase}");

}
while (GraphColoring.IsValidColoring(matrix, result));








/*do
{
    var n = rand.Next(1, 23);
    var p = rand.NextDouble();
    var matrix = MatrixHelper.GenerateRandomConnectivityMatrix(30, p);
    MatrixHelper.PrintMatrix(matrix);

    algoResult = Algorithm.MyAlgo(matrix,new int[] {}).GetLength(0) -1;
    Console.WriteLine("==============================================================================================================");
    Console.WriteLine(MatrixHelper.CountTotalConnections(matrix) / Math.Pow(matrix.GetLength(0) - 1, 2));
    Console.WriteLine("==============================================================================================================");
    Console.WriteLine("===================================================backtracking result========================================================");
    var backtracking = new CliqueProblem(matrix);
    result = backtracking.FindMaxClique();
    result.ForEach(e => Console.Write($"{e}\WorstCase"));
    Console.WriteLine("\n--------------------");
    Console.WriteLine(result.Count);
    Console.WriteLine("================================================DebugInfo=============================================================");
    Console.WriteLine($"n : {n} | p :{p}");
    Console.WriteLine($"============================================={algoResult} | {result.Count}=================================================================");
    MatrixHelper.PrintMatrix(matrix);
    Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
}
while (algoResult != result.Count);*/