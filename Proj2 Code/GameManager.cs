using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isGameOver = false;
    public TextMeshProUGUI coinText;
    private int coinCount = 0;
    private int enemyCount = 3;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public GoldenPlatformSpawner platformSpawner;
    
    void Awake() { instance = this; }
    
    public void AddCoin()
    {
        coinCount++;
        coinText.text = "Coins: " + coinCount;
        Debug.Log("Coin collected! Total: " + coinCount);
        if (coinCount == 10) {
            {
            //platformSpawner.SpawnGoldenPlatform();
        }
        }
    }
       
        
    public void EnemyDefeated()
    {
        enemyCount--;
        if (enemyCount <= 0) SpawnBoss();
    }
    
    void SpawnBoss()
    {
        if (bossPrefab != null && bossSpawnPoint != null)
        {
            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            Debug.Log("Boss Spawned!");
        }
        else
        {
            Debug.LogError("Boss Prefab or Boss Spawn Point not assigned in GameManager.");
        }
    }

    
    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over");
    }
    
    public void WinGame() { Debug.Log("You Win!"); }
}
