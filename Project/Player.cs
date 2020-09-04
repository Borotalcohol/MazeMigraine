using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeMigraine
{
    class Player
    {
        public Ellipse ellipse;
        public int unitX, unitY;
        public int posX, posY;
        public int dim;
        public int cellDim;

        public Player(int x, int y, int cd)
        {
            unitX = x;
            unitY = y;

            dim = cd / 2;

            cellDim = cd;

            posX = (x * cellDim) + dim / 2;
            posY = (y * cellDim) + dim / 2;

            ellipse = new Ellipse();

            ellipse.Width = dim;
            ellipse.Height = dim;
            ellipse.Fill = Brushes.Red;

            Canvas.SetLeft(ellipse, posX);
            Canvas.SetTop(ellipse, posY);
        }

        public void Move(int x, int y)
        {
            posX = (x * cellDim) + dim / 2;
            posY = (y * cellDim) + dim / 2;

            unitX = x;
            unitY = y;

            Canvas.SetLeft(ellipse, posX);
            Canvas.SetTop(ellipse, posY);
        }

        public bool CheckWin(int targetX, int targetY)
        {
            return (unitX == targetX && unitY == targetY);
        }
    }
}
