using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMigraine
{
    [Serializable]
    class SerializableMazeLevel
    {
        int cols, rows, dim;
        string name;

        [Serializable]
        struct wallSet
        {
            public bool[] walls;
        }

        wallSet[,] mazeWalls;

        public SerializableMazeLevel(int c, int r, int d, string n)
        {
            cols = c;
            rows = r;
            dim = d;
            name = n;

            mazeWalls = new wallSet[rows, cols];

            for(int i = 0; i < rows; i++) {
                for(int j = 0; j < cols; j++) {
                    mazeWalls[i, j].walls = new bool[4] { false, false, false, false };
                }
            }
        }

        public static SerializableMazeLevel GetSerializableMaze(MazeLevel ml)
        {
            SerializableMazeLevel sml = new SerializableMazeLevel(ml.Cols, ml.Rows, ml.Dim, ml.Name);

            for(int i = 0; i < ml.Rows; i++) {
                for(int j = 0; j < ml.Cols; j++) {

                    for (int k = 0; k < 4; k++)
                        sml.mazeWalls[i, j].walls[k] = ml.Grid[i, j].walls[k];
                }
            }

            return sml;
        }

        public static MazeLevel GetMazeLevel(SerializableMazeLevel sml)
        {
            MazeLevel ml = new MazeLevel(sml.cols, sml.rows, sml.dim);
            ml.Name = sml.name;

            for(int i = 0; i < sml.rows; i++) {
                for(int j = 0; j < sml.cols; j++) {

                    for (int k = 0; k < 4; k++)
                        ml.Grid[i, j].walls[k] = sml.mazeWalls[i, j].walls[k];
                }
            }

            return ml;
        }
    }
}
