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
    /// Logica di interazione per WIN_PlayAgain.xaml
    /// </summary>
    public partial class WIN_PlayAgain : Window
    {
        WIN_Game game_window;
        MainWindow mw;

        public WIN_PlayAgain(WIN_Game gw)
        {
            InitializeComponent();

            mw = (MainWindow)Application.Current.MainWindow;

            game_window = gw;

            this.Title = "MAZE MIGRAINE";
        }

        private void Bt_yes_Click(object sender, RoutedEventArgs e)
        {
            mw.mp.Stop();
            String projdir = System.IO.Directory.GetCurrentDirectory();
            mw.mp.Open(new Uri(projdir + "/mp3s/SuspenseLoop.wav"));
            mw.mp.Play();

            WIN_Mode wm = new WIN_Mode();
            wm.Show();
            this.Close();
            game_window.Close();
        }

        private void Bt_no_Click(object sender, RoutedEventArgs e)
        {
            mw.mp.Stop();
            mw.canPlayAgain = true;

            mw.Show();
            this.Close();
            game_window.Close();
        }
    }
}
