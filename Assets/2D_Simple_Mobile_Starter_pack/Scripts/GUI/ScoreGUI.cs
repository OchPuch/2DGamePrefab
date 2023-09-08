using System.Net.Mime;
using _2D_Simple_Mobile_Starter_pack.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.GUI
{
    [RequireComponent(typeof(Text))]
    public class ScoreGUI : MonoBehaviour
    {
        [SerializeField]
        private Text scoreText;

        private void Start()
        {
            if (!scoreText) scoreText = GetComponent<Text>();
            OnScore(0);
            GameManager.Instance.OnScoreChange += OnScore;
        }
        private void OnScore(int score)
        {
            if (scoreText) scoreText.text = GameManager.Instance.currentScore.ToString();
        }
    }
}
