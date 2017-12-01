using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class Crush : MonoBehaviour {

	//-------INPUT-----------
	public float time_wudi;

	public Material m1;

	public Material m2;
	
	//----------PRIVATE-----------
	public bool wudi;
	
	//--------TEST---------
	public int i;
	// Use this for initialization
	void Start ()
	{
		wudi = false;
		i = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		
		if (other.tag == "Player" && !wudi)
		{
			
			collide();
			
		}
	}

	private void collide()
	{
		//------here add other collide feedback------------
		i++;
		//--------------end-------------------------------
		if (Tutorial.instance.isInTutorial)
		{
			//print("crush running tutorial");
			Players.instance.position=new Vector3(Players.instance.position.x,Players.instance.position.y,
				Players.instance.position.z-Tutorial.instance.distance_approaching[Tutorial.instance.stage-1]);
		}
		else
		{

			if (gameObject.layer != LayerMask.NameToLayer("Tutorial"))
			{

				if (LifeSystem.instance.speed > 1)
				{
					LifeSystem.instance.out_candy();
					MajorSound.instance.rushend_audio();
				}
				else
				{
					LifeSystem.instance.life_decrease();

				}
				StartCoroutine(state_wudi());
			}
			else
			{
				//print("crush tutorial");
			}
		}
		
	}

	IEnumerator state_wudi()
	{
		wudi = true;
		float time = 0;
		while (time < time_wudi)
		{
			time += 0.05f;
			yield return new WaitForSecondsRealtime(0.05f);
			//m1.
		}
		//yield return new WaitForSecondsRealtime(time_wudi);
		wudi = false;
	}
	
	
	
	
}
