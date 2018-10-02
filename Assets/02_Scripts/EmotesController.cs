using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotesController : MonoBehaviour {

    public Sprite[] baloes; 
    public SpriteRenderer spriteRenderer;

    // Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowEmote(int emote){
        if (emote == 0){
            spriteRenderer.sprite = null;
        }
        spriteRenderer.sprite = baloes[emote];
    }
}
