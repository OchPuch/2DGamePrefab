using System;
using System.Collections;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PrefabBehaviour
{
    public class EnemySpawner : MonoBehaviour
    {
        private float leftX = 0f;
        private float rightX = 0f;
        private float topY = 0f;
        private float bottomY = 0f;
        
        [Header("Location")]
        [SerializeField] private Vector3 offset = new Vector3(0,0,0);
        [SerializeField] private bool onTopOfScreen = true;
        [Header("Enemy Prefabs")]
        [SerializeField] private GameObject[] enemyPrefab;
        [Header("Spawn time")]
        [SerializeField] private float spawnRate = 1f;
        
        private enum RateIncreaseCause
        {
            Time,
            Score,
            None
        }
        [SerializeField] private RateIncreaseCause rateIncreaseCause = RateIncreaseCause.None;
        private enum RateIncreaseType
        {
            Linear,
            Exponential,
            None
        }
        [SerializeField] private RateIncreaseType rateIncrease = RateIncreaseType.None;
        [Header("Spawn references")]
        
        [SerializeField] private Transform[] spawnPoints;
        private enum SpawnType
        {
            Points,
            ByScreenWidth,
            ByScreenHeight,
            None
        }
        
        [SerializeField] private bool spawnRandomly = false;
        
        void Start()
        {
            ToTheTopOfTheScreen();
            GameManager.instance.onGameStart += OnGameStarted;
            if (GameManager.instance.gameState == GameManager.GameState.Play) OnGameStarted();
        }

        private void ToTheTopOfTheScreen()
        {
            transform.localPosition = new Vector3(0f, Camera.main.ScreenToWorldPoint(new Vector2(0,Screen.height)).y + offset.y, 0f);
            leftX = Camera.main.ScreenToWorldPoint(new Vector2(0,0)).x;
            rightX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,0)).x;
        }

        public void OnGameStarted()
        {
            StartCoroutine(SpawnEnemy());
        }
    
        private IEnumerator SpawnEnemy()
        {
            if (enemyPrefab.Length == 0) yield break;
            while (true)
            {
                yield return new WaitForSeconds(spawnRate +
                                                2f / (GameManager.instance.currentScore == 0
                                                    ? 1
                                                    : (float) Math.Pow(GameManager.instance.currentScore, 1/6f)));
                float randomX = Random.Range(leftX, rightX);
                int randomIndex = Random.Range(0, enemyPrefab.Length);
                GameObject obj = Instantiate(enemyPrefab[randomIndex], new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
                obj.transform.SetParent(transform);
            }
        }

        private void GetIncreasedRate()
        {
            switch (rateIncrease)
            {
                
            }
        }
    
    }
}
