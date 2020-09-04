using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logica di interazione per WIN_LevelSelection.xaml
    /// </summary>
    public partial class WIN_LevelSelection : Window
    {
        public static Random rand = new Random();
        string choosenMode;
        MazeLevel ml;

        public WIN_LevelSelection(string mode)
        {
            InitializeComponent();

            this.Title = "LEVEL SELECTION";

            choosenMode = mode;
            ml = new MazeLevel();

            switch(choosenMode)
            {
                case "Easy":
                    ml.Rows = 10;
                    ml.Cols = 10;
                    ml.Dim = 60;
                    break;

                case "Normal":
                    ml.Rows = 20;
                    ml.Cols = 20;
                    ml.Dim = 30;
                    break;

                case "Difficult":
                    ml.Rows = 30;
                    ml.Cols = 30;
                    ml.Dim = 20;
                    break;

                case "Migraine":
                    ml.Rows = 60;
                    ml.Cols = 60;
                    ml.Dim = 10;
                    break;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            // Load existing levels

            List<MazeLevel> mazes = new List<MazeLevel>();
            List<SerializableMazeLevel> smls = FileManager.GetMazesByMode(choosenMode);

            foreach(SerializableMazeLevel sml in smls)
            {
                MazeLevel ml = SerializableMazeLevel.GetMazeLevel(sml);
                mazes.Add(ml);
            }

            cb_existing_levels.ItemsSource = mazes;
        }

        private void Bt_select_Click(object sender, RoutedEventArgs e)
        {
            // Play selected level

            if(cb_existing_levels.SelectedItem == null) {
                MessageBox.Show("You have to select a level!");
            } else {
                MainWindow mw = ((MainWindow)Application.Current.MainWindow);
                mw.Hide();

                WIN_Game wg = new WIN_Game((MazeLevel)cb_existing_levels.SelectedItem, choosenMode, false);
                wg.Show();
                this.Close();
            }
        }

        private void Bt_generate_Click(object sender, RoutedEventArgs e)
        {
            // Generate and load new maze
            ml.Grid = GenerateMaze(ml.Cols, ml.Rows, ml.Dim);

            MainWindow mw = ((MainWindow)Application.Current.MainWindow);
            mw.Hide();

            WIN_Game wg = new WIN_Game(ml, choosenMode, true);
            wg.Show();
            this.Close();
        }

        public Cell[,] GenerateMaze(int cols, int rows, int dim)
        {
            Stack<Cell> stack = new Stack<Cell>();
            Cell[,] grid = new Cell[rows, cols];
            Cell current;

            for(int i = 0; i < rows; i++) {
                for(int j = 0; j < cols; j++) {
                    grid[i, j] = new Cell(i, j, dim);
                }
            }

            // 1. Make the initial cell the current
            //    cell and mark it as visited
            int randX = rand.Next(cols);
            int randY = rand.Next(rows);
            current = grid[randX, randY];
            grid[randX, randY].hasBeenVisited = true;


            // 2. While there are unvisited cells:
            while(!CheckIfDone(grid)) {
                
                // 1. If the current cell has any neighbour
                //    which have not been visited
                if(CheckNeighbours(grid, current)) {
                    List<Cell> neighbours = new List<Cell>();

                    if (current.y - 1 >= 0)
                        if (!grid[current.x, current.y-1].hasBeenVisited)
                            neighbours.Add(grid[current.x, current.y-1]);

                    if (current.y + 1 < rows)
                        if (!grid[current.x, current.y + 1].hasBeenVisited)
                            neighbours.Add(grid[current.x, current.y + 1]);

                    if (current.x - 1 >= 0)
                        if (!grid[current.x - 1, current.y].hasBeenVisited)
                            neighbours.Add(grid[current.x - 1, current.y]);

                    if (current.x + 1 < cols)
                        if (!grid[current.x + 1, current.y].hasBeenVisited)
                            neighbours.Add(grid[current.x + 1, current.y]);

                    // 1. Choose randomly one of the unvisited neighbours
                    int pick = rand.Next(neighbours.Count);

                    // 2. Push current cell to the stack
                    stack.Push(current);

                    // 3. Remove the walls between the current
                    //    wall and the choosen cell
                    if(current.y > neighbours[pick].y)
                    {
                        grid[current.x, current.y].RemoveWall(0);
                        grid[current.x, neighbours[pick].y].RemoveWall(2);
                    }
                    else if (current.y < neighbours[pick].y)
                    {
                        grid[current.x, current.y].RemoveWall(2);
                        grid[current.x, neighbours[pick].y].RemoveWall(0);
                    }
                    else if (current.x > neighbours[pick].x)
                    {
                        grid[current.x, current.y].RemoveWall(3);
                        grid[neighbours[pick].x, current.y].RemoveWall(1);
                    }
                    else if (current.x < neighbours[pick].x)
                    {
                        grid[current.x, current.y].RemoveWall(1);
                        grid[neighbours[pick].x, current.y].RemoveWall(3);
                    }


                    // 4. Make the choosen cell the current cell
                    current = neighbours[pick];
                    grid[current.x, current.y].hasBeenVisited = true;
                }
                // 2. Else if stack is not empty
                else if(stack.Count > 0)
                {
                    current = stack.Pop();
                }
            }

            return grid;
        }

        public bool CheckIfDone(Cell[,] grid) {
            int numOfCells = ml.Rows * ml.Cols;
            int visitedCells = 0;

            for(int i = 0; i < ml.Rows; i++) {
                for(int j = 0; j < ml.Cols; j++) {
                    if (grid[i, j].hasBeenVisited)
                        visitedCells++;
                }
            }

            return (visitedCells == numOfCells);
        }

        public bool CheckNeighbours(Cell[,] grid, Cell curr)
        {
            bool found = false;

            if(curr.y + 1 < ml.Rows) {
                found = !grid[curr.x,curr.y + 1].hasBeenVisited;
                if (found) return found;
            }

            if (curr.y - 1 >= 0)
            {
                found = !grid[curr.x, curr.y - 1].hasBeenVisited;
                if (found) return found;
            }

            if (curr.x - 1 >= 0)
            {
                found = !grid[curr.x - 1, curr.y].hasBeenVisited;
                if (found) return found;
            }

            if (curr.x + 1 < ml.Cols)
            {
                found = !grid[curr.x + 1, curr.y].hasBeenVisited;
                if (found) return found;
            }

            return found;
        }

        private void Bt_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
