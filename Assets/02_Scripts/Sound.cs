using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
	public	string		name;

	public	AudioClip	clip;

	public	bool		isMusic = false;

	public	bool		isSFX = false;

	public	bool		loop = false;

	[Range(0f, 1f)]
	public	float		volume = 0.5f;

	[Range(0.1f, 3f)]
	public	float		pitch = 1f;

	[HideInInspector]
	public	AudioSource	sourse;


}
