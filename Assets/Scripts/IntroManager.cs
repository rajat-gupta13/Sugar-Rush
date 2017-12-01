using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {

    public Image introImage;
    public Sprite[] spriteArray;
    private float timeFromStart;
    private int index;
	// Use this for initialization
	void Start () {
        introImage.sprite = spriteArray[0];
        timeFromStart = 0.0f;
        index = 1;
	}
	
	// Update is called once per frame
	void Update () {
        timeFromStart += Time.deltaTime;
        if (timeFromStart > 3.5f && index == 1)
        {
            introImage.sprite = spriteArray[1];
            index++;
            //SceneManager.LoadScene(1);
        }
        else if (timeFromStart > 7.0f && index == 2)
        {
            introImage.sprite = spriteArray[2];
            index++;
        }
        else if (timeFromStart > 10.0f && index == 3)
        {
            SceneManager.LoadScene(1);
        }
    }
}
