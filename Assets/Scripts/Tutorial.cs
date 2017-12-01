using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class Tutorial : MonoBehaviour {
	//---------INPUT-------------
	public float[] distance_approaching;
	public float timescale;
	public int num_obstacle;
	//------OUTPUT_----------
	public bool isInTutorial;


	//------PRIVATE----------
	public Transform[] obstacles;
	public int stage;
	
	//-----------TEST-------------
	public bool outoftutorial;
	
	//--------STATIC-----------
	public static Tutorial instance;
	
	//public float[] edges;
	// 0 for start; n for tutorial part n
	// Use this for initialization
	void Awake()
	{
		if (instance == null) instance = this;
	}
	void Start ()
	{
		
		obstacles = new Transform[num_obstacle+1];
		//obstacles = GetComponentsInChildren<Transform>();
		obstacles[0] = transform;
		int i = 1;
		foreach (Transform ob in GetComponentsInChildren<Transform>())
		{
			if (ob.parent == transform)
			{
				obstacles[i] = ob;
				i++;
			}
		}
		stage = 0;
		//num_obstacle = obstacles.Length-1;
		isInTutorial = false;
		
		//edges = new float[4];
	}
	
	// Update is called once per frame
	void Update () {

		
		if (!isInTutorial && stage < num_obstacle &&
		    obstacles[stage+1].position.z-Players.instance.position.z < distance_approaching[stage])
		{
			stage++;
			isInTutorial = true;
			UI.instance.showtutorial(stage);
			Time.timeScale = timescale;
		}
		else if (isInTutorial && CheckTutorialCompleted(stage))
		{
			isInTutorial = false;
			UI.instance.quittutorial();
			Time.timeScale = 1;
			
			if (stage >= num_obstacle)
			{
				this.enabled = false;
			}
		}
		
	}

	private bool CheckTutorialCompleted(int num)
	{
		if (outoftutorial)
		{
			outoftutorial = false;
			return true;
		}
		switch (num)
		{
			case 2:
			{
				return Players.instance.gameObject.GetComponent<Players>().jumping;
				break;
			}
			case 3:
			{
				return Players.instance.gameObject.GetComponent<Players>().sliding;
				break;
			}
			case 1:
			{
				
				return !Collision(Players.instance.gameObject,obstacles[num].gameObject );
				break;
			}
			case 4:
			{
				return Players.instance.gameObject.GetComponent<Players>().punching;
				break;
			}
		}
		
		return false;

	}

	/*private bool NonCollision(float position_player,float scale_player, float position_obstacle, float scale_obstacle) // x_s
	{
		return (position_player-0.5f*scale_player>position_obstacle + 0.5f*scale_obstacle)||
		       (position_player+0.5f*scale_player<position_obstacle - 0.5f*scale_obstacle);
	}*/

	public bool Collision(GameObject player, GameObject obstacle)
	{
		CapsuleCollider collider_player = player.GetComponent<CapsuleCollider>();
		BoxCollider collider_obstacle = obstacle.GetComponent<BoxCollider>();
		if (collider_obstacle == null || collider_player == null)
		{
			//
			return false;
		}
		/*edges[0] = (player.transform.position.x+collider_player.center.x -
		           collider_player.radius*0.5f )* player.transform.lossyScale.x;
		edges[1] = (obstacle.transform.position.x+collider_obstacle.center.x +
		           collider_obstacle.size.x*0.5f) * obstacle.transform.lossyScale.x;
		edges[2] = (player.transform.position.x+collider_player.center.x+
		           collider_player.radius*0.5f) * player.transform.lossyScale.x ;
		edges[3] = (obstacle.transform.position.x+collider_obstacle.center.x-
		           collider_obstacle.size.x *0.5f)* obstacle.transform.lossyScale.x ;
		
		return edges[0] > edges[1] || edges[2] < edges[3];*/
		// rotation: not x
		
		Ray ray=new Ray(player.transform.position,player.transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 2000f))
		{
		
			return (hit.collider == collider_obstacle);
		}
		else return false;
	}
}
