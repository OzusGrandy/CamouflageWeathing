using System.Drawing;

namespace CamouflageWeathingApp
{
    public class DataController
    {

        public ImageDataModel ImageData { get; private set; }

        public DataController()
        {
            ImageData = new ImageDataModel();
            FillingCellsData();
        }

        public decimal SetColorOnePercentage(decimal percentages)
        {
            ImageData.ColorOnePercentage = CheckPercentage(
                percentages, 
                ImageData.ColorTwoPercentage, 
                ImageData.ColorThreePercentage);
            return ImageData.ColorOnePercentage;
        }

        public decimal SetColorTwoPercentage(decimal percentages)
        {
            ImageData.ColorTwoPercentage = CheckPercentage(
                percentages, 
                ImageData.ColorThreePercentage, 
                ImageData.ColorOnePercentage);
            return ImageData.ColorTwoPercentage;
        }

        public decimal SetColorThreePercentage(decimal percentages)
        {
            ImageData.ColorThreePercentage = CheckPercentage(
                percentages, 
                ImageData.ColorOnePercentage, 
                ImageData.ColorTwoPercentage);
            return ImageData.ColorThreePercentage;
        }

        public void GenerateMazeCells()
        {
            ImageData.Cells = new List<MazeCellModel>();
            FillingCellsData();
            Random random = new Random();
            List<int> randomCellsOne = new List<int>();
            List<int> randomCellsTwo = new List<int>();
            List<int> randomCellsThree = new List<int>();
            List<MazeCellModel> resultList = new List<MazeCellModel>();
            int currentNumber = 0;

            decimal numberOfCells = ImageData.ColorOnePercentage * 8;

            if (numberOfCells > 0)
            {
                while (numberOfCells > 0)
                {
                    currentNumber = random.Next(0, 799);
                    if (!randomCellsOne.Contains(currentNumber))
                    {
                        randomCellsOne.Add(currentNumber);
                        numberOfCells--;
                    }
                }
                foreach (int cell in randomCellsOne)
                {
                    ImageData.Cells[cell].Color = ImageData.ColorOne;
                    ImageData.Cells[cell].DefaultColor = false;
                    ImageData.Cells[cell].ColorType = ColorTypeEnum.ColorOne;
                }
            }

            numberOfCells = ImageData.ColorTwoPercentage * 8;

            if (numberOfCells > 0)
            {
                var resultColorTwo = (from c in ImageData.Cells where c.DefaultColor == true select c).ToList();

                while (numberOfCells > 0)
                {
                    currentNumber = random.Next(0, resultColorTwo.Count - 1);
                    if (!randomCellsTwo.Contains(resultColorTwo[currentNumber].SequenceNumber))
                    {
                        randomCellsTwo.Add(resultColorTwo[currentNumber].SequenceNumber);
                        numberOfCells--;
                        resultColorTwo.Remove(resultColorTwo[currentNumber]);
                    }
                }
                foreach (int cell in randomCellsTwo)
                {
                    ImageData.Cells[cell].Color = ImageData.ColorTwo;
                    ImageData.Cells[cell].DefaultColor = false;
                    ImageData.Cells[cell].ColorType = ColorTypeEnum.ColorTwo;
                }
            }

            numberOfCells = ImageData.ColorThreePercentage * 8;

            if (numberOfCells > 0)
            {
                var resultColorThree = (from c in ImageData.Cells where c.DefaultColor == true select c).ToList();
                while (numberOfCells > 0)
                {
                    currentNumber = random.Next(0, resultColorThree.Count - 1);
                    if (!randomCellsThree.Contains(resultColorThree[currentNumber].SequenceNumber))
                    {
                        randomCellsThree.Add(resultColorThree[currentNumber].SequenceNumber);
                        numberOfCells--;
                        resultColorThree.Remove(resultColorThree[currentNumber]);
                    }
                }
                foreach (int cell in randomCellsThree)
                {
                    ImageData.Cells[cell].Color = ImageData.ColorThree;
                    ImageData.Cells[cell].DefaultColor = false;
                    ImageData.Cells[cell].ColorType = ColorTypeEnum.ColorThree;
                }
            }

            for (int i = 0; i < 800; i++)
            {
                if (ImageData.Cells[i].DefaultColor)
                {
                    ImageData.Cells[i].Color = ImageData.ColorThree;
                    ImageData.Cells[i].DefaultColor = false;
                    ImageData.Cells[i].ColorType = ColorTypeEnum.ColorThree;
                    ImageData.ColorThreePercentage = ImageData.ColorThreePercentage + 0.125m;
                    ImageData.FreePercentage = ImageData.FreePercentage - 0.125m;
                }
            }
        }

