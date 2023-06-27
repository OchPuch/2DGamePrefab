
using UnityEngine;

namespace DataBase
{
    public static class DataBase
    {
        public static readonly string HighScoreKey = "HighScore";
        
        public static int GetHighScore()
        {
            if (PlayerPrefs.HasKey(HighScoreKey))
            {
                return PlayerPrefs.GetInt(HighScoreKey);
            }

            PlayerPrefs.SetInt(HighScoreKey, 0);
            return 0;
        }
        
        public static void SetHighScore(int score)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
        }
    }
}