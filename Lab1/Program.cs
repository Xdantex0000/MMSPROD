internal class Program
{
    private static async Task Main(string[] args)
    {
        int N = 25;
        var matrix = GetRandomMatrix(N);
        var vector = GetRandomVector(N);

        var multipliedMatrix = await ParallelMultiply(matrix, vector);

        Console.WriteLine("Multiplied matrix:");
        Print(multipliedMatrix);
         
        Console.ReadKey();
    }

    private static async Task<int[,]> ParallelMultiply(int[,] matrix, int[] vector)
    {
        var result = new int[vector.Length, 1];
        var tasks = new List<Task>();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            int temp_i = i;
            tasks.Add(Task.Run(() =>
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[temp_i, 0] += matrix[temp_i, j] * vector[j];
                }
            }));
        }

        await Task.WhenAll(tasks);
        return result;
    }

    private static int[,] GetRandomMatrix(int size, bool toPrint = true)
    {
        int minRandValue = 0;
        int maxRandValue = 100;
        var matrix = new int[size, size];
        var rand = new Random();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = rand.Next(minRandValue, maxRandValue);
            }
        }

        if (toPrint)
        {
            Console.WriteLine("Generated matrix: ");
            Print(matrix);
        }

        return matrix;
    }

    private static int[] GetRandomVector(int size, bool toPrint = true)
    {
        int minRandValue = 0;
        int maxRandValue = 100;
        var vector = new int[size];
        var rand = new Random();

        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] = rand.Next(minRandValue, maxRandValue);
        }

        if (toPrint)
        {
            Console.WriteLine("Generated vector: ");
            Print(vector);
        }

        return vector;
    }

    private static void Print(int[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", matrix[i, j]));
            }
            Console.Write(Environment.NewLine);
        }

        Console.WriteLine();
    }

    private static void Print(int[] vector)
    {
        int rowLength = vector.GetLength(0);

        for (int i = 0; i < rowLength; i++)
        {
            Console.WriteLine(string.Format("{0} ", vector[i]));
        }

        Console.WriteLine();
    }
}