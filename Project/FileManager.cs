using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MazeMigraine
{
    static class FileManager
    {
        public static void AddPlayerToXml(User p)
        {
            List<User> players = new List<User>();
            players.Add(p);

            XmlSerializer xs = new XmlSerializer(players.GetType());
            Stream st = new FileStream("games.xml", FileMode.Create, FileAccess.Write);
            xs.Serialize(st, players);
            st.Close();
        }

        public static List<User> GetPlayersFromXml()
        {
            List<User> players = new List<User>();

            if (File.Exists("games.xml"))
            {
                XmlSerializer xs = new XmlSerializer(players.GetType());
                Stream st = new FileStream("games.xml", FileMode.Open, FileAccess.Read);
                players = (List<User>)xs.Deserialize(st);
                st.Close();
            }

            return players;
        }

        public static void OverridePlayersXml(List<User> user)
        {
            XmlSerializer xs = new XmlSerializer(user.GetType());
            Stream st = new FileStream("games.xml", FileMode.Create, FileAccess.Write);
            xs.Serialize(st, user);
            st.Close();
        }

        public static void AddMazeToFile(MazeLevel maze, string mode)
        {
            string path = "";
            string levelname = "";
            List<SerializableMazeLevel> mazes = GetMazesByMode(mode);
            int count = mazes.Count;

            switch(mode)
            {
                case "Easy":
                    levelname = "EZ_" + (count + 1);
                    path = "Mazes/EZ/EZ_";
                    break;

                case "Normal":
                    levelname = "NOR_" + (count + 1);
                    path = "Mazes/NOR/NOR_";
                    break;

                case "Difficult":
                    levelname = "DIF_" + (count + 1);
                    path = "Mazes/DIF/DIF_";
                    break;
            }

            BinaryFormatter bf = new BinaryFormatter();
            maze.Name = levelname;
            mazes.Add(SerializableMazeLevel.GetSerializableMaze(maze));

            int i = 1;
            Stream st;

            foreach (SerializableMazeLevel sml in mazes)
            {
                string fullpath = path + i.ToString();

                st = new FileStream(fullpath, FileMode.Create, FileAccess.Write);
                bf.Serialize(st, sml);
                st.Close();

                i++;
            }
        }

        public static List<SerializableMazeLevel> GetMazesByMode(string mode)
        {
            List<SerializableMazeLevel> mazes = new List<SerializableMazeLevel>();
            BinaryFormatter bf = new BinaryFormatter();
            Stream st;

            string fullpath = "";
            string path = "";
            int i = 1;

            switch(mode)
            {
                case "Easy":
                    path = "Mazes/EZ/EZ_";
                    break;

                case "Normal":
                    path = "Mazes/NOR/NOR_";
                    break;

                case "Difficult":
                    path = "Mazes/DIF/DIF_";
                    break;
            }

            do
            {
                fullpath = path + i.ToString();

                if (File.Exists(fullpath))
                {
                    st = new FileStream(fullpath, FileMode.Open, FileAccess.Read);
                    SerializableMazeLevel sml = (SerializableMazeLevel)bf.Deserialize(st);
                    mazes.Add(sml);
                    st.Close();
                }

                i++;

            } while (File.Exists(fullpath));

            return mazes;
        }
    }
}
