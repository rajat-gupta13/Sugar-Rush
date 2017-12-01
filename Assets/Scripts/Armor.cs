using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
	private Animator animator;

	public float threshold;
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
		animator.enabled = false;
		GetComponent<AudioSource>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.enabled == false && transform.position.z - Players.instance.position.z < threshold)
		{
			animator.enabled = true;
			GetComponent<AudioSource>().enabled = true;
			//GetComponent<AudioSource>().pitch = 3;
			this.enabled = false;
		}
	}
}
