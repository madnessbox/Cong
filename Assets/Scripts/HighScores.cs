using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    [SerializeField]
    private Text[] HighScoresTexts = new Text[5];

    private int[] topScores = new int[5];

    void Start()
    {
        PrintScoresToUI();
    }

    void GetScores()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            topScores[0] = PlayerPrefs.GetInt("HighScore");
        }

        for (int i = 1; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("Score" + i))
            {
                topScores[i] = PlayerPrefs.GetInt("Score" + i);
            }
        }

    }

    void PrintScoresToUI()
    {
        for (int i = 0; i < topScores.Length; i++)
        {
            if (topScores[i] == 0)
            {
                HighScoresTexts[i].text = "No Score";
            }
            else
            {
                HighScoresTexts[i].text = topScores[i].ToString();
            }
        }
    }

    public void SubmitScore(int score)
    {
        for (int i = topScores.Length - 1; i >= 0; i--)
        {
            if (score > topScores[i])
            {
                topScores[i] = score;
                HighScoresTexts[i].text = score.ToString();
                return;
            }
        }
    }
}
