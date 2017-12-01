using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {
	//---------INPUT---------
	public float speed_rotate1;
	public float speed_rotate2;
	public float scale_flying;
	public float edge_destroy;
	public Vector3 position_local;
	
	//----PRIVATE--------
	private bool rotating;
	// Use this for initialization
	void Start ()
	{
		rotating = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0,1,0),speed_rotate1*Time.deltaTime,Space.World);
		transform.Rotate(new Vector3(1,0,0),speed_rotate1*Time.deltaTime,Space.World);

		if(transform.position.y<edge_destroy) Destroy(gameObject);
		/*if (rotating)
		{
			transform.RotateAround(Players.instance.position,new Vector3(0,1,0),speed_rotate2*Time.deltaTime);
		}*/
	}

	/*public void get()
	{
		Destroy(gameObject);
		transform.parent = Players.instance;
		transform.localScale=new Vector3(scale_flying,scale_flying,scale_flying);
		transform.localPosition=position_local;
		rotating = true;
		MajorSound.instance.candyget_audio();
	}*/
}
