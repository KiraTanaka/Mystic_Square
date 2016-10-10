using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using System.Windows.Markup;

namespace Game15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        private Game Game = new Game();
        ControlPanel ControlPanel;
        public MainWindow()
        {
            InitializeComponent();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star)});
            Grid.SetColumn(grid1,0);
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.SetColumn(grid2, 1);
            ControlPanel = new ControlPanel(this);
            CreateControlPanel();
        }
        public void CreateGame(int size)
        {
            Game.Create(size);
            for (int i = 0; i < size; i++)
            {
                grid1.RowDefinitions.Add(new RowDefinition());
                grid1.ColumnDefinitions.Add(new ColumnDefinition());
            }
            TilesPlacement();           
        }
        private void TilesPlacement()
        {
            foreach (var tile in Game.Tiles)
            {
                if (tile.Value != 0)
                {
                    Button button = new Button() { Content = tile.Value };

                    button.Style = (Style)this.Resources["First"];
                    button.Click += tile_Click;
                    Grid.SetRow(button, (int)tile.Point.X);
                    Grid.SetColumn(button, (int)tile.Point.Y);
                    tile.View = button;
                    grid1.Children.Add(button);
                }
            }
        }
        void CreateControlPanel()
        {
            List<Control> controls = ControlPanel.Create();
            foreach (var control in controls)
            {
                grid2.Children.Add(control);
            }
        }
        private void tile_Click(object sender, RoutedEventArgs e)
        {
            if (Game.Shift(Int32.Parse(((Button)sender).Content.ToString())))
            {
                MoveTiles();
                WhetherGameIsOver();
            }
        }
        void MoveTiles()
        {
            foreach (var tile in Game.Tiles)
            {
                if (tile.Value != 0)
                {
                    Grid.SetRow((Button)tile.View, (int)tile.Point.X);
                    Grid.SetColumn((Button)tile.View, (int)tile.Point.Y);
                }
            }
        }
        private void WhetherGameIsOver()
        {
            if (Game.WhetherGameIsOver())
            {
                Label label = (Label)ControlPanel.Controls.First(x => x.Name == "Wins");               
                label.Content = "Wins:  " + ++Game.Wins;
                MessageBox.Show("Congratulations!)");
            }
        }
        public void NewGame(int size=0)
        {
            int sizeOfGame = (size==0)?ControlPanel.minSizeOfGame:size;
            if (Game.Tiles.Count != 0)
            {
                sizeOfGame = (size == 0) ? Game.Size : size;
                grid1.Children.Clear();
                grid1.RowDefinitions.Clear();
                grid1.ColumnDefinitions.Clear();
                Game.DeleteData();
            }
            CreateGame(sizeOfGame);
        }
        public void RollBackStep()
        {
            if(Game.RollBackStep())
                MoveTiles();
        }
        public int GetWins()
        {
            return Game.Wins;
        }
    }
}
