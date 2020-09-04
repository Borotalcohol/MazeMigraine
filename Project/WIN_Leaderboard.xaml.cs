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
    /// Logica di interazione per WIN_Leaderboard.xaml
    /// </summary>
    public partial class WIN_Leaderboard : Window
    {
        public WIN_Leaderboard()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            // Load the leaderboard
            List<User> players = FileManager.GetPlayersFromXml();
            lv_scores.ItemsSource = players;
        }

        private void Bt_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
