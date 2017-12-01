using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Generation : MonoBehaviour {

	//---------INPUT--------------
	public int num_passway;
	public float possibility_painting;

	
	void Awake()
	{
		generate_passway_all();
		generate_painting_all();
		generate_obstacle_all();

	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void generate_passway(int i)
	{
		GameObject passway = Instantiate(Resources.Load<GameObject>("Model/passway"));
		passway.transform.parent = transform;
		passway.transform.localPosition= new Vector3(0,-0.1f,24*i-19);
	}

	public void generate_painting(float distance,  bool left)
	{
		float height = Random.Range(2, 9.8f);
		float scale = Random.Range(150, 300) ;
		int index = Mathf.FloorToInt(Random.Range(1, 6.99f));
		GameObject painting = Instantiate(Resources.Load<GameObject>("Model/painting" + index.ToString()));
		painting.transform.parent = transform;
		painting.transform.localPosition = new Vector3(left ? -5.8f : 5.8f, height, distance);
		painting.transform.localScale = new Vector3(1, scale, scale);
		if (!left)
		{
			painting.transform.Rotate(painting.transform.up,180,Space.World);
		}

	}

	public void generate_obstacle()
	{
		
	}

	private void generate_passway_all()
	{
		for (int i = 1; i <= num_passway; i++)
		{
			generate_passway(i);
		}
	}

	private void generate_painting_all()
	{
		for (int distance = 3; distance < num_passway * 24-5; distance++)
		{
			if (Random.Range(0, 1) < possibility_painting)
			{
				generate_painting(distance, false);
				distance += 3;
			}
		}
		for (int distance = 3; distance < num_passway * 24-5; distance++)
		{
			if (Random.Range(0, 1) < possibility_painting)
			{
				generate_painting(distance, true);
				distance += 3;
			}
		}
	}

	private void generate_obstacle_all()
	{
		
	}
	
		
	
}
