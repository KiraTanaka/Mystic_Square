using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game15
{
    public class ControlPanel
    {
        MainWindow Window;
        public List<Control> Controls { get; } = new List<Control>();
        public ControlPanel(MainWindow window)
        {
            this.Window = window;
        }
        public List<Control> Create()
        {
            Button newgame = new Button()
            {
                Content = "New game",
                Height = 30,
                Width = 100,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 160, 0, 0)
            };
            newgame.Click += newGame_Click;

            Button rollbackStep = new Button()
            {
                Content = "Rollback step",
                Height = 30,
                Width = 100,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 100, 0, 0)
            };
            rollbackStep.Click += rollbackStep_Click;

            Label wins = new Label()
            {
                Content = "Wins:  0",
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Name = "Wins",
                Margin = new Thickness(0, 40, 0, 0)
            };

            int sizeOfElement = 40;
            ListBox kindsOfGames = new ListBox()
            {
                Margin = new Thickness(0, 180, 0, 0),
                BorderBrush = Brushes.Gainsboro,
                Height = 5
            };
            kindsOfGames.SelectionChanged += kindsOfGames_SelectionChanged;
            for (int i = Window.MinSizeOfGame; i <= Window.MaxSizeOfGame; i++)
            {
                ListBoxItem item = new ListBoxItem()
                {
                    Content = String.Format(i + " X " + i),
                    Height = sizeOfElement,
                    HorizontalContentAlignment = HorizontalAlignment.Center
                };
                kindsOfGames.Height += sizeOfElement;
                kindsOfGames.Items.Add(item);
            }           
            Controls.Add(newgame);
            Controls.Add(rollbackStep);
            Controls.Add(wins);
            Controls.Add(kindsOfGames);
            return Controls;
        }
        private void newGame_Click(object sender, RoutedEventArgs e)
        {
            Window.NewGame();
        }
        private void rollbackStep_Click(object sender, RoutedEventArgs e)
        {
            Window.RollBackStep();
        }
        private void kindsOfGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Window.NewGame(Int32.Parse(((ListBoxItem)e.AddedItems[0]).Content.ToString()[0].ToString()));           
        }
    }
}
