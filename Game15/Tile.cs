using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game15
{
    public class Tile
    {
        public Point Point = new Point();
        public int Value { get; set; }
        public object View { get; set; }
        public Tile() { }
        public Tile(int x, int y, int value)
        {
            Point.X = x;
            Point.Y = y;
            Value = value;
        }
        public void SetLocation(double x, double y)
        {
            Point.X = x;
            Point.Y = y;
        }
        public bool IsNeighbor(Point other)
        {
            var deltaX = Point.X - other.X;
            var deltaY = Point.Y - other.Y;
            var distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            return (distance <= 1) ? true : false;
        }
    }
}
