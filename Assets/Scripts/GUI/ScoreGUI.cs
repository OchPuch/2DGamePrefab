using Managers;
using UnityEngine;

namespace GUI
{
    public class ScoreGUI : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TextMeshPro scoreText;
        [SerializeField]
        private TMPro.TextMeshProUGUI scoreTextGUI;
        void Start()
        {
            if (!scoreText) scoreText = GetComponent<TMPro.TextMeshPro>();
            if (!scoreTextGUI) scoreTextGUI = GetComponent<TMPro.TextMeshProUGUI>();
            OnScore(0);
            GameManager.instance.onScoreChange += OnScore;
        }
        private void OnScore(int score)
        {
            if (scoreText) scoreText.text = GameManager.instance.currentScore.ToString();
            if (scoreTextGUI) scoreTextGUI.text = GameManager.instance.currentScore.ToString();
        }
    }
}
