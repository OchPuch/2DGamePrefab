using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        public enum GameState
        {
            Setup,
            Play,
            End
        }
        
        
        public Action onGameOver;
        public Action onGameStart;
        public Action<int> onScoreChange;
        public Action onHighScoreBeaten;
        
        [Header("Settings")]
        [SerializeField] private bool restartOnGameOver = true;
        
        [Header("Game state")]
        public int currentScore = 0;
        public GameState gameState = GameState.Setup;
        
        private void Awake()
        {
            if (!instance) instance = this;
            onScoreChange += AddScore;
            onGameOver += OnGameEnd;
            onGameStart += OnGameBegin;
            onScoreChange?.Invoke(0);
        }

        public void ForceStartGame()
        {
            onGameStart?.Invoke();
        }
    
        public void ForceGameOver()
        {
            onGameOver?.Invoke();
        }
    
        private void OnGameEnd()
        {
            gameState = GameState.End;
            if (restartOnGameOver) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    
        private void OnGameBegin()
        {
            gameState = GameState.Play;
        }

        private void AddScore(int amount)
        {
            instance.currentScore += amount;
        }
    }
}
