using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Game15
{
    public class ViewTile
    {
        Tile tile;
        public Control Element { get;}

        public ViewTile(Tile tile, RoutedEventHandler handler = null, Style style = null)
        {
            this.tile = tile;
            Element = new Button() { Content = tile.Value };
            Element.Style = style;
            ((Button)Element).Click += handler;
            SetLocation();
        }
        public void SetLocation()
        {
            Grid.SetRow((Button)Element, (int)tile.Point.X);
            Grid.SetColumn((Button)Element, (int)tile.Point.Y);
        }
    }
}
