using Managers;
using UnityEngine;

namespace FancyStuff
{
    public class Party : MonoBehaviour
    {
        [SerializeField] private GameObject confetti;
    
        void Start()
        {
            GameManager.instance.onHighScoreBeaten += OnHighScore;
        }

        private void OnHighScore()
        {
            if (GameManager.instance.gameState == GameManager.GameState.End) return;
            Instantiate(confetti, transform.position, Quaternion.identity);
        }
    }
}
