using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour{

	private SoundController sc;

	void Start()
	{
		sc = FindObjectOfType<SoundController> ();
	}

	public void OnChange(){
		sc.OnVolumeChange (FindObjectOfType<Slider> () );
	}
}
