using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorSound : MonoBehaviour
{

	private AudioSource[] audios;
	private AudioClip[,] clips;
	public int stamp;
	public static MajorSound instance;
	
	//---------INPUT----------
	public float time_jump;
	public float time_fall;
	public float time_slide;
	public float time_punch;
	//-----TEST------------
	public bool test;
	
	// Use this for initialization
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	void Start ()
	{
		stamp = 0;
		audios = GetComponents<AudioSource>();
		clips = new AudioClip[,]
		{
			{
				Resources.Load<AudioClip>("Sound/BackgroundMusic/music_slow"),Resources.Load<AudioClip>("Sound/BackgroundMusic/music_fast"),
				null,null,null,null,null,null,null,Resources.Load<AudioClip>("Sound/BackgroundMusic/win"),
				null,null,null,Resources.Load<AudioClip>("Sound/BackgroundMusic/lose")
			},
			{null,null,null,null,null,null,null,Resources.Load<AudioClip>("Sound/BackgroundSound/rushbegin"),null,
				null,
				null,null,null,null},
			{null,Resources.Load<AudioClip>("Sound/BoyDialog/jump"),null,Resources.Load<AudioClip>("Sound/BoyDialog/fall"),
				Resources.Load<AudioClip>("Sound/BoyDialog/getup"),Resources.Load<AudioClip>("Sound/BoyDialog/slide"),null,
				Resources.Load<AudioClip>("Sound/BoyDialog/rushbegin"),Resources.Load<AudioClip>("Sound/BoyDialog/rushend"),
				Resources.Load<AudioClip>("Sound/BoyDialog/win"),Resources.Load<AudioClip>("Sound/BoyDialog/hurt"),
				Resources.Load<AudioClip>("Sound/BoyDialog/punch"),Resources.Load<AudioClip>("Sound/BoyDialog/armor"),
				Resources.Load<AudioClip>("Sound/BoyDialog/lose")},
			{Resources.Load<AudioClip>("Sound/BoySound/run"),Resources.Load<AudioClip>("Sound/BoySound/jump"),
				null,Resources.Load<AudioClip>("Sound/BoySound/fall"),null,
				Resources.Load<AudioClip>("Sound/BoySound/slide"),Resources.Load<AudioClip>("Sound/BoySound/candyget"),null,null,
				null,null,Resources.Load<AudioClip>("Sound/BoySound/punch"),null,null}
		};
		checknullnumber();
		
		play(0,0,true);
		
		play(3,0,true);
	}
	
	// Update is called once per frame
	void Update () {
		if (test)
		{
			testfunction();
		}
	}

	public void rush_audio()
	{
		float time = audios[0].time;
		time = time / 100 * 63;
		audios[0].clip = clips[0,1];
		audios[0].time = time;
		audios[0].Play();
	}

	public void rushstop_audio()
	{
		float time = audios[0].time;
		time = time / 63 * 100;
		audios[0].clip = clips[0,0];
		audios[0].time = time;
		audios[0].Play();
	}

	public void jump_audio()
	{	
		play(2,1,false);
		play(3,1,false);
		stamp++;
		StartCoroutine(delayplay(time_jump,3,0,true,stamp));
	}

	

	public void fall_audio()
	{
		play(2,3,false);
		play(3, 3, false);
		stamp++;
		StartCoroutine(delayplay(time_fall, 3, 0, true, stamp));
		StartCoroutine(delayplay(time_fall, 2, 4, false, stamp));
	}

	public void slide_audio()
	{
		play(2, 5, false);
		play(3, 5, false);
		stamp++;
		StartCoroutine(delayplay(time_slide, 3, 0, true, stamp));
	}

	public void candyget_audio()
	{
		play(3, 6, false);
		play(2,7,false);
		play(1,7,false);
		stamp++;
		//---------next line should be rush sound
		StartCoroutine(delayplay(clips[3, 6].length, 3, 0, true, stamp));
		//StartCoroutine(delayplay(clips[1, 7].length, 1, 10, false, -1));
	}

	public void rushend_audio()
	{
		play(2, 8, false);
		play(3,0,true);
		stamp++;
	}

	public void punch_audio()
	{
		play(2,11,false);
		play(3, 11, false);
		stamp++;
		StartCoroutine(delayplay(time_punch, 3, 1, true, stamp));
	}

	public void dodge_audio()
	{
		play(2, 12, false);
		stamp++;
	}

	public void lose_audio()
	{
		play(2,13,false);
		play(0,13,true);
		audios[3].Pause();
	}

	public void win_audio()
	{
		play(0,9,true);
		play(2,9,false);
		audios[3].Pause();

	}

	public void testfunction()
	{
		test = false;
		rush_audio();
	}

	private void checknullnumber()
	{
		int num = 0;
		for (int i = 2; i < 3; i++)
		{
			for (int j = 0; j < 14; j++)
			{
				if (clips[i, j] == null) num++;
			}
		}
		print(num);
	}

	

	IEnumerator delayplay(float time_wait, int audio, int clip, bool loop,int stamp_local)
	{
		yield return new WaitForSecondsRealtime(time_wait);
		if(stamp_local==stamp)
			play(audio,clip,loop);

	}
	
	IEnumerator delayplayplay(float time_wait, int audio, int clip1, int clip2,bool loop2,int stamp_local)
	{
		yield return new WaitForSecondsRealtime(time_wait);
		if (stamp_local==stamp || stamp_local==-1)
		{
			play(audio, clip1, false);
			print(clips[audio, clip1].length);

			yield return new WaitForSecondsRealtime(clips[audio, clip1].length);
			if (stamp_local == stamp || stamp_local == -1)
			{
				play(audio, clip2, loop2);
			}
				
		}
	}
	
	

	private void play(int audio, int clip, bool loop)
	{
		audios[audio].clip = clips[audio, clip];
		audios[audio].loop = loop;
		audios[audio].Play();
	}
}
