﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField]
    private int score = 0;
    private int bps = 0;
    private float currentTime = 0;
    private float scoreMultiplier = 1;
    private bool RadicalChecker = true;
    private int perBounceOnRadical = 0;
  

    [Header("Prefabs")]
    [SerializeField]
    private GameObject speedChanger;
    [SerializeField]
    private GameObject ballPrefab;

    [Header("References")]
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text bpsText;

    [SerializeField]
    private Text speedMultiplierText;

    [SerializeField]
    private GameObject[] playerObjects;

    [SerializeField]
    private Animator[] playerPortraits;

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

    public int PerBounceOnRadical { get => perBounceOnRadical; set => perBounceOnRadical = value; }

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

        SpawnBall(false);
    }

    private void FixedUpdate()
    {
        SetBpsMultiplierText();
    }

    public void IncreaseBps(int amount)
    {
        bps += amount;
    }

    public void IncreaseScore(int amount)
    {

        // gör radical om bps <= 2 och sedan sätter RadicalCheckerFalse så det bara händer en gång
        if ((scoreMultiplier >= 2f)&&(RadicalChecker))
        {
            Radical()
            RadicalChecker = false;
        }

        //gör RadicalChecker true om ens bps går för lågt, så att radical kan återaktiveras om man kommer upp till 2 bps igen
        else if (scoreMultiplier <= 1.8f)
            RadicalChecker = true;

        //gör radical igen om man behåller bps >= 2 på 5 bounce
        if (RadicalChecker = false)
        {
            perBounceOnRadical++;
            if (perBounceOnRadical == 5)
            {
                Radical()
                perBounceOnRadical = 0;
            }
                
        }
        


        score += (int)(500f * scoreMultiplier);
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

    public void SetBpsMultiplierText()
    {
        scoreMultiplier = 1 + (bps / Time.time);
        bpsText.text = (scoreMultiplier).ToString("0.0");

       
        
    }

    public void SpawnPickup(GameObject pickupToSpawn, float timeBetweenSpawns)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        Vector2 randomPos = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized * Random.Range(0f, 4f);
        GameObject spawnedPickup = Instantiate(pickupToSpawn, randomPos, Quaternion.identity);
        if (pickupToSpawn.tag == "Multiball")
        {
            spawnedPickup.GetComponent<MultiBallScript>().gm = this;
        }
        StartCoroutine(StartPickupTimer(pickupToSpawn, timeBetweenSpawns));
    }

    public void SpawnPickup(GameObject pickupToSpawn)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        Vector2 randomPos = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized * Random.Range(0f, 4f);
        GameObject spawnedPickup = Instantiate(pickupToSpawn, randomPos, Quaternion.identity);
        //if (pickupToSpawn.tag == "Multiball")
        //{
        //    spawnedPickup.GetComponent<MultiBallScript>().gm = this;
        //}
    }

    IEnumerator StartPickupTimer(GameObject pickupToSpawn, float timeBetweenSpawns)
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnPickup(pickupToSpawn, timeBetweenSpawns);
    }


    public void SpawnBall(bool multiball)
    {
        Ball ballTemp = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity)?.GetComponent<Ball>();
        ballTemp.gm = this;
        if (multiball)
        {
            ballTemp.isMultiball = true;
        }
    }


    //call radical when ballSpeedDelta is over .5
    public void Radical()
    {

        Debug.Log("radical!");

    }

    public void AnimatePortrait(Player playerIdentifier)
    {
        switch (playerIdentifier.getPlayerIndex()) {
            case 0:
                playerPortraits[0].Play("BlueAnim"); 
                break;
            case 1:
                playerPortraits[1].Play("GreenAnim");
                break;
            case 2:
                playerPortraits[2].Play("RedAnim");
                break;
            case 3:
                playerPortraits[3].Play("YellowAnim");
                break;
        }
        
    }

}
