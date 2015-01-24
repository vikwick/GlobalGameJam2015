using UnityEngine;
using System.Collections;
using System;

public class OGChickenController : MonoBehaviour {
    public float maxSpeed = 10f;
    public float HP = 100f;
    public float infectedLevel = 0f;
    public string ability = "hello";
    public float ATK = 5f;
    public float currentHP = 100f;
    private float idleTime = 0f;
    bool timesUp = false;
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
        anim.SetFloat("Speed", rigidbody2D.velocity.x);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            idleTime+=Time.deltaTime;
        }
        else{
            idleTime = 0;
        }
        if((int)idleTime%3==0 && !timesUp){
            timesUp = true;
        }
        else if(((int)idleTime+1)%3==0 & timesUp){
            timesUp = false;
            infectedLevel += 5;
        }
        if(Input.GetButtonDown("attack")){
            anim.SetTrigger("attacking");
        }

    }

    void OnGUI(){
        float boxSizeInfected = (Screen.width / 2) / (100/infectedLevel);
        float boxSizeHP = Screen.width / 2 / (HP/(currentHP+.1f));
        GUI.color = Color.black;
        GUI.Box(new Rect(10, 10, Screen.width / 2, 20), "HP:" + currentHP + "/" + HP);
        GUI.Box(new Rect(10, 30, Screen.width / 2, 20), "Infection:" + infectedLevel + "%");
        if ((int)currentHP > 3){
            GUI.color = Color.green;
            GUI.Button(new Rect(10, 10, boxSizeHP, 20), "");
        }
        if ((int)infectedLevel > 3){
            GUI.color = Color.magenta;
            GUI.Button(new Rect (10, 30, boxSizeInfected, 20), "");
        }
    }

    void OnCollisionEnter2D(Collision2D c){
        Debug.Log(c.gameObject.tag);
        if (c.gameObject.tag == "Enemy" && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
            currentHP -= 10f;
        }
    }
}
