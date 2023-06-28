using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace PrefabBehaviour
{
    public class EnemySpawner : MonoBehaviour
    {
        private float leftX;
        private float rightX;
        private float topY;
        private float bottomY;
        
        [Header("Location")]
        [SerializeField] private Vector3 offset;
        [SerializeField] private bool onTopOfScreen = true;
        [Header("Enemy Prefabs")]
        [SerializeField] private GameObject[] enemyPrefab;
        [SerializeField] private bool randomizeEnemyPrefab;
        private int enemyPrefabIndex;
        [FormerlySerializedAs("spawnRate")]
        [Header("Spawn time")]
        [SerializeField] private float minimumSpawnRate = 1f;
        private float timer;
        
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
            Exponential
        }
        [SerializeField] private RateIncreaseType rateIncreaseType = RateIncreaseType.Exponential;
        [SerializeField] private float exponentialRateMultiplier = 1f; //The lower it is, the longer it takes to reach the minimum spawn rate

        [Header("Spawn references")]
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private bool spawnOnRandomPoints;
        [SerializeField] private BoxCollider2D spawnArea;
        private int spawnPointIndex;
        
        private enum SpawnType
        {
            Points,
            ByScreenWidth,
            ByScreenHeight,
            ByFullScreen,
            InsideSpawnArea,
            OnTheSpot
        }

        [SerializeField] private SpawnType spawnType = SpawnType.OnTheSpot;
        
        
        void Start()
        {
            if (onTopOfScreen) ToTheTopOfTheScreen();
            if (spawnType is SpawnType.ByFullScreen or SpawnType.ByScreenHeight or SpawnType.ByScreenWidth) SetScreenBounds();
            GameManager.instance.onGameStart += OnGameStarted;
            if (GameManager.instance.gameState == GameManager.GameState.Play) OnGameStarted();
        }

        private void ToTheTopOfTheScreen()
        {
            if (Camera.main != null)
            {
                transform.localPosition = new Vector3(0f,
                    Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + offset.y, 0f);
            }
        }

        private void SetScreenBounds()
        {
            if (Camera.main != null)
            {
                leftX = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
                rightX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
                topY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
                bottomY = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
            }
        }

        private void OnGameStarted()
        {
            StartCoroutine(SpawnEnemy());
        }

        private void OnDisable()
        {
            GameManager.instance.onGameStart -= OnGameStarted;
            StopCoroutine(SpawnEnemy());
        }

        private void Update()
        {
            if (rateIncreaseCause == RateIncreaseCause.Time && GameManager.instance.gameState == GameManager.GameState.Play ) timer += Time.deltaTime;
        }

        private IEnumerator SpawnEnemy()
        {
            if (enemyPrefab.Length == 0) yield break;
            while (true)
            {
                yield return new WaitForSeconds(GetDecreasedRate());
                Vector3 spawnPosition = Vector3.zero;
                int enemyIndex = enemyPrefabIndex;
                if (randomizeEnemyPrefab) enemyIndex = Random.Range(0, enemyPrefab.Length);
                else
                {
                    enemyPrefabIndex++;
                    if (enemyPrefabIndex >= enemyPrefab.Length) enemyPrefabIndex = 0;
                }
                
                switch (spawnType)
                {
                    case SpawnType.Points:
                        if (spawnPoints.Length == 0) yield break;
                        var index = spawnPointIndex;
                        if (spawnOnRandomPoints) index = Random.Range(0, spawnPoints.Length);
                        else spawnPointIndex++;
                        spawnPosition = spawnPoints[index].position;
                        break;
                    case SpawnType.ByScreenWidth: 
                        float randomX = Random.Range(leftX, rightX);
                        spawnPosition = new Vector3(randomX, transform.position.y, 0f);
                        break;
                    case SpawnType.ByScreenHeight:
                        float randomY = Random.Range(bottomY, topY);
                        spawnPosition = new Vector3(transform.position.x, randomY, 0f);
                        break;
                    case SpawnType.ByFullScreen:
                        float randomX2 = Random.Range(leftX, rightX);
                        float randomY2 = Random.Range(bottomY, topY);
                        spawnPosition = new Vector3(randomX2, randomY2, 0f);
                        break;
                    case SpawnType.InsideSpawnArea:
                        if (!spawnArea) yield break;
                        var bounds = spawnArea.bounds;
                        float randomX3 = Random.Range(bounds.min.x, bounds.max.x);
                        float randomY3 = Random.Range(bounds.min.y, bounds.max.y);
                        spawnPosition = new Vector3(randomX3, randomY3, 0f);
                        break;
                    case SpawnType.OnTheSpot:
                        spawnPosition = transform.position;
                        break;
                }
                
                GameObject obj = Instantiate(enemyPrefab[enemyIndex], spawnPosition, Quaternion.identity);
                obj.transform.SetParent(transform);
            }
        }
        
        

        private float GetDecreasedRate()
        {
            var rateAdd = 0f;
            
            switch (rateIncreaseCause)
            {
                case RateIncreaseCause.Time:
                    rateAdd = timer;
                    break;
                case RateIncreaseCause.Score:
                    rateAdd = GameManager.instance.currentScore;
                    if (rateAdd == 0) rateAdd = 1f;
                    break;
            }

            switch (rateIncreaseType)
            {
                case RateIncreaseType.Exponential:
                    if (rateAdd != 0) rateAdd = 1f/(float) Math.Pow(rateAdd, exponentialRateMultiplier);
                    break;
                case RateIncreaseType.Linear:
                    if (rateAdd != 0) rateAdd = 1f/rateAdd;
                    break;
            }

            return minimumSpawnRate + rateAdd;
        }
    
    }
}
