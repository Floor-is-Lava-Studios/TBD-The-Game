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
        private static Dictionary<string, bool> levelsUnlockedDict;
        private const string UNLOCKEDLEVELFILE = "UnlockedLevels.txt";

        private const string HIGHSCORE = "HighScores.txt";
        private static Dictionary<string, int> highScoreDict;
        
        // future plans: new game, load game (multiple save files)

        //constructor
        public SaveInfo()
        {
            levelsUnlockedDict = ReadUnlock();
            highScoreDict = ReadHighScore();
        }

        // reading the unlocked file
        public Dictionary<string,bool> ReadUnlock()
        {
            // reading in the file
            StreamReader input = null;
            levelsUnlockedDict = new Dictionary<string, bool>();
            input = new StreamReader(UNLOCKEDLEVELFILE);
            string levelLine;
            while ((levelLine = input.ReadLine()) != null)
            {
                string[] levelLineArray = levelLine.Split(':');
                
                string level = levelLineArray[0];
                string levelBool = levelLineArray[1];
                
                bool isUnlocked = false;
                
                if (levelBool == "True")
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
            string[] keys = levelsUnlockedDict.Keys.ToArray();

            for(int x = 0; x < keys.Length; x++)
            {
                output.WriteLine(keys[x] + ":" + levelsUnlockedDict[keys[x]]);
            }

            output.Close();
        }
        // after sucessfully finishing a level the next level will be unlocked
        public void LevelUnlocked(string lvlName) // get it so that the name of the current level identifies the next one
        {
            levelsUnlockedDict[lvlName] = true;
        }

        //Unlock Next Level Method
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

        //Reset Level Lock Method
        public void ResetLevelLock()
        {
            StreamWriter writer = new StreamWriter(UNLOCKEDLEVELFILE);

            string[] keys = levelsUnlockedDict.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                if (i == 0)
                {
                    levelsUnlockedDict[keys[i]] = true;
                    writer.WriteLine(keys[i] + ":True");
                }
                else
                {
                    levelsUnlockedDict[keys[i]] = false;
                    writer.WriteLine(keys[i] + ":False");
                }
            }

            writer.Close();
        }
        
        // HighScore File reader
        public Dictionary<string,int> ReadHighScore()
        {
            StreamReader input = null;
            input = new StreamReader(HIGHSCORE);
            highScoreDict = new Dictionary<string, int>();
            string levelLine;
            string scoreTemp;
            int score;
            while ((levelLine = input.ReadLine()) != null)
            {
                scoreTemp = input.ReadLine();

                int.TryParse(scoreTemp, out score);

                highScoreDict.Add(levelLine, score);
            }
            input.Close();
            return highScoreDict;
        }

        public void UpdateHighScore()
        {
            StreamWriter output = null;
            output = new StreamWriter(HIGHSCORE);
            string[] keys = highScoreDict.Keys.ToArray();

            for (int x = 0; x < keys.Length; x++)
            {
                output.WriteLine(keys[x]);
                output.WriteLine(highScoreDict[keys[x]]);
            }

            output.Close();
        }
        public void newHighScore(string lvlName, int score) // get it so that the name of the current level identifies the next one
        {
            if(highScoreDict[lvlName] < score)
            {
                highScoreDict[lvlName] = score;
            }
        }
        
    }
}
