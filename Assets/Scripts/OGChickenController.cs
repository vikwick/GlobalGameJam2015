using UnityEngine;
using System.Collections;
using System;

public class OGChickenController : MonoBehaviour {
    public float maxSpeed = 10f;
    public int maxProjSpeed = 10;
    public float HP = 100f;
    public float infectedLevel = 0f;
    public string ability = "hello";
    public float ATK = 5f;
    public float currentHP = 100f;
    private float idleTime = 0f;
    public Vector2 OGChickenVec;
    public string projectile;
    bool timesUp = false;
    GameObject enemy;
    EnemyController _enemy;
    Animator anim;
    public float attackWait = 0.5f;
    // Use this for initialization
    void Start () {
            anim = GetComponent<Animator>();
//            enemy = GameObject.FindGameObjectWithTag("Enemy");
//            _enemy = enemy.GetComponent<EnemyController>();
    }
    void OnEnable(){
        projectile = "beak";
    }
    
    // Update is called once per frame
    void Update () {
    }

    void FixedUpdate () {
        anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
        rigidbody2D.velocity = new Vector2 ((maxSpeed * Input.GetAxis("Horizontal")), maxSpeed * Input.GetAxis("Vertical"));
        OGChickenVec = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        anim.SetFloat("Speed", rigidbody2D.velocity.x);
        attackWait += Time.deltaTime;
        string trueproj = "Prefabs/" + projectile;
        Debug.Log (trueproj);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            idleTime+=Time.deltaTime;
        }
        else{
            idleTime = 0;
        }
        if((int)idleTime%2==0 && !timesUp){
            timesUp = true;
        }
        else if(((int)idleTime+1)%2==0 & timesUp){
            timesUp = false;
            infectedLevel += 5;
        }
        if(Input.GetKey("space") && attackWait >= 0.5f){
            anim.SetBool("attacking", true);
            GameObject beak = Instantiate(Resources.Load (trueproj, typeof(GameObject)), transform.position, transform.rotation) as GameObject;
            attackWait = 0f;
        }
        else{
            anim.SetBool("attacking", false);
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

}