        public void ChangeColor(Color sameColor, Color newColor)
        {
            foreach (var cell in ImageData.Cells)
            {
                if (cell.Color == sameColor)
                {
                    cell.Color = newColor;
                }
            }
        }

        public void ManualEditing(int locationX, int locationY, ColorNameComboBoxModel color)
        {
            int changedCell = 0;
            bool isFound = false;
            foreach (var cell in ImageData.Cells)
            {
                foreach (var line in cell.CoordinateLines)
                {
                    if (line.CoordinateX == locationX &&
                        line.CoordinatesY[0] <= locationY &&
                        line.CoordinatesY[1] >= locationY)
                    {
                        changedCell = cell.SequenceNumber;
                        isFound = true;
                        break;
                    }
                    if (isFound)
                    {
                        break;
                    }
                }
            }

            if (ImageData.Cells[changedCell].Color != color.Color)
            {
                ImageData.Cells[changedCell].Color = color.Color;

                switch (color.Type)
                {
                    case ColorTypeEnum.ColorOne:
                        if (ImageData.ColorOnePercentage < 100m)
                        {
                            ImageData.ColorOnePercentage = ImageData.ColorOnePercentage + 0.125m;
                        }
                        break;
                    case ColorTypeEnum.ColorTwo:
                        if (ImageData.ColorTwoPercentage < 100m)
                        {
                            ImageData.ColorTwoPercentage = ImageData.ColorTwoPercentage + 0.125m;
                        }
                        break;
                    case ColorTypeEnum.ColorThree:
                        if (ImageData.ColorThreePercentage < 100m)
                        {
                            ImageData.ColorThreePercentage = ImageData.ColorThreePercentage + 0.125m;
                        }
                        break;
                }

                switch (ImageData.Cells[changedCell].ColorType)
                {
                    case ColorTypeEnum.ColorOne:
                        if (ImageData.ColorOnePercentage > 0)
                        {
                            ImageData.ColorOnePercentage = ImageData.ColorOnePercentage - 0.125m;
                        }
                        break;
                    case ColorTypeEnum.ColorTwo:
                        if (ImageData.ColorTwoPercentage > 0)
                        {
                            ImageData.ColorTwoPercentage = ImageData.ColorTwoPercentage - 0.125m;
                        }
                        break;
                    case ColorTypeEnum.ColorThree:
                        if (ImageData.ColorThreePercentage > 0)
                        {
                            ImageData.ColorThreePercentage = ImageData.ColorThreePercentage - 0.125m;
                        }
                        break;
                }

                ImageData.Cells[changedCell].ColorType = color.Type;
            }
        }

        private decimal CheckPercentage(
            decimal percentage,
            decimal otherColorOne,
            decimal otherColorTwo)
        {
            decimal awailablePercentages = (100 - otherColorOne) - otherColorTwo;
            if (awailablePercentages >= percentage)
            {
                ImageData.FreePercentage = awailablePercentages - percentage;
                return percentage;
            }
            else
            {
                return awailablePercentages;
            }
        }

