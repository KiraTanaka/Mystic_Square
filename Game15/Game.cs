using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Game15
{

    public class Game
    {
        public List<Tile> Tiles { get; } = new List<Tile>();
        public int Size { get; private set; }
        private List<int> CorrectSequenceOfNumbers = new List<int>();
        public List<int> History { get; } = new List<int>();
        int wins = 0;
        public int Wins
        {
            get { return wins; }
            set { wins = value; }
        }
        public void Create(int size)
        {
            this.Size = size;
            GenerateNumbers();
        }
        private void GenerateNumbers()
        {
            int maxValue = (int)Math.Pow(Size, 2);
            Random rand = new Random();           
            for (int i = 1; i < maxValue; i++)
            {
                CorrectSequenceOfNumbers.Add(i);
            }
            CorrectSequenceOfNumbers.Add(0);
            List<int> randomSequenceOfNumbers = CorrectSequenceOfNumbers.OrderBy(x => rand.Next()).ToList();
            TilesFilling(randomSequenceOfNumbers);
        }
        private void TilesFilling(List<int> randomSequenceOfNumbers)
        {
            int indexRow = 0;
            int indexColumn = 0;
            foreach (var number in randomSequenceOfNumbers)
            {
                Tiles.Add(new Tile(indexRow, indexColumn, number));
                indexColumn = (indexColumn == Size - 1) ? 0 : indexColumn + 1;
                indexRow = (indexColumn == 0) ? indexRow + 1 : indexRow;
            }
        }
        public int this[int x, int y]
        {
            get
            {
                return Tiles.First(tile => tile.Point.X == x
                                            & tile.Point.Y == y)
                                            .Value;
            }
        }
        public Point GetLocation(int value)
        {
            return Tiles.Where(tile => tile.Value == value).First().Point;
        }
        public bool Shift(int value)
        {
            Tile movingTile = Tiles.First(tile => tile.Value == value);
            Tile tileWithZero = Tiles.Where(tile => tile.IsNeighbor(movingTile.Point) & tile.Value == 0).FirstOrDefault();
            if (tileWithZero == null)
                return false;
            SwapTiles(movingTile, tileWithZero);
            History.Add(value);
            return true;
        }
        void SwapTiles(Tile firstTile, Tile secondTile)
        {
            Point point = new Point(firstTile.Point.X, firstTile.Point.Y);
            firstTile.SetLocation(secondTile.Point.X, secondTile.Point.Y);
            secondTile.SetLocation(point.X, point.Y);
        }
        public bool WhetherGameIsOver()
        {
            for (int i = 0; i < CorrectSequenceOfNumbers.Count; i++)
            {
                int value = CorrectSequenceOfNumbers[i];
                Tile tile = Tiles.FirstOrDefault(el => el.Value == value);
                int position = (int)(tile.Point.X * Size + tile.Point.Y);
                if (position != i)
                    return false;
            }
            return true;
        }
        public void DeleteData()
        {
            CorrectSequenceOfNumbers?.Clear();
            Tiles?.Clear();
            History?.Clear();
        }
        public bool RollBackStep()
        {
            if (History.Count == 0)
                return false;
            Tile tile = Tiles.FirstOrDefault(x => x.Value == History.Last());
            Tile tileWithZero = Tiles.FirstOrDefault(x => x.Value == 0);
            SwapTiles(tile, tileWithZero);
            History.RemoveAt(History.Count - 1);
            return true;
        }
    }
}
