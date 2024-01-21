namespace CamouflageWeathingApp
{
    public class MazeCellModel
    {
        public Color Color { get; set; }= Color.White;
        public ColorTypeEnum ColorType { get; set; } = ColorTypeEnum.Default;
        public bool DefaultColor { get; set; } = true;
        public int SequenceNumber { get; set; } = 0;
        public MazeCellTypeEnum Type { get; set; } = MazeCellTypeEnum.BaseG;
        public List<CoordinateLineModel> CoordinateLines { get; set; } = new List<CoordinateLineModel>();
        public List<int[]> DrawingTrack { get; set; } = new List<int[]>();
    }
}