        private void FillingCellsData() 
        {
            int X = ImageData.NetOffset;
            int Y = ImageData.NetOffset + ImageData.NetCellSize * 4;
            int startX = X;
            int startY = Y;
            for (int i = 0; i < 20; i++)
            {
                startY = Y + (ImageData.NetCellSize * 3 * i);
                for (int j = 0; j < 40; j++)
                {
                    ImageData.Cells.Add(new MazeCellModel());
                    ImageData.Cells[ImageData.Cells.Count - 1].SequenceNumber = ImageData.Cells.Count - 1;
                    if (j == 0 && (i == 0 || i == 3 || i == 6 || i == 9 || i == 12 || i == 15 || i == 18))
                    {
                        ImageData.Cells[ImageData.Cells.Count - 1].Type = MazeCellTypeEnum.LeftHalfG;
                    }
                    if (j == 39 && (i == 1 || i == 4 || i == 7 || i == 10 || i == 13 || i == 16 || i == 19))
                    {
                        ImageData.Cells[ImageData.Cells.Count - 1].Type = MazeCellTypeEnum.RigtHalfG;
                    }
                    if (i == 19 && j < 39)
                    {
                        ImageData.Cells[ImageData.Cells.Count - 1].Type = MazeCellTypeEnum.LowerG;
                    }
                    if (j == 0)
                    {
                        switch (i)
                        {
                            case 1:
                            case 4:
                            case 7:
                            case 10:
                            case 13:
                            case 16:
                            case 19:
                                startX = X + ImageData.NetCellSize;
                                break;
                            default:
                                startX = X;
                                break;
                        }
                    }
                    else if(j == 1)
                    {
                        switch (i)
                        {
                            case 0:
                            case 3:
                            case 6:
                            case 9:
                            case 12:
                            case 15:
                            case 18:
                                startX = startX + (ImageData.NetCellSize * 2);
                                break;
                            default:
                                startX = startX + (ImageData.NetCellSize * 3);
                                break;
                        }
                    }
                    else
                    {
                        startX = startX + (ImageData.NetCellSize * 3);
                    }

                    FillingMazeCellCoordinatesAndDrawingTracks(
                        ImageData.Cells[ImageData.Cells.Count - 1].Type,
                        startX,
                        startY,
                        ImageData.Cells.Count - 1);
                }
            }
        }

        private void FillingMazeCellCoordinatesAndDrawingTracks(
            MazeCellTypeEnum type, 
            int startX, 
            int startY,
            int sequenceNumber)
        {

            switch (type)
            {
                case MazeCellTypeEnum.LowerG:
                    FillingLowerGCoordinates(startX, startY, sequenceNumber);
                    FillingLowerGDrawingTrack(startX, startY, sequenceNumber);
                    break;
                case MazeCellTypeEnum.RigtHalfG:
                    FillingRightHalfGCoordinates(startX, startY, sequenceNumber);
                    FillingRightHalfGDrawingTrack(startX, startY, sequenceNumber);
                    break;
                case MazeCellTypeEnum.LeftHalfG:
                    FillingLeftHalfGCoodinates(startX, startY, sequenceNumber);
                    FillingLeftHalfGDrawingTrack(startX, startY, sequenceNumber);
                    break;
                default:
                    FillingBaseGCoordinates(startX, startY, sequenceNumber);
                    FillingBaseGDrawingTrack(startX, startY, sequenceNumber);
                    break;
            }
        }

        private void FillingBaseGCoordinates(
            int startX, 
            int startY, 
            int sequenceNumber) 
        {

            for (int i = 0; i < ImageData.NetCellSize*3; i++)
            {
                int x = startX + i;
                int y1, y2;
                if (i >= 0 && i <= ImageData.NetCellSize)
                {
                    y1 = startY - (ImageData.NetCellSize*2 + i);
                    y2 = startY;
                } 
                else if (i > ImageData.NetCellSize && i <= ImageData.NetCellSize * 2)
                {
                    y1 = startY - (ImageData.NetCellSize * 3);
                    y2 = startY + (i - ImageData.NetCellSize);
                }
                else
                {
                    y1 = (startY - (ImageData.NetCellSize * 3)) + (i - ImageData.NetCellSize*2);
                    y2 = (startY + ImageData.NetCellSize) - (i - ImageData.NetCellSize * 2);
                }
                ImageData.Cells[sequenceNumber].CoordinateLines.Add(new CoordinateLineModel 
                { 
                    CoordinateX = x, 
                    CoordinatesY = new int[] 
                    { 
                        y1, 
                        y2 
                    }
                });
            }
        }

        private void FillingLowerGCoordinates(
            int startX,
            int startY,
            int sequenceNumber)
        {
            for (int i = 0; i < ImageData.NetCellSize * 3; i++)
            {
                int x = startX + i;
                int y1, y2;
                if (i >= 0 && i <= ImageData.NetCellSize)
                {
                    y1 = startY - (ImageData.NetCellSize * 2 + i);
                    y2 = startY + ImageData.NetCellSize;
                }
                else if (i > ImageData.NetCellSize && i <= ImageData.NetCellSize * 2)
                {
                    y1 = startY - (ImageData.NetCellSize * 3);
                    y2 = startY + ImageData.NetCellSize;
                }
                else
                {
                    y1 = (startY - (ImageData.NetCellSize * 3)) + (i - ImageData.NetCellSize * 2);
                    y2 = (startY + ImageData.NetCellSize) - (i - ImageData.NetCellSize * 2);
                }
                ImageData.Cells[sequenceNumber].CoordinateLines.Add(new CoordinateLineModel
                {
                    CoordinateX = x,
                    CoordinatesY = new int[]
                    {
                        y1,
                        y2
                    }
                });
            }
        }

