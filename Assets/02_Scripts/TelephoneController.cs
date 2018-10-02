using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneController : MonoBehaviour {

    public Sprite[] telSprites;
    public SpriteRenderer spriteRenderer;
    public SoundController soundController;
    public GF[] gf;

    public bool isRinging = false;
    private bool playingSound = false;
    private bool playerNear = false;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundController = FindObjectOfType<SoundController>();
	}
	
	// Update is called once per frame
    void Update () {
        gf = FindObjectsOfType<GF>();
        if (isRinging)
        {
            if (!playingSound)
            {
                soundController.Play("PhoneRing");
                playingSound = true;
            }
            spriteRenderer.sprite = telSprites[1];
        }else {
            soundController.Stop("PhoneRing");
            spriteRenderer.sprite = telSprites[0];
//            foreach (var girl in gf)
//            {
//                if (girl.Fazer_Algo == 3){
//                    girl.emotes.ShowEmote((9));
//                    girl.startMarker = girl.transform.position;
//                    girl.GF_Want2Go(girl.transform.position,girl.oldStartMarker,girl.oldAndar);
//                }
//            }
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))&& playerNear)
        {
            Interact();
        }
    }
        
    void OnDestroy(){
        soundController.Stop("PhoneRing");
    }

    void Interact (){
        isRinging = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
        {
            playerNear = true;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
        {
            playerNear = false;
        }

    }
}
