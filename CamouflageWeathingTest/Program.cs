namespace CamouflageWeathingTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DistributionAlgorithm algorithm = new DistributionAlgorithm();
            Drawing(algorithm.CalculateCells(0,0,0));
        }

        private static void Drawing(List<Cell> cells)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (i > 0)
                {
                    if (cells[i].Y > cells[i - 1].Y)
                    {
                        Console.WriteLine();
                    }
                }
                switch (cells[i].Color)
                {
                    case Color.One:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('\u2588');
                        Console.Write('\u2588');
                        break;
                    case Color.Two:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('\u2588');
                        Console.Write('\u2588');
                        break;
                    case Color.Three:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('\u2588');
                        Console.Write('\u2588');
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('\u2588');
                        Console.Write('\u2588');
                        break;
                }
            }
        }
    }
}