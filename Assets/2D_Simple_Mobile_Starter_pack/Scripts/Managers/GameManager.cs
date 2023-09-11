using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using Screen = UnityEngine.Device.Screen;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [Header("Application settings")]
        public ScreenOrientation screenOrientation = ScreenOrientation.LandscapeLeft;
        public int targetFrameRate = 60;
        
        public enum GameState
        {
            Setup,
            Play,
            End
        }
        
        
        public Action OnGameOver;
        public Action OnGameStart;
        public Action<int> OnScoreChange;
        public Action OnHighScoreBeaten;
        private bool highScoreBeaten = false;
        
        [Header("Settings")]
        [SerializeField] private bool restartOnGameOver = true;
        
        [Header("Game state")]
        public int currentScore = 0;
        public GameState gameState = GameState.Setup;
        
        private void Awake()
        {
            if (!Instance) Instance = this;
            
            Application.targetFrameRate = targetFrameRate;
            Screen.orientation = screenOrientation;
            
            OnScoreChange += AddScore;
            OnGameOver += OnGameEnd;
            OnGameStart += OnGameBegin;
            OnScoreChange?.Invoke(0);
        }

        public void ForceStartGame()
        {
            OnGameStart?.Invoke();
        }
    
        public void ForceGameOver()
        {
            OnGameOver?.Invoke();
        }
        
        public void ForceRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    
        private void OnGameEnd()
        {
            gameState = GameState.End;
            if (restartOnGameOver) ForceRestart();
        }
    
        private void OnGameBegin()
        {
            gameState = GameState.Play;
        }

        private void AddScore(int amount)
        {
            Instance.currentScore += amount;
            if (currentScore <= Database.DataBase.GetHighScore()) return;
            Database.DataBase.SetHighScore(currentScore);
            if (highScoreBeaten) return;
            highScoreBeaten = true;
            OnHighScoreBeaten?.Invoke();
        }
    }
}
