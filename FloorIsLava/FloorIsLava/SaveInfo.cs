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
        private const string UNLOCKEDLEVELFILE = "UnlockedLevels.txt";
        
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
            Dictionary<string, bool> levelsUnlockedDict = new Dictionary<string, bool>();
            input = new StreamReader(UNLOCKEDLEVELFILE);
            string levelLine;
            while ((levelLine = input.ReadLine()) != null)
            {
                string[] levelLineArray = levelLine.Split(':');
                
                string level = levelLineArray[0];
                string levelBool = levelLineArray[1];
                
                bool isUnlocked = false;
                
                if (levelBool == "true")
                {
                    isUnlocked = true;
                }

                levelsUnlockedDict.Add(level, isUnlocked);
            }
            input.Close();
            return levelsUnlockedDict;
            
        }

        // this will update the file containing the levels that have been unlocked
        public void UpdateFile()
        {
            StreamWriter output = null;
            output = new StreamWriter(UNLOCKEDLEVELFILE);

            for(int x = 0;x < levels.Count; x++)
            {
                output.WriteLine(levels.Keys + ":" + levels.Values);
            }

            output.Close();
        }
        // after sucessfully finishing a level the next level will be unlocked
        public void LevelUnlocked(string lvlName) // get it so that the name of the current level identifies the next one
        {
            levels[lvlName] = true;
        }

        public void UnlockNextLvl(Dictionary<string, bool> levelsUnlockedDict)
        {
            string[] keys = levelsUnlockedDict.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                bool levelBool = levelsUnlockedDict[keys[i]];

                if (levelBool == false)
                {
                    levelsUnlockedDict[keys[i]] = true;
                    return;
                }

            }
        }
        // current level

        
    }
}
