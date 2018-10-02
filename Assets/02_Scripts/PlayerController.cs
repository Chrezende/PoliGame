using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float playerSpeed = 2.0f;
    public int andarPlayer;
    public bool direita;
    public float translation;

	void Update () {
        if (this.transform.position.y >= 0.5f && this.transform.position.y <= 2f) { andarPlayer = 1;} else {andarPlayer = 2;}
        translation = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        if(Input.GetAxis("Horizontal") < 0){
            GetComponent<SpriteRenderer>().flipX = true;
            direita = false;
        } else if(Input.GetAxis("Horizontal") > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
            direita = true;
        }
        Vector2 actualPosition = new Vector2(transform.position.x, transform.position.y);
        GetComponent<Rigidbody2D>().MovePosition(actualPosition + new Vector2(translation, 0f));
        /*transform.Translate(translation,0f,0f);*/
	}
}
