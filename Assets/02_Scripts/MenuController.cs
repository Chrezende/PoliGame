using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	static bool tut = false;

	private SceneController sc;

	public GameObject painel;

	public GameObject tutorial;

	void Awake(){
		
		if (tut == false)
		{
			sc = FindObjectOfType<SceneController> ();
			tutorial.SetActive (true); 
			tut = true;
			sc.PauseResume ();
		}
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) {
			painel.SetActive (!painel.activeSelf);
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			Debug.Log ("Uai");
			sc.PauseResume ();
		}
	}
}
