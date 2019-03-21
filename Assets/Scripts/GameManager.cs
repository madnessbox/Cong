using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField]
    private int score = 0;

    [SerializeField]
    private int[] scoreMilestones;

    private int milestonesReached = 0;

    private int bounces = 0;
    private float currentTime = 0;
    private float scoreMultiplier = 1;
    private int lastTextShowIndex = 0;
    private int balls = 0;


    [Header("Prefabs")]
    [SerializeField]
    private GameObject speedChanger;
    [SerializeField]
    private GameObject ballPrefab;

    [Header("References")]
    [SerializeField]
    private HighScores highScoreManager;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text bouncesText;

    [SerializeField]
    private Text speedMultiplierText;

    [SerializeField]
    private GameObject[] playerObjects;

    [SerializeField]
    private Animator[] playerPortraits;

    [SerializeField]
    private Animator[] textAnimators;

    [SerializeField]
    private GameObject[] onScreenPopups;

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

        SpawnBall();
    }

    private void FixedUpdate()
    {
        SetBpsMultiplierText();
    }

    public void IncreaseBps(int amount)
    {
        bounces += amount;
    }

    public void IncreaseScore(int amount)
    {
        score += (int)(500f * scoreMultiplier);
        scoreText.text = score.ToString();

        if (milestonesReached < scoreMilestones.Length)
        {
            if (score >= scoreMilestones[milestonesReached])
            {
                milestonesReached++;

                int nextTextIndex = 0;
                do
                {
                    nextTextIndex = Random.Range(0, 5);
                } while (nextTextIndex != lastTextShowIndex);

                switch (nextTextIndex)
                {
                    case 0:
                        TriggerPopupText(textAnimators[0]);
                        break;
                    case 1:
                        TriggerPopupText(textAnimators[1]);
                        break;
                    case 2:
                        TriggerPopupText(textAnimators[2]);
                        break;
                    case 3:
                        TriggerPopupText(textAnimators[3]);
                        break;

                }
            }
        }

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
        scoreMultiplier = 1 + (bounces / Time.time);
        bouncesText.text = (scoreMultiplier).ToString("0.0");
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
    }

    IEnumerator StartPickupTimer(GameObject pickupToSpawn, float timeBetweenSpawns)
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnPickup(pickupToSpawn, timeBetweenSpawns);
    }

    public void SpawnBall()
    {
        Ball ballTemp = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity)?.GetComponent<Ball>();
        ballTemp.gm = this;
        balls++;
    }

    public void AnimatePortrait(Player playerIdentifier)
    {
        switch (playerIdentifier.GetPlayerIndex())
        {
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
            default:
                Debug.Log("playerIdentifier invalid");
                break;
        }
    }

    public void TriggerPopupText(Animator animator)
    {
        animator.SetTrigger("PlayAnim");
        foreach (Animator anim in playerPortraits)
        {
            anim.SetTrigger("PlayAnim");
        }
    }

    public void DecreaseBallsActive()
    {
        balls--;
        if (balls <= 0)
        {
            Lost();
        }
    }

    public void StartGame()
    {

    }

    public void Lost()
    {
        highScoreManager.SubmitScore(score);


        score = 0;
    }
}
