using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;

    public static float scoreCount;
    public static float hiScoreCount;

    public float pointsPerSecond;
    public float scoreMultiplier;
    public bool isIncreasing = false;
 

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("HighScore"))
        {
            hiScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
        scoreCount = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (isIncreasing)
        {
            scoreCount += pointsPerSecond * scoreMultiplier * Time.deltaTime;
        }

        if (scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", hiScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
	}
}
