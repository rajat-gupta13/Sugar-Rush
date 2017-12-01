using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

	//------------INPUT----------
	public float edge_credit;
	public float speed_credit;
	//---------STATIC-------------
	public static UI instance;

	//---------PRIVATE---------------
	private Image[] images;
	
	//----------TEST---------------
	public bool clean;


	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	//------PRIVATE------------
	// Use this for initialization
	void Start()
	{
		images = GetComponentsInChildren<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		if (clean)
		{
			clear();
		}
	}

	public void setlife(int lifenum, int lifemax)
	{
		for (int i = 1; i <= lifemax; i++)
		{
			images[i].color = Color.clear;
		}
		for (int i = 1; i <= lifenum; i++)
		{
			images[i].color = Color.white;
		}

	}

	public void showtutorial(int num)
	{
		images[0].color = Color.white;
		images[0].sprite = Instantiate(Resources.Load<Sprite>("Image/tutorial" + num.ToString()));
	}

	public void quittutorial()
	{
		images[0].color = Color.clear;
	}

	public void die()
	{
		images[0].color = Color.white;
		images[0].sprite=Instantiate(Resources.Load<Sprite>("Image/death"));
	}

	private void clear()
	{
		images[0].color = Color.clear;
	}

	public void glowing()
	{
		for (int i = 1; i < 1 + LifeSystem.instance.get_lifenum(); i++)
		{
			images[i].gameObject.GetComponent<Animator>().SetTrigger("GLOW");

		}
	}

	public void stop_glowing()
	{
		for (int i = 1; i < 1 + LifeSystem.instance.num_life_max; i++)
		{
			images[i].gameObject.GetComponent<Animator>().ResetTrigger("GLOW");
			images[i].gameObject.GetComponent<Animator>().SetTrigger("STOP");
		}

	}

	public IEnumerator credits()
	{
		RectTransform rect= images[4].GetComponent<RectTransform>();
		while (rect.anchoredPosition.y < edge_credit)
		{
			yield return new WaitForSeconds(0.05f);
			rect.localPosition = new Vector2(0,rect.localPosition.y+speed_credit * 0.05f);
		}
		
	}
}
