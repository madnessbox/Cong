﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField]
    private int score = 0;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject speedChanger;
    [SerializeField]
    private GameObject ballPrefab;

    [Header("References")]
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text speedMultiplierText;

    [SerializeField]
    private GameObject[] playerObjects;

    [System.Serializable]
    struct SpawnSetting
    {
        public GameObject itemPrefab;
        public bool spawnAfterScore;
        public float spawnInterval;
        public float spawnScoreInterval;
    }

    [SerializeField]
    private SpawnSetting[] itemSpawnSettings;

    private List<SpawnSetting> itemsToSpawnAfterScore = new List<SpawnSetting>();

    void Start()
    {
        if (PlayerPrefs.HasKey("Is4Player"))
        {
            if (PlayerPrefs.GetInt("Is4Player") == 1)
            {
                playerObjects[2].SetActive(true);
                playerObjects[3].SetActive(true);
            }
        }
        


        foreach (SpawnSetting toSpawn in itemSpawnSettings)
        {
            if (toSpawn.spawnAfterScore == false && 
                toSpawn.itemPrefab != null && 
                toSpawn.spawnInterval > 1)
            {
                StartCoroutine(StartPickupTimer(toSpawn.itemPrefab, toSpawn.spawnInterval));
            }
            else if (toSpawn.spawnAfterScore)
            {
                itemsToSpawnAfterScore.Add(toSpawn);
            }
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();

        foreach (SpawnSetting toSpawn in itemsToSpawnAfterScore)
        {
            if (score % toSpawn.spawnScoreInterval == 0)
            {
                SpawnPickup(toSpawn.itemPrefab);
            }
        }
    }

    public void DecreaseScore(int amount)
    {
        score = score - amount < 0 ? 0 : score - amount;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "0";
    }

    public void ResetScoreAndSpeedMultiplier()
    {
        score = 0;
        scoreText.text = "0";

        speedMultiplierText.text = "x1";
    }

    public void SetSpeedMultiplierText(float value)
    {
        speedMultiplierText.text = "x" + value.ToString("0.0");
    }

    public void SpawnPickup(GameObject pickupToSpawn, float timeBetweenSpawns)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        Vector2 randomPos = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized * Random.Range(0f, 4f);
        GameObject spawnedPickup = Instantiate(pickupToSpawn, randomPos, Quaternion.identity);
        StartCoroutine(StartPickupTimer(pickupToSpawn, timeBetweenSpawns));
    }

    public void SpawnPickup(GameObject pickupToSpawn)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        Vector2 randomPos = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized * Random.Range(0f, 4f);
        GameObject spawnedPickup = Instantiate(pickupToSpawn, randomPos, Quaternion.identity);
    }

    IEnumerator StartPickupTimer(GameObject pickupToSpawn, float timeBetweenSpawns)
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnPickup(pickupToSpawn, timeBetweenSpawns);
    }


    public void SpawnBall()
    {
        Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
    }

}
