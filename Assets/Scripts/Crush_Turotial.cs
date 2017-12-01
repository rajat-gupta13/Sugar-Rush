using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crush_Turotial : MonoBehaviour {

	//-----TEST------
	public bool restart;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(restart)
		{
			restart = false;
			OnTriggerEnter(Players.instance.GetComponent<Collider>());}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{

			if (Tutorial.instance.isInTutorial)
			{

				Players.instance.position = new Vector3(Players.instance.position.x, Players.instance.position.y,
					Players.instance.position.z - Tutorial.instance.distance_approaching[Tutorial.instance.stage - 1]);
				Animator anim = GetComponent<Animator>();
				if ( anim!= null)
				{
					anim.Rebind();

					anim.SetTrigger("RESTART");
				}
				if (GetComponent<AudioSource>() != null)
				{
					GetComponent<AudioSource>().time = 0;
					GetComponent<AudioSource>().Play();
				}
			}
		}
	}
}
