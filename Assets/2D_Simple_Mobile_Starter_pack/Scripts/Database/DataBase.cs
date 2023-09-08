
using UnityEngine;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.Database
{
    public static class DataBase
    {
        private const string HighScoreKey = "HighScore";
        
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