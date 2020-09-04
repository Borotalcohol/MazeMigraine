using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMigraine
{
    [Serializable]
    public class MazeLevel
    {
        string name { get; set; }
        Cell[,] grid { get; set; }
        int rows { get; set; }
        int cols { get; set; }
        int dim { get; set; }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Cell[,] Grid
        {
            get { return grid; }
            set { grid = value; }
        }

        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        public int Cols
        {
            get { return cols; }
            set { cols = value; }
        }

        public int Dim
        {
            get { return dim; }
            set { dim = value; }
        }

        public MazeLevel() {}

        public MazeLevel(Cell[,] grid, int cols, int rows, int dim)
        {
            grid = new Cell[rows, cols];

            for(int i = 0; i < rows; i++) {
                for(int j = 0; j < cols; j++) {
                    grid[i, j] = new Cell(i, j, dim);
                }
            }

            Rows = rows;
            Cols = cols;
            Dim = dim;
        }

        public MazeLevel(int cols, int rows, int dim)
        {
            grid = new Cell[rows, cols];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    grid[i, j] = new Cell(i, j, dim);
                }
            }

            Rows = rows;
            Cols = cols;
            Dim = dim;
        }

        public bool CanMove(int x1, int y1, int x2, int y2)
        {
            if(x1 > x2) {
                // Move left
                return (grid[x1, y1].walls[3] && grid[x2, y2].walls[1]);
            }
            else if(x1 < x2) {
                // Move right
                return (grid[x1, y1].walls[1] && grid[x2, y2].walls[3]);
            }
            else if(y1 > y2) {
                // Move up
                return (grid[x1, y1].walls[0] && grid[x2, y2].walls[2]);
            }
            else if(y1 < y2) {
                // Move down
                return (grid[x1, y1].walls[2] && grid[x2, y2].walls[0]);
            }

            return false;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
