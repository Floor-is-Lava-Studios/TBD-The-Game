using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;

namespace FloorIsLava
{
    // this will read in the file with all the unlocked levels on it
    class SaveInfo
    {
        //attributes
        private Dictionary<string, bool> levels;
        private string fileName = "UnlockedLevels.txt"; 
        
        // future plans: new game, load game (multiple save files)

        //constructor
        public SaveInfo()
        {
            levels = new Dictionary<string, bool>();
        }

        // reading the unlocked file
        public Dictionary<string,bool> ReadUnlock()
        {
            // reading in the file
            StreamReader input = null;

            input = new StreamReader(fileName);
            string temp = "";
            string name = "";
            bool unlocked = false;

            while ((temp = input.ReadLine()) != null)
            {
                string[] words = temp.Split(' ');
                name = words[0];
                temp = words[1];
                unlocked = Convert.ToBoolean(temp);
                levels.Add(name, unlocked);
            }

            input.Close();
            return levels;
        }

        // this will update the file containing the levels that have been unlocked
        public void UpdateFile()
        {
            StreamWriter output = null;
            output = new StreamWriter(fileName);

            for(int x = 0;x < levels.Count; x++)
            {
                output.WriteLine(levels.Keys + ", " + levels.Values);
            }
        }

        // after sucessfully finishing a level the next level will be unlocked
        public void LevelUnlocked(string lvlName) // get it so that the name of the current level identifies the next one
        {
            levels[lvlName] = true;
        }
        
    }
}
