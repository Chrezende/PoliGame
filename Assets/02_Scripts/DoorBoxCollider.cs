using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBoxCollider : MonoBehaviour {

    public bool playerNear = false;


    void OnTriggerEnter2D (Collider2D col){
        playerNear = true;
        Debug.Log("Porta! = " + playerNear);

    }

    void OnTriggerExit2D (Collider2D col){
        playerNear = false;
        Debug.Log("Porta! = " + playerNear);

    }
}
