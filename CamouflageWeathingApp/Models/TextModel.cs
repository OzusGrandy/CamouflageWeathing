namespace CamouflageWeathingApp
{
    public class TextModel
    {
        public Font FontOfSchemeName { get; set; } = new Font("Lucida Sans Unicode", 12);
        public Font BaseFont { get; set; } = new Font("Lucida Sans Unicode", 10);
        public Point StartPointNameOfColorOne { get; set; } = new Point(50, 630);
        public Point StartPointNameOfColorTwo { get; set; } = new Point(50, 645);
        public Point StartPointNameOfColorThree { get; set; } = new Point(50, 660);
        public Point StartPointNameOfScheme { get; set; } = new Point(30, 7);
        public int LowTopStringsOfNumbersStartX { get; set; } = 47;
        public int LowTopStringsOfNumbersStartY { get; set; } = 600;
        public int SideStringsOfNumbersStartX { get; set; } = 10;
        public int SideStringsOfNumbersStartY { get; set; } = 561;
        public string NameOfScheme { get; set; } = string.Empty;
        public string NameOfColorOne { get; set; } = string.Empty;
        public string NameOfColorTwo { get; set; } = string.Empty;
        public string NameOfColorThree { get; set; } = string.Empty;
    }
}
