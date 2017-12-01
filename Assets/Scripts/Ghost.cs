using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Ghost : MonoBehaviour {

	//--------INPUT---------
	public float time_loop;
	public float height_loop;
	public bool flag_stop;

	public float time_punch;

	public AudioClip clip_die;
	//---------OUTPUT-----------
	public Candy candy;
	//public int num_smooth;
	//--------PRIVATE---------
	private float y_original;
	// Use this for initialization
	void Start ()
	{
		y_original = transform.position.y;
		StartCoroutine(move());
	}
	
	// Update is called once per frame
	void Update () {
		if (flag_stop)
		{
			
			GetComponent<Animator>().SetTrigger("DIE");
			//wait_get();
		}
	}

	IEnumerator move()
	{
		Rigidbody rig=GetComponent<Rigidbody>();
		rig.velocity=new Vector3(0,height_loop/time_loop,0);
		yield return new WaitForSecondsRealtime(time_loop/2);
		while (!flag_stop)
		{
			rig.velocity=-rig.velocity;
			yield return new WaitForSecondsRealtime(time_loop);
		}
	}

	public void die()
	{
		MajorSound.instance.candyget_audio();
		GetComponent<AudioSource>().clip = clip_die;
		GetComponent<AudioSource>().loop = false;
		GetComponent<AudioSource>().Play();
		Destroy( gameObject);
	}

	/*IEnumerator wait_get()
	{
		yield return new WaitForSecondsRealtime(time_punch);
		MajorSound.instance.candyget_audio();

	}*/
}
