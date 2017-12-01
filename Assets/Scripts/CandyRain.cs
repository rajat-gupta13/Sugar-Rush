using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyRain : MonoBehaviour {

	//---------INPUT--------------
	public int num_candies;
	public float possibility;
	public float angle_min;
	public float angle_max;
	public float velocity_min;
	public float velocity_max;
	public AnimationCurve possibility_angle;
	public AnimationCurve possibility_velocity;
	public Transform parent;
	//----------PRIVATE------------
	public float angle;
	public float direction;
	public float velocity;
	private float seed_float;
	private float seed_int;
	private GameObject newcandy;
	
	//---------TEST----------
	public Vector3 test;

	public float seed_1;

	public float seed_2;

	public float seed_3;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.value < possibility * Time.deltaTime)
		{
			seed_float = Random.value;
			seed_1 = seed_float;
			angle = possibility_angle.Evaluate(seed_float);
			angle = angle_min + (angle_max - angle_min) * angle;
			direction = Random.value * 360;
			seed_float = Random.value;
			seed_2 = seed_float;
			velocity = possibility_velocity.Evaluate(seed_float);
			velocity = velocity_min + (velocity_max - velocity_min) * velocity;
			seed_int = Random.Range(0, num_candies);
			newcandy = Instantiate(Resources.Load<GameObject>("Model/candy" + seed_int.ToString()));
			newcandy.transform.position=transform.position;
			newcandy.transform.parent = parent;
			//newcandy.transform.localPosition=new Vector3(0,0,0);
			newcandy.transform.localScale*=5f;
			test=new Vector3(velocity*Mathf.Cos(angle/180*Mathf.PI)*Mathf.Sin(direction/180*Mathf.PI),
				velocity*Mathf.Cos(angle/180*Mathf.PI)*Mathf.Cos(direction/180*Mathf.PI),velocity*Mathf.Sin(angle/180*Mathf.PI));
			newcandy.GetComponent<Rigidbody>().velocity = test;
		}
	}
}
