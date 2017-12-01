using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeSystem : MonoBehaviour {

	//--------INPUT-------------
	public int num_life_max;
	public float parameter_speedup;
	public float parameter_farpunch;
	public float time_dush;
	public float range_original;
	public float speed_original;
	public float error;
    public GameObject scoreManager;
	public GameObject effect_rush;
	//-----------OUTPUT-------------
	public float speed;
	public bool flag_inrush;
	//--------STATIC------------
	public static LifeSystem instance;
	//--------PRIVATE-----------
	private int num_life_present;
	private float time_dushstart;
	
	//---------TEST-------------

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	// Use this for initialization
	void Start ()
	{
		num_life_present = num_life_max;
		speed = 1;
		flag_inrush = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (flag_inrush && Mathf.Abs(Time.time - time_dushstart-time_dush) < error)
		{
			out_candy();
		}
	}

	public int get_lifenum()
	{
		return num_life_present;
	}

	public int life_decrease()
	{
		num_life_present--;
		if (num_life_present < 0)
		{
			die();
		}
		UI.instance.setlife(num_life_present,num_life_max);
        Players.instance.GetComponent<Animator>().SetBool("Tripping", true);
        return num_life_present;
	}

	public int life_increase()
	{
		num_life_present += num_life_present < num_life_max ? 1 : 0;
		UI.instance.setlife(num_life_present,num_life_max);
		return num_life_present;
	}

	public void eat()
	{
		flag_inrush = true;
		
			//life_decrease();
			speed = parameter_speedup;
			//Time.timeScale = speed;
			Players.instance.GetComponent<Players>().speed = parameter_speedup;
			Players.instance.GetComponent<Players>().distane_punch = parameter_farpunch;
        scoreManager.GetComponent<ScoreManager>().scoreMultiplier = 2.5f;

			UI.instance.glowing();
			time_dushstart = Time.time;
		effect_rush.active = true;
		//StartCoroutine(dush(time_dushstart));

	}

	public void out_candy()
	{
		speed = 1;
		Players.instance.GetComponent<Players>().speed = speed_original;
		Players.instance.GetComponent<Players>().distane_punch  = range_original;
        scoreManager.GetComponent<ScoreManager>().scoreMultiplier = 1.0f;
        flag_inrush = false;
		effect_rush.active = false;
		UI.instance.stop_glowing();

	}

	public void die()
	{
		UI.instance.die();
		Players.instance.gameObject.GetComponent<Animator>().SetTrigger("lose");
		Players.instance.gameObject.GetComponent<Players>().enabled = false;
		Players.instance.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
		MajorSound.instance.lose_audio();
		StartCoroutine(delayload());
        scoreManager.GetComponent<ScoreManager>().isIncreasing = false;
        
	}

	IEnumerator delayload()
	{
		yield return new WaitForSecondsRealtime(5f);
		SceneManager.LoadScene(2);
	}

	/*IEnumerator dush(float time_enter)
	{
		yield return new WaitForSecondsRealtime(time_dush);
		if(Time.time-time_enter>time_dush-0.2 && Time.time-time_enter<time_dush+0.2) out_candy();
	}*/
}
