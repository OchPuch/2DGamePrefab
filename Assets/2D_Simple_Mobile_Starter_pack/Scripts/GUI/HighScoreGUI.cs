using _2D_Simple_Mobile_Starter_pack.Scripts.Database;
using _2D_Simple_Mobile_Starter_pack.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.GUI
{
    [RequireComponent(typeof(Text))]
    public class HighScoreGUI : MonoBehaviour
    {
        [SerializeField] private Text highScoreText;
        
        void Start()
        {
            if (!highScoreText) highScoreText = GetComponent<Text>();
            GameManager.Instance.OnScoreChange+= OnScore;
            highScoreText.text = DataBase.GetHighScore().ToString();
        }
        
        private void OnScore(int score)
        {
            if (GameManager.Instance.currentScore >= DataBase.GetHighScore())
            {
                highScoreText.text = GameManager.Instance.currentScore.ToString();
            }
        }
       
        
    }
}
