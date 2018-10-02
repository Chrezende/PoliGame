using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbUpStairs : MonoBehaviour {

    public PlayerController playerController;
    public ClimbDownStairs stairsUp;

    public bool playerNear = false;
	// Use this for initialization
	void Start () {
		playerController = FindObjectOfType<PlayerController>();
        stairsUp = FindObjectOfType<ClimbDownStairs>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Interact();
        }
	}

    void OnTriggerEnter2D (Collider2D col){
      if (col.GetComponent<PlayerController>())
      {
        playerNear = true;
        Debug.Log("Escada! = " + playerNear); 
      }

    }

    void OnTriggerExit2D (Collider2D col){
      if (col.GetComponent<PlayerController>())
      {
        playerNear = false;
        Debug.Log("Escada! = " + playerNear); 
      }

    }

    public void Interact (){
        if (playerNear){
            Debug.Log("Teve interação");
            playerController.transform.position = stairsUp.transform.position;
        } else
        {
            Debug.Log("Tentando descer, mas não conseguindo.");
        }
    }
}
