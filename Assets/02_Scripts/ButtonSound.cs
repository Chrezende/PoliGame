using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSound : MonoBehaviour {

	private SoundController sc;

	void Start()
	{
		sc = FindObjectOfType<SoundController> ();
	}

	public void Ping()
	{
		sc.Play("Ping");
	}
}
