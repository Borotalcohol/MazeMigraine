using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMigraine
{
    public class Game
    {
        string levelName { get; set; }
        string time { get; set; }

        public string LevelName
        {
            get { return levelName; }
            set { levelName = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public Game() { }

        public Game(string l, string t)
        {
            levelName = l;

            time = t;
        }
    }
}
