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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MazeMigraine
{
    /// <summary>
    /// Interaction logic for WIN_Game.xaml
    /// </summary>
    public partial class WIN_Game : Window
    {
        MazeLevel maze;
        Player player;
        string mode;
        int time;
        bool saveToFile;
        DispatcherTimer timer;

        MainWindow mw;

        bool alreadyActivated = false;

        public WIN_Game(MazeLevel m, string mod, bool stf)
        {
            InitializeComponent();

            this.Title = "MAZE MIGRAINE";

            lb_time.Content = "0:00";

            saveToFile = stf;

            maze = m;
            mode = mod;

            mw = (MainWindow)Application.Current.MainWindow;
            String projdir = System.IO.Directory.GetCurrentDirectory();
            mw.mp.Open(new Uri(projdir + "/mp3s/KidStalker.wav"));
            mw.mp.Play();

            time = 0;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time++;
            UpdateTimeLabel(time);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (!alreadyActivated)
            {
                for (int i = 0; i < maze.Rows; i++)
                {
                    for (int j = 0; j < maze.Cols; j++)
                    {
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

                alreadyActivated = true;
            }
        }

        public void UpdateTimeLabel(int seconds)
        {
            int copy_seconds = seconds;
            int minutes = 0;

            while (copy_seconds >= 60)
            {
                minutes++;
                copy_seconds -= 60;
            }

            string str_sec = "";
            if (copy_seconds < 10) str_sec = "0" + copy_seconds;
            else str_sec = copy_seconds.ToString();

            string time = minutes + ":" + str_sec;
            lb_time.Content = time;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
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
                    break;

                case Key.Right:
                    if (maze.CanMove(player.unitX, player.unitY, player.unitX + 1, player.unitY))
                    {
                        player.Move(player.unitX + 1, player.unitY);
                        hasWin = player.CheckWin(maze.Grid[maze.Rows - 1, maze.Cols - 1].x, maze.Grid[maze.Rows - 1, maze.Cols - 1].y);
                    }
                    break;

                case Key.Down:
                    if (maze.CanMove(player.unitX, player.unitY, player.unitX, player.unitY + 1))
                    {
                        player.Move(player.unitX, player.unitY + 1);
                        hasWin = player.CheckWin(maze.Grid[maze.Rows - 1, maze.Cols - 1].x, maze.Grid[maze.Rows - 1, maze.Cols - 1].y);
                    }
                    break;

                case Key.Left:
                    if (maze.CanMove(player.unitX, player.unitY, player.unitX - 1, player.unitY))
                    {
                        player.Move(player.unitX - 1, player.unitY);
                        hasWin = player.CheckWin(maze.Grid[maze.Rows - 1, maze.Cols - 1].x, maze.Grid[maze.Rows - 1, maze.Cols - 1].y);
                    }
                    break;
            }

            if (hasWin)
            {
                timer.Stop();
                WIN_GameOver wgo = new WIN_GameOver(maze, mode, time, saveToFile, this);
                wgo.Show();

                mw.mp.Stop();

                String projdir = System.IO.Directory.GetCurrentDirectory();
                mw.mp.Open(new Uri(projdir + "/mp3s/PowerBotsLoop.wav"));

                mw.mp.Play();

                this.IsEnabled = false;
            }
        }
    }
}
