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
        private Game Game;
        ControlPanel ControlPanel;
        public int MaxSizeOfGame { get; } = 5;
        public int MinSizeOfGame { get; } = 3;
        public MainWindow()
        {
            InitializeComponent();
            Layout();           
            ControlPanel = new ControlPanel(this);
            CreateControlPanel();
        }
        void Layout()
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            Grid.SetColumn(grid1, 0);
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.SetColumn(grid2, 1);
        }
        private void CreateGame(int size)
        {
            Game = new Game(size);
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
                    tile.View = new ViewTile(tile_Click,(Style)this.Resources["First"],tile);
                    grid1.Children.Add(tile.GetControl());
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
                    tile.SetLocationView();
                }
            }
        }
        private void WhetherGameIsOver()
        {
            if (Game.WhetherGameIsOver())
            {
                Label label = (Label)ControlPanel.Controls.First(x => x.Name == "Wins");
                int countWins = Int32.Parse(label.Content.ToString().Last().ToString());         
                label.Content = "Wins:  " + ++countWins;
                MessageBox.Show("Congratulations!)");
            }
        }
        public void NewGame(int size=0)
        {
            int sizeOfGame;
            if (Game != null)
            {
                sizeOfGame = (size == 0) ? Game.Size : size;
                grid1.Children.Clear();
                grid1.RowDefinitions.Clear();
                grid1.ColumnDefinitions.Clear();
            }
            else
                sizeOfGame = (size == 0) ? MinSizeOfGame : size;
            CreateGame(sizeOfGame);
        }
        public void RollBackStep()
        {
            if(Game.RollBackStep())
                MoveTiles();
        }
    }
}
