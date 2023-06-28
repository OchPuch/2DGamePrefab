using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject playScreen;
    
    private void Start()
    {
        GameManager.instance.onGameStart += OnGameStart;
        GameManager.instance.onGameOver += OnGameEnd;
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
        if (deathScreen) deathScreen.SetActive(true);
        if (playScreen) playScreen.SetActive(false);
        if (startScreen) startScreen.SetActive(false);
    }
    
}
