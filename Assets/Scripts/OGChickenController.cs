using UnityEngine;
using System.Collections;
using System;

public class OGChickenController : MonoBehaviour {
    bool facingLeft = true;
    public float maxSpeed = 10f;
    public float HP = 100f;
    public float infectedLevel = 0f;
    public string ability = "hello";
    public float ATK = 5f;

    Animator anim;
    // Use this for initialization
    void Start () {
            anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {

    }

    void FixedUpdate () {

        anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

        rigidbody2D.velocity = new Vector2 ((maxSpeed * Input.GetAxis("Horizontal")), maxSpeed * Input.GetAxis("Vertical"));
        anim.SetFloat("Speed", Math.Abs(rigidbody2D.velocity.x));
        
        if (rigidbody2D.velocity.x < 0.0 && !facingLeft){
            Flip();
            }
        else if (rigidbody2D.velocity.x > 0.0 && facingLeft){
            Flip();
            }
    }
    void Flip () {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;
        transform.localScale = theScale;
        facingLeft = !facingLeft;
    }
}
