using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public Sprite[] doorSprites;
    public BoxCollider2D thisBoxCollider;
    public SpriteRenderer spriteRenderer;
    public DoorBoxCollider doorCollider;
    public SoundController soundController;


    public bool doorOpen = false;
	// Use this for initialization
	
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        thisBoxCollider = GetComponent<BoxCollider2D>();
        doorCollider = GetComponentInChildren<DoorBoxCollider>();
        soundController = FindObjectOfType<SoundController>();
	}
	
/*    void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Interact();
        }
    }*/
    void OnCollisionEnter2D (Collision2D col){
        if (doorCollider.playerNear == true){
            Debug.Log("Usando porta");
            DoorInteract();
        } else
        {
            Debug.Log("Tentando usar a porta, mas não conseguindo.");
        }
    }

    void OnTriggerEnter2D (Collider2D col){
        if (col.GetComponent<GF>())
        {
            if (doorCollider.playerNear == true)
            {
                Debug.Log("Usando porta");
                DoorInteract();
            }
            else
            {
                Debug.Log("Tentando usar a porta, mas não conseguindo.");
            }
        }
    }

    void OnTriggerExit2D (Collider2D col){
        if (doorCollider.playerNear == true){
            Debug.Log("Usando porta");
            DoorInteract();
        } else
        {
            Debug.Log("Tentando usar a porta, mas não conseguindo.");
        }

    }


/*    public void Interact (){
        if (doorCollider.playerNear == true){
            Debug.Log("Usando porta");
            DoorInteract();
        } else
        {
            Debug.Log("Tentando usar a porta, mas não conseguindo.");
        }
    }*/

    public void DoorInteract(){
        if (!doorOpen){
            soundController.Play("OpenDoor");
            spriteRenderer.sprite = doorSprites[1];
            thisBoxCollider.isTrigger = true;
            doorOpen = true;

        } else{
            soundController.Play("ClosingDoor");
            spriteRenderer.sprite = doorSprites[0];
            thisBoxCollider.isTrigger = false;
            doorOpen = false;

        }

    }

}
