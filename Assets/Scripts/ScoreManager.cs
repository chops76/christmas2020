using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public float scoreCount;
    private float highScoreCount;

    public float pointsPerSecond;
    public bool scoreIncreasing = true;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High: " + Mathf.Round(highScoreCount);
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", highScoreCount);
    }

    public void addScore(int points)
    {
        scoreCount += points;
    }
}
