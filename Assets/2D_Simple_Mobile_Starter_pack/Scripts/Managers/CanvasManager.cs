using System.Collections;
using UnityEngine;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject deathScreen;
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject playScreen;
        
        [SerializeField] private float deathScreenDelay = 1f;
        private void Start()
        {
            GameManager.Instance.OnGameStart += OnGameStart;
            GameManager.Instance.OnGameOver += OnGameEnd;
            
            if (deathScreen) deathScreen.SetActive(false);
            if (playScreen) playScreen.SetActive(false);
            if (startScreen) startScreen.SetActive(true);
        }
    
        private void OnGameStart()
        {
            if (deathScreen) deathScreen.SetActive(false);
            if (playScreen) playScreen.SetActive(true);
            if (startScreen) startScreen.SetActive(false);
        }
    
        private void OnGameEnd()
        {
            if (deathScreen)
            {
                if (deathScreenDelay <= 0)
                {
                    deathScreen.SetActive(true);
                }
                else
                {
                    StartCoroutine(WaitAndSetActive(deathScreen, true, deathScreenDelay));
                }
            }
            
            if (playScreen) playScreen.SetActive(false);
            if (startScreen) startScreen.SetActive(false);
        }
        
        private static IEnumerator WaitAndSetActive(GameObject go, bool state, float time)
        {
            yield return new WaitForSeconds(time);
            go.SetActive(state);
        }
    
    }
}
