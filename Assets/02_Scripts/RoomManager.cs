using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    /*public enum RoomName {Bedroom,Corridor2,Bathroom,Kitchen,Corridor1,Livingroom}
    public RoomName roomName;*/

    private GFManager gfManager;

    public int countGF = 0;
	// Use this for initialization
	void Start () {
        //roomName = RoomName.Livingroom;
        gfManager = FindObjectOfType<GFManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (countGF > 1){
            gfManager.GameOver();
        }
	}

    void OnTriggerEnter2D (Collider2D col){
        if (col.GetComponent<GF>())
        {
            //col.GetComponent<GF>().atRoom = roomName;
            countGF++;
        }
    }

    void OnTriggerExit2D (Collider2D col){
        if (col.GetComponent<GF>())
        {
            countGF--;
        }
    }
}