        private void FillingLeftHalfGCoodinates(
            int startX,
            int startY,
            int sequenceNumber)
        {
            for (int i = 0; i < ImageData.NetCellSize * 2; i++)
            {
                int x = startX + i;
                int y1, y2;
                if (i >= 0 && i <= ImageData.NetCellSize)
                {
                    y1 = startY - (ImageData.NetCellSize * 2);
                    y2 = startY + ImageData.NetCellSize;
                }
                else
                {
                    y1 = startY - (ImageData.NetCellSize * 2);
                    y2 = (startY + ImageData.NetCellSize) - (i - ImageData.NetCellSize);
                }
                ImageData.Cells[sequenceNumber].CoordinateLines.Add(new CoordinateLineModel
                {
                    CoordinateX = x,
                    CoordinatesY = new int[]
                    {
                        y1,
                        y2
                    }
                });
            }
        }

        private void FillingRightHalfGCoordinates(
            int startX,
            int startY,
            int sequenceNumber)
        {
            for (int i = 0; i < ImageData.NetCellSize * 2; i++)
            {
                int x = startX + i;
                int y1, y2;
                if (i >= 0 && i <= ImageData.NetCellSize)
                {
                    y1 = startY - (ImageData.NetCellSize * 2 + i);
                    y2 = startY;
                }
                else
                {
                    y1 = startY - (ImageData.NetCellSize * 3);
                    y2 = startY;
                }
                ImageData.Cells[sequenceNumber].CoordinateLines.Add(new CoordinateLineModel
                {
                    CoordinateX = x,
                    CoordinatesY = new int[]
                    {
                        y1,
                        y2
                    }
                });
            }
        }

        private void FillingBaseGDrawingTrack(
            int startX,
            int startY,
            int sequenceNumber)
        {
            int x = startX + 14;
            int y = startY + ImageData.NetCellSize;
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[] 
            { 
                x, 
                y, 
                x + 14, 
                y - 14 
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[] 
            { 
                x + 14, 
                y - 14, 
                x, 
                y - 27 
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[] 
            {
                x,
                y - 27,
                x, 
                y - 9 
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[] 
            {
                x,
                y - 9,
                x - 13, 
                y - 23 
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[] 
            {
                x - 13,
                y - 23,
                x, 
                y - 36 
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[] 
            {
                x,
                y - 36,
                x + 9, 
                y - 27 
            });
        }

        private void FillingLowerGDrawingTrack(
            int startX,
            int startY,
            int sequenceNumber)
        {
            int x = startX + 5;
            int y = startY + ImageData.NetCellSize;
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[]
            {
                x,
                y,
                x,
                y - 9
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[]
            {
                x,
                y - 9,
                x + 9,
                y
            });
            FillingBaseGDrawingTrack(
                startX, 
                startY, 
                sequenceNumber);
        }

        private void FillingLeftHalfGDrawingTrack(
            int startX,
            int startY,
            int sequenceNumber)
        {
            int x = startX + 4;
            int y = startY + ImageData.NetCellSize;
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[]
            {
                x,
                y,
                x + 14,
                y - 14
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[]
            {
                x + 14,
                y - 14,
                x,
                y - 27
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[]
            {
                x,
                y - 27,
                x,
                y - 9
            });
        }

        private void FillingRightHalfGDrawingTrack(
            int startX,
            int startY,
            int sequenceNumber)
        {
            int x = startX + 14;
            int y = startY - (ImageData.NetCellSize * 2);
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[]
            {
                x,
                y,
                x,
                y + 18
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[]
            {
                x,
                y + 18,
                x - 13,
                y + 4
            });
            ImageData.Cells[sequenceNumber].DrawingTrack.Add(new int[]
            {
                x - 13,
                y + 4,
                x,
                y - 9
            });
        }
    }
}
