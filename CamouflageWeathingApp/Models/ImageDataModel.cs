namespace CamouflageWeathingApp
{
    public class ImageDataModel
    {
        public Color ColorOne { get; set; } = Color.White;
        public Color ColorTwo { get; set; } = Color.White;
        public Color ColorThree { get; set; } = Color.White;
        public Color BackgroundColor { get; set; } = Color.White;
        public Color NetColor { get; set; } = Color.DarkGray;
        public Color DividingLineColor { get; set; } = Color.Red;
        public decimal ColorOnePercentage { get; set; } = 0;
        public decimal ColorTwoPercentage { get; set; } = 0;
        public decimal ColorThreePercentage { get; set; } = 0;
        public decimal FreePercentage { get; set; } = 100;
        public int NetOffset { get; set; } = 30;
        public int NumberOfNetCellsX { get; set; } = 120;
        public int NumberOfNetCellsY { get; set; } = 63;
        public int NetCellSize { get; set; } = 9;
        public int StartDividingLineX { get; set; } = 30;
        public int StartDividingLineY { get; set; } = 588;
        public int StartSchemeX { get; set; } = 44;
        public int StartSchemeY { get; set; } = 588;
        public SchemeColorIconModel ColorOneIcon { get; set; } = new SchemeColorIconModel 
        {
            PositionX = 30,
            PositionY = 630,
            Width = 15,
            Height = 15
        };
        public SchemeColorIconModel ColorTwoIcon { get; set; } = new SchemeColorIconModel
        {
            PositionX = 30,
            PositionY = 645,
            Width = 15,
            Height = 15
        };
        public SchemeColorIconModel ColorThreeIcon { get; set; } = new SchemeColorIconModel
        { 
            PositionX = 30, 
            PositionY = 660, 
            Width = 15, 
            Height = 15 };
        public TextModel TextModel { get; set; } = new TextModel();
        public List<MazeCellModel> Cells { get; set; } = new List<MazeCellModel>();
    }
}
