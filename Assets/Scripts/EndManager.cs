using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

    public Text highScoreText, scoreText;
    private float highScore, score;

	// Use this for initialization
	void Start () {
        score = ScoreManager.scoreCount;
        highScore = ScoreManager.hiScoreCount;
        highScoreText.text = "High Score: " + Mathf.Round(highScore);
        scoreText.text = "Score: " + Mathf.Round(score);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
