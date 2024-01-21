namespace CamouflageWeathingApp
{
    public class RuntimePaintingBitmap
    {
        public int PictureHeight { get; private set; }
        public int PictureWidth { get; private set; }
        public bool IsDrawingScheme { get; set; } = false;

        private Bitmap _bitmap;
        private Graphics _graphics;

        public RuntimePaintingBitmap(int pictureHeight, int pictureWidth)
        {
            PictureHeight = pictureHeight;
            PictureWidth = pictureWidth;
            _bitmap = new Bitmap(PictureHeight, PictureWidth);
            _graphics = Graphics.FromImage(_bitmap);
        }


        public Bitmap Rendering(ImageDataModel imageData)
        {
            FillBackground(
                PictureHeight,
                PictureWidth,
                imageData.BackgroundColor);

            RenderingNet(
                imageData.NetOffset,
                imageData.NumberOfNetCellsX,
                imageData.NumberOfNetCellsY,
                imageData.NetCellSize,
                imageData.NetColor);

            RenderingSchemeColorIcons(
                imageData.ColorOneIcon,
                imageData.ColorTwoIcon,
                imageData.ColorThreeIcon,
                imageData.ColorOne,
                imageData.ColorTwo,
                imageData.ColorThree);

            RenderingText(
                imageData.TextModel,
                imageData.ColorOnePercentage,
                imageData.ColorTwoPercentage,
                imageData.ColorThreePercentage);

            RenderingDividingLines(
                new Pen(imageData.DividingLineColor, 2),
                imageData.StartDividingLineX,
                imageData.StartDividingLineY);

            if (IsDrawingScheme)
            {
                RenderingScheme(imageData.Cells);
            }

            return _bitmap;
        }

        private void FillBackground(
            int width, 
            int height, 
            Color color)
        {
            SolidBrush solidBrush = new SolidBrush(color);

            _graphics.FillRectangle(solidBrush, 0, 0, width, height);
        }

        private void RenderingNet(
            int offset, 
            int numOfCellsX, 
            int numOfCellsY, 
            int cellSize, 
            Color netColor)
        {
            Pen pen = new Pen(netColor);

            for (int y = 0; y <= numOfCellsY; ++y)
            {
                _graphics.DrawLine(
                    pen, 
                    offset, 
                    y * cellSize + offset, 
                    numOfCellsX * cellSize + offset, 
                    y * cellSize + offset);
            }

            for (int x = 0; x <= numOfCellsX; ++x)
            {
                _graphics.DrawLine(
                    pen, 
                    x * cellSize + offset, 
                    offset, x * cellSize + offset, 
                    numOfCellsY * cellSize + offset);
            }
        }

        private void RenderingSchemeColorIcons(
            SchemeColorIconModel iconOne, 
            SchemeColorIconModel iconTwo,
            SchemeColorIconModel iconThree,
            Color colorOne,
            Color colorTwo,
            Color colorThree)
        {
            _graphics.FillRectangle(
                new SolidBrush(colorOne),
                iconOne.PositionX,
                iconOne.PositionY,
                iconOne.Width,
                iconOne.Height);
            _graphics.FillRectangle(
                new SolidBrush(colorTwo),
                iconTwo.PositionX,
                iconTwo.PositionY,
                iconTwo.Width,
                iconTwo.Height);
            _graphics.FillRectangle(
                new SolidBrush(colorThree),
                iconThree.PositionX,
                iconThree.PositionY,
                iconThree.Width,
                iconThree.Height);
        }

        private void RenderingText(
            TextModel textModel,
            decimal colorOnePerc,
            decimal colorTwoPerc,
            decimal colorThreePerc)
        {
            _graphics.DrawString(
                textModel.NameOfScheme, 
                textModel.FontOfSchemeName, 
                Brushes.Black, 
                textModel.StartPointNameOfScheme);

            _graphics.DrawString(
                colorOnePerc.ToString() + "% " + textModel.NameOfColorOne, 
                textModel.BaseFont, 
                Brushes.Black, 
                textModel.StartPointNameOfColorOne);
            _graphics.DrawString(
                colorTwoPerc.ToString() + "% " + textModel.NameOfColorTwo, 
                textModel.BaseFont, 
                Brushes.Black, 
                textModel.StartPointNameOfColorTwo);
            _graphics.DrawString(
                colorThreePerc.ToString() + "% " + textModel.NameOfColorThree, 
                textModel.BaseFont, 
                Brushes.Black, 
                textModel.StartPointNameOfColorThree);

            int lowTopStringOfNumberStartX = textModel.LowTopStringsOfNumbersStartX;
            int lowTopStringOfNumberStartY = textModel.LowTopStringsOfNumbersStartY;

            for (int i = 0; i < 39; i++)
            {
                if (i == 0)
                {
                    _graphics.DrawString(
                        $"{i + 1}", 
                        textModel.BaseFont, 
                        Brushes.Black, 
                        new Point(lowTopStringOfNumberStartX, lowTopStringOfNumberStartY));
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black, 
                        new Point(lowTopStringOfNumberStartX + 171, lowTopStringOfNumberStartY - 590));
                }
                else if ((i > 0 && i < 9) || (i > 9 && i < 33))
                {
                    lowTopStringOfNumberStartX = lowTopStringOfNumberStartX + 27;
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(lowTopStringOfNumberStartX, lowTopStringOfNumberStartY));
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(lowTopStringOfNumberStartX + 171, lowTopStringOfNumberStartY - 590));
                }
                else if (i == 9)
                {
                    lowTopStringOfNumberStartX = lowTopStringOfNumberStartX + 23;
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(lowTopStringOfNumberStartX, lowTopStringOfNumberStartY));
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(lowTopStringOfNumberStartX + 171, lowTopStringOfNumberStartY - 590));
                }
                else if (i >= 33)
                {
                    lowTopStringOfNumberStartX = lowTopStringOfNumberStartX + 27;
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(lowTopStringOfNumberStartX, lowTopStringOfNumberStartY));
                }
            }

            int sideStringsOfNumbersStartX = textModel.SideStringsOfNumbersStartX;
            int sideStringsOfNumbersStartY = textModel.SideStringsOfNumbersStartY;

            for (int i = 0; i < 20; i++)
            {
                if (i == 0)
                {
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont, 
                        Brushes.Black, 
                        new Point(sideStringsOfNumbersStartX, sideStringsOfNumbersStartY));
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(sideStringsOfNumbersStartX + 1105, sideStringsOfNumbersStartY));
                }
                else if ((i > 0 && i < 9) || i > 9)
                {
                    sideStringsOfNumbersStartY = sideStringsOfNumbersStartY - 27;
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(sideStringsOfNumbersStartX, sideStringsOfNumbersStartY));
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(sideStringsOfNumbersStartX + 1105, sideStringsOfNumbersStartY));
                }
                else if (i == 9)
                {
                    sideStringsOfNumbersStartY = sideStringsOfNumbersStartY - 27;
                    sideStringsOfNumbersStartX = sideStringsOfNumbersStartX - 4;
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(sideStringsOfNumbersStartX, sideStringsOfNumbersStartY));
                    _graphics.DrawString(
                        $"{i + 1}",
                        textModel.BaseFont,
                        Brushes.Black,
                        new Point(sideStringsOfNumbersStartX + 1105, sideStringsOfNumbersStartY));
                }
            }
        }

        private void RenderingDividingLines(
            Pen pen, 
            int startDividingLineX, 
            int startDividingLineY)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == 0)
                {
                    int startX = startDividingLineX;
                    int startY = startDividingLineY - 405;

                    for (int j = 0; j < 6; j++)
                    {
                        if (j == 0)
                        {
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 13, 
                                startY - 13);
                            _graphics.DrawLine(
                                pen, 
                                startX + 13, 
                                startY - 13, 
                                startX + 4, 
                                startY - 22);
                        }
                        else if (j == 1)
                        {
                            startX = startX + 4;
                            startY = startY - 22;
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 18, 
                                startY - 18);
                            _graphics.DrawLine(
                                pen, 
                                startX + 18, 
                                startY - 18, 
                                startX + 9, 
                                startY - 27);
                        }
                        else if (j > 1 && j < 5)
                        {
                            startX = startX + 9;
                            startY = startY - 27;
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 18, 
                                startY - 18);
                            _graphics.DrawLine(
                                pen, 
                                startX + 18, 
                                startY - 18, 
                                startX + 9, 
                                startY - 27);
                        }
                        else if (j == 5)
                        {
                            startX = startX + 9;
                            startY = startY - 27;
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 18, 
                                startY - 18);
                        }
                    }
                }
                else if (i > 0 && i < 8)
                {
                    int startX = startDividingLineX;
                    int startY = startDividingLineY;
                    for (int j = 0; j < 21; j++)
                    {
                        if (j == 0)
                        {
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 13, 
                                startY - 13);
                            _graphics.DrawLine(
                                pen, 
                                startX + 13, 
                                startY - 13, 
                                startX + 4, 
                                startY - 22);
                        }
                        else if (j == 1)
                        {
                            startX = startX + 4;
                            startY = startY - 22;
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 18, 
                                startY - 18);
                            _graphics.DrawLine(
                                pen, 
                                startX + 18, 
                                startY - 18, 
                                startX + 9, 
                                startY - 27);
                        }
                        else if (j > 1 && j < 20)
                        {
                            startX = startX + 9;
                            startY = startY - 27;
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 18, 
                                startY - 18);
                            _graphics.DrawLine(
                                pen, 
                                startX + 18, 
                                startY - 18, 
                                startX + 9, 
                                startY - 27);
                        }
                        else if (j == 20)
                        {
                            startX = startX + 9;
                            startY = startY - 27;
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 18, 
                                startY - 18);
                        }
                    }
                    startDividingLineX = startDividingLineX + 135;
                }
                else if (i == 8)
                {
                    int startX = startDividingLineX;
                    int startY = startDividingLineY;
                    for (int j = 0; j < 15; j++)
                    {
                        if (j == 0)
                        {
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 13, 
                                startY - 13);
                            _graphics.DrawLine(
                                pen, 
                                startX + 13, 
                                startY - 13, 
                                startX + 4, 
                                startY - 22);
                        }
                        else if (j == 1)
                        {
                            startX = startX + 4;
                            startY = startY - 22;
                            _graphics.DrawLine(
                                pen, startX, 
                                startY, 
                                startX + 18, 
                                startY - 18);
                            _graphics.DrawLine(
                                pen, 
                                startX + 18, 
                                startY - 18, 
                                startX + 9, 
                                startY - 27);
                        }
                        else if (j > 1 && j < 14)
                        {
                            startX = startX + 9;
                            startY = startY - 27;
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 18, 
                                startY - 18);
                            _graphics.DrawLine(
                                pen, 
                                startX + 18, 
                                startY - 18, 
                                startX + 9, 
                                startY - 27);
                        }
                        else if (j == 14)
                        {
                            startX = startX + 9;
                            startY = startY - 27;
                            _graphics.DrawLine(
                                pen, 
                                startX, 
                                startY, 
                                startX + 14, 
                                startY - 14);
                        }
                    }
                }
            }

        }

        private void RenderingScheme(List<MazeCellModel> cells)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                DrawingG(cells[i].DrawingTrack, cells[i].Color);
                //DrawingGAreaTest(cells[i].CoordinateLines, cells[i].Color);
            }
        }
        private void DrawingG(List<int[]> coordinates, Color color)
        {
            Pen pen = new Pen(color, 2);
            for (int i = 0; i < coordinates.Count; i++)
            {
                _graphics.DrawLine(
                pen,
                coordinates[i][0],
                coordinates[i][1],
                coordinates[i][2],
                coordinates[i][3]);
            }
        }

        private void DrawingGAreaTest(List<CoordinateLineModel> coordinates, Color color)
        {
            Pen pen = new Pen(color, 2);
            for (int i = 0; i < coordinates.Count; i++)
            {
                _graphics.DrawLine(
                pen,
                coordinates[i].CoordinateX,
                coordinates[i].CoordinatesY[0],
                coordinates[i].CoordinateX,
                coordinates[i].CoordinatesY[1]);
            }
        }

    }
}
