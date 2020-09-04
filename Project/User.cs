using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMigraine
{
    public class User
    {
        string nickname { get; set; }
        List<Game> playedGames { get; set; }

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }

        public List<Game> PlayedGames
        {
            get { return playedGames; }
            set { playedGames = value; }
        }

        public User() { }

        public User(string n)
        {
            nickname = n;

            playedGames = new List<Game>();
        }

        public User(string n, string t, string ln)
        {
            nickname = n;
            
            Game g = new Game(ln, t);
            playedGames = new List<Game>();
        }

        public void AddGame(Game g)
        {
            playedGames.Add(g);
        }

        public override string ToString()
        {
            string result = nickname;

            foreach(Game game in playedGames)
                result += string.Format("\n* {0}  :  {1}", game.LevelName, game.Time);

            result += "\n\n";

            return result;    
        }
    }
}
