using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : MonoBehaviour {

    public Sprite[] tvSprites;
    public SpriteRenderer spriteRenderer;

    public bool tvOn = false;
    public bool playerNear = false;

    // Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Interact();
        }
    }
	
    void OnTriggerEnter2D (Collider2D col){
        playerNear = true;
        Debug.Log("TV! = " + playerNear);

    }

    void OnTriggerExit2D (Collider2D col){
        playerNear = false;
        Debug.Log("TV! = " + playerNear);

    }

    public void Interact (){
        if (playerNear){
            if (!tvOn)
            {
                Debug.Log("Ligou a TV");
                spriteRenderer.sprite = tvSprites[1];
                tvOn = true;
            }else {
                Debug.Log("Desligou a TV");
                spriteRenderer.sprite = tvSprites[0];
                tvOn = false;
            }
        } else
        {
            Debug.Log("Tentando usar a TV, mas não conseguindo.");
        }
    }
}
