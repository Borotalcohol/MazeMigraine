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
using System.Windows.Threading;
using MazeMigraine.Properties;

namespace MazeMigraine
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    /// 


    /*
        TODO:
        
        - Movimento fluido
        - Aggiungere dinamicità ai livelli (Perk, Nemici ecc...)
        - Ottimizzare spazio occupato dai file contenenti i livelli
        - Implementare la modalità "emicrania"

        BUG FIXES:
        
        - Da gestire il multiplo click sul bottone classifica
        
    */

    public partial class MainWindow : Window
    {
        public MediaPlayer mp;
        public bool canPlayAgain = true;

        public MainWindow()
        {
            InitializeComponent();
            
            mp = new MediaPlayer();
            mp.MediaEnded += Mp_MediaEnded;
            
            this.Title = "MAZE MIGRAINE MENU";
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            gr_wrapper.Children.Clear();
            gr_wrapper.Children.Add(new UC_MenuPage());

            if (canPlayAgain) {
                String projdir = System.IO.Directory.GetCurrentDirectory();
                mp.Open(new Uri(projdir+"/mp3s/SuspenseLoop.wav"));
                mp.Play();
                canPlayAgain = false;
            }
        }

        private void Mp_MediaEnded(object sender, EventArgs e)
        {
            mp.Position = TimeSpan.Zero;
            mp.Play();
        }
    }
}
