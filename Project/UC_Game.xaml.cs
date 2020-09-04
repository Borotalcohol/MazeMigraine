using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeMigraine
{
    /// <summary>
    /// Logica di interazione per UC_Game.xaml
    /// </summary>
    public partial class UC_Game : UserControl
    {
        MazeLevel maze;
        Player player;
        MainWindow mw;

        public UC_Game(MazeLevel m)
        {
            InitializeComponent();

            maze = m;
            mw = (MainWindow)Application.Current.MainWindow;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < maze.Rows; i++) {
                for(int j = 0; j < maze.Cols; j++) {
                    if (!maze.Grid[i, j].walls[0])
                        canvas.Children.Add(maze.Grid[i, j].topWall);

                    if (!maze.Grid[i, j].walls[1])
                        canvas.Children.Add(maze.Grid[i, j].rightWall);

                    if (!maze.Grid[i, j].walls[2])
                        canvas.Children.Add(maze.Grid[i, j].bottomWall);

                    if (!maze.Grid[i, j].walls[3])
                        canvas.Children.Add(maze.Grid[i, j].leftWall);
                }
            }

            Ellipse targetPoint = new Ellipse();
            targetPoint.Width = maze.Dim / 2;
            targetPoint.Height = maze.Dim / 2;
            targetPoint.Fill = Brushes.Gold;

            Canvas.SetLeft(targetPoint, maze.Grid[maze.Rows - 1, maze.Cols - 1].posx + targetPoint.Width / 2);
            Canvas.SetTop(targetPoint, maze.Grid[maze.Rows - 1, maze.Cols - 1].posy + targetPoint.Height / 2);

            canvas.Children.Add(targetPoint);

            player = new Player(0, 0, maze.Dim);
            canvas.Children.Add(player.ellipse);

            mw.KeyDown += Mw_KeyDown;
        }

        private void Mw_KeyDown(object sender, KeyEventArgs e)
        {
            bool hasWin = false;

            switch (e.Key)
            {
                case Key.Up:
                    if (maze.CanMove(player.unitX, player.unitY, player.unitX, player.unitY - 1))
                    {
                        player.Move(player.unitX, player.unitY - 1);
                        hasWin = player.CheckWin(maze.Grid[maze.Rows - 1, maze.Cols - 1].x, maze.Grid[maze.Rows - 1, maze.Cols - 1].y);
                    }

                    MessageBox.Show("Pressed UP");
                    break;

                case Key.Right:
                    if (maze.CanMove(player.unitX, player.unitY, player.unitX + 1, player.unitY))
                    {
                        player.Move(player.unitX + 1, player.unitY);
                        hasWin = player.CheckWin(maze.Grid[maze.Rows - 1, maze.Cols - 1].x, maze.Grid[maze.Rows - 1, maze.Cols - 1].y);
                    }

                    MessageBox.Show("Pressed RIGHT");
                    break;

                case Key.Down:
                    if (maze.CanMove(player.unitX, player.unitY, player.unitX, player.unitY + 1))
                    {
                        player.Move(player.unitX, player.unitY + 1);
                        hasWin = player.CheckWin(maze.Grid[maze.Rows - 1, maze.Cols - 1].x, maze.Grid[maze.Rows - 1, maze.Cols - 1].y);
                    }

                    MessageBox.Show("Pressed DOWN");
                    break;

                case Key.Left:
                    if (maze.CanMove(player.unitX, player.unitY, player.unitX - 1, player.unitY))
                    {
                        player.Move(player.unitX - 1, player.unitY);
                        hasWin = player.CheckWin(maze.Grid[maze.Rows - 1, maze.Cols - 1].x, maze.Grid[maze.Rows - 1, maze.Cols - 1].y);
                    }

                    MessageBox.Show("Pressed LEFT");
                    break;
            }

            if(hasWin) {

            }
        }
    }
}
