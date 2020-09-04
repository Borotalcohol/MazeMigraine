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

namespace MazeMigraine
{
    /// <summary>
    /// Interaction logic for WIN_GameOver.xaml
    /// </summary>
    public partial class WIN_GameOver : Window
    {
        string playedMode;
        string time;
        bool saveToFile;
        MazeLevel maze;
        WIN_Game game_window;

        public WIN_GameOver(MazeLevel m, string mode, int t, bool stf, WIN_Game gw)
        {
            InitializeComponent();

            this.Title = "MAZE MIGRAINE";

            game_window = gw;

            playedMode = mode;

            saveToFile = stf;

            maze = m;
            
            int minutes = 0;

            while(t >= 60) {
                t -= 60;
                minutes++;
            }

            string str_sec = "";

            if (t < 10) str_sec = string.Format("0{0}", t);
            else str_sec = t.ToString();

            time = string.Format("{0}:{1}", minutes, str_sec);
            lb_time.Content = time;
        }

        private void Bt_addscore_Click(object sender, RoutedEventArgs e)
        {
            string name = tb_name.Text;

            if(name.Equals("")) {
                MessageBox.Show("Fill the name field");
            } else {

                if (saveToFile)
                    FileManager.AddMazeToFile(maze, playedMode);

                User user = new User(name, time, maze.Name);

                Game game = new Game(maze.Name, time);

                List<User> users = FileManager.GetPlayersFromXml();

                bool found = false;
                foreach(User u in users)
                {
                    if(user.Nickname.Equals(u.Nickname))
                    {
                        found = true;
                        u.AddGame(game);
                    }
                }

                if(!found) {
                    user.AddGame(game);
                    users.Add(user);
                    FileManager.AddPlayerToXml(user);
                }

                FileManager.OverridePlayersXml(users);

                WIN_PlayAgain wpa = new WIN_PlayAgain(game_window);
                wpa.Show();
                this.Close();
            }
        }
    }
}
