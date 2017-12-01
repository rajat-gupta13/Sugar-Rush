using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA;

public class Zombie : MonoBehaviour
{

	public float distance_teletranslate;
	public float time_turn;
	public float upperbound;
	public float lowerbound;

	public bool lefting;

	private Animator anim;
	//-------TEST---------
	public float positive;
	// Use this for initialization
	void Start ()
	{
		lefting = false;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!lefting && transform.position.x > upperbound)
		{
			StartCoroutine(turnleft());
		}
		if (lefting && transform.position.x < lowerbound)
		{
			StartCoroutine(turnright());
		}
	}

	public void teletranslate()
	{
		transform.Translate(transform.forward*distance_teletranslate,Space.World);
	}

	private IEnumerator turnleft()
	{
		anim.enabled = false;
		while (transformtopositive(transform.rotation.eulerAngles.y)>89 && transformtopositive(transform.rotation.eulerAngles.y)<270)
		{
			yield return new WaitForSecondsRealtime(0.05f);
			transform.Rotate(transform.up,0.6f,Space.World);
		}
		transform.localEulerAngles = new Vector3(0, 270, 0);
		lefting = true;
		anim.enabled = true;
	}

	private IEnumerator turnright()
	{
		anim.enabled = false;
		while (transformtopositive(transform.rotation.eulerAngles.y)>90 && transformtopositive( transform.rotation.eulerAngles.y)<271)
		{
			yield return new WaitForSecondsRealtime(0.05f);
			transform.Rotate(transform.up,-0.6f,Space.World);
		}
	
		transform.localEulerAngles = new Vector3(0, 90, 0);
		lefting = false;
		anim.enabled = true;
	}

	private float transformtopositive(float original)
	{
		positive = (original + 360) % 360;
		return positive;
	}
}
