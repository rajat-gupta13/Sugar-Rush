using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PumpkinKing : MonoBehaviour
{

	public float distance_trigger;

	public GameObject candyrain1;
    public GameObject scoreManager;
    public GameObject highScoreText;

	public GameObject candyrain2;
    private bool flag;
	// Use this for initialization
	void Start () {
        flag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Players.instance.position.z > distance_trigger && !flag)
		{
            flag = true;
			Players.instance.gameObject.GetComponent<Players>().enabled = false;
			Players.instance.gameObject.GetComponent<Animator>().SetTrigger("win");
			Players.instance.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
			StartCoroutine(UI.instance.credits());
			candyrain1.active = true;
			candyrain2.active = true;
			MajorSound.instance.win_audio();
            scoreManager.GetComponent<ScoreManager>().isIncreasing = false;
            highScoreText.SetActive(true);
            highScoreText.GetComponent<Text>().text = "High Score: " + Mathf.Round(ScoreManager.hiScoreCount);
            StartCoroutine(delayshow());

		}
	}

    IEnumerator delayshow()
    {
        yield return new WaitForSecondsRealtime(15f);
    }
}
