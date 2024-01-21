namespace CamouflageWeathingTest
{
    public class DistributionAlgorithm
    {
        private List<Cell> _cells = new List<Cell>();

        public DistributionAlgorithm() 
        {
            for (int i = 0; i < 20; i++) 
            {
                for(int j = 0; j < 40; j++)
                {
                    _cells.Add(new Cell { Y = i, X = j, Color = Color.Default});
                }
            }
        }

        public List<Cell> CalculateCells(int colorOnePerc, int colorTwoPerc, int colorThreePerc)
        {
            colorOnePerc = colorOnePerc + colorThreePerc / 2;
            colorTwoPerc = colorTwoPerc + (colorThreePerc - colorThreePerc / 2);

            return _cells;
        }
    }

    public class Cell
    {
        public int Y { get; set; }
        public int X { get; set; }
        public Color Color { get; set; }
    }

    public enum Color
    {
        One,
        Two, 
        Three,
        Default
    }
}
