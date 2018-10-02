using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandDress : MonoBehaviour {

    public Sprite[] dress;
    // Use this for initialization
    void Start () {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = dress[Random.Range(0, dress.Length)];
    }
}
