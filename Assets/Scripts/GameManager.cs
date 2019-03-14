using System.Collections;
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

    [Header("References")]
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text speedMultiplierText;

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

    void Start()
    {
        foreach (SpawnSetting toSpawn in itemSpawnSettings)
        {
            if (toSpawn.spawnAfterScore == false && 
                toSpawn.itemPrefab != null && 
                toSpawn.spawnInterval > 1)
            {
                StartCoroutine(StartPickupTimer(toSpawn.itemPrefab, toSpawn.spawnInterval));
            }
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
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
        speedMultiplierText.text = "x" + value.ToString();
    }

    public void SpawnPickup(GameObject pickupToSpawn, float timeBetweenSpawns)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        Vector2 randomPos = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized * Random.Range(0f, 4f);
        GameObject spawnedPickup = Instantiate(pickupToSpawn, randomPos, Quaternion.identity);
        StartCoroutine(StartPickupTimer(pickupToSpawn, timeBetweenSpawns));
    }

    IEnumerator StartPickupTimer(GameObject pickupToSpawn, float timeBetweenSpawns)
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnPickup(pickupToSpawn, timeBetweenSpawns);
    }

}
