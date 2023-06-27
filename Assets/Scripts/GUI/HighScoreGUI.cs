
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GUI
{
    public class HighScoreGUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private Color colorBase;
        [SerializeField] private Color colorHighScoreBeat;
        void Start()
        {
            if (!highScoreText) highScoreText = GetComponent<TextMeshProUGUI>();
            GameManager.instance.onScoreChange+= OnScore;
            GameManager.instance.onHighScoreBeaten += OnHighScoreBeat;
            highScoreText.color = colorBase;
            highScoreText.text = DataBase.DataBase.GetHighScore().ToString();
            
        }
        
        private void OnScore(int score)
        {
            if (GameManager.instance.currentScore >= DataBase.DataBase.GetHighScore())
            {
                highScoreText.text = GameManager.instance.currentScore.ToString();
            }
        }
        private void OnHighScoreBeat()
        {
            highScoreText.color = colorHighScoreBeat;
        }
        
    }
}
