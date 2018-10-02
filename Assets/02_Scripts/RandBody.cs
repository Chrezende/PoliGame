using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandBody : MonoBehaviour {

    public Sprite[] body;
	// Use this for initialization
	void Start () {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = body[Random.Range(0, body.Length)];
	}
}
