using _2D_Simple_Mobile_Starter_pack.Scripts.Managers;
using UnityEngine;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.FancyStuff
{
    public class Party : MonoBehaviour
    {
        [SerializeField] private GameObject confetti;
    
        void Start()
        {
            GameManager.Instance.OnHighScoreBeaten += OnHighScore;
        }

        private void OnHighScore()
        {
            if (GameManager.Instance.gameState == GameManager.GameState.End) return;
            Instantiate(confetti, transform.position, Quaternion.identity);
        }
    }
}
