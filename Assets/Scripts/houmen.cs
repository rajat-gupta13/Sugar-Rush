using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class houmen : MonoBehaviour
{

	public int hm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (hm)
		{
			case 1:
			{
				hm1();
				break;
			}
		}
	}

	private void hm1()
	{
		Players.instance.position=new Vector3(Players.instance.position.x,Players.instance.position.y,765f);
	}
}
