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
    [Serializable]
    public class Cell
    {
        public bool[] walls;
        public bool hasBeenVisited;
        public int x, y;
        public int posx, posy;
        public int dim;

        public Rectangle topWall;
        public Rectangle rightWall;
        public Rectangle bottomWall;
        public Rectangle leftWall;

        public Cell(int _x, int _y, int d)
        {
            x = _x;
            y = _y;
            dim = d;

            hasBeenVisited = false;
            walls = new bool[4] { false, false, false, false };

            posx = x * dim;
            posy = y * dim;

            topWall = new Rectangle()     { Width = dim, Height = 1};
            rightWall = new Rectangle()   { Width = 1, Height = dim };
            bottomWall = new Rectangle()  { Width = dim, Height = 1 };
            leftWall = new Rectangle()    { Width = 1, Height = dim };

            topWall.Fill = Brushes.White;
            rightWall.Fill = Brushes.White;
            bottomWall.Fill = Brushes.White;
            leftWall.Fill = Brushes.White;

            Canvas.SetLeft(topWall, posx);
            Canvas.SetTop(topWall, posy);
            Canvas.SetLeft(rightWall, (posx + dim)-rightWall.Width);
            Canvas.SetTop(rightWall, posy);
            Canvas.SetLeft(bottomWall, posx);
            Canvas.SetTop(bottomWall, posy + dim);
            Canvas.SetLeft(leftWall, posx);
            Canvas.SetTop(leftWall, posy);
        }

        public void RemoveWall(int index)
        {
            walls[index] = true;
        }
    }
}
