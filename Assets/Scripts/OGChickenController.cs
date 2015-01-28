using UnityEngine;
using System.Collections;
using System;

public class OGChickenController : MonoBehaviour {
	private bool runningCoroutine = false;
    public float maxSpeed = 10;
    public int maxProjSpeed = 10;
    public int HP = 100;
    public int infectedLevel = 0;
    public string ability = "hello";
    public int ATK = 5;
    public int currentHP = 100;
    private float idleTime = 0f;
    public Vector2 OGChickenVec;
    public string projectile;
	public bool ATKUp = false;
	public bool ATKSPDUp = false;
	public bool SPDUp = false;
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

	/*
	 * Necessary so that projectile isn't null for the first call to FixedUpdate
	 */
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

        if(Input.GetKey("space") && attackWait >= 0.5f && !runningCoroutine){
//            anim.SetBool("attacking", true);
//            GameObject beak = Instantiate(Resources.Load (trueproj, typeof(GameObject)), new Vector2(0,transform.position.y), transform.rotation) as GameObject;
//            attackWait = 0f;
			StartCoroutine(fire ());

        }
        else{
            anim.SetBool("attacking", false);
        }
    }

	public IEnumerator fire(){
		anim.SetBool("attacking", true);
		runningCoroutine = true;
		GameObject beak = Instantiate(Resources.Load ("Prefabs/" + projectile, typeof(GameObject)), transform.position, transform.rotation) as GameObject;
		if (ATKUp){
			beak.GetComponent<SpriteRenderer>().color = Color.red;
		}
		else if (ATKSPDUp){
			beak.GetComponent<SpriteRenderer>().color = (new Color (0, 0, 255, 255));
			ATKUp = false;
		}
		else if (SPDUp){
			beak.GetComponent<SpriteRenderer>().color = Color.green;
			ATKUp = false;
			ATKSPDUp = false;
		}
		yield return new WaitForSeconds(.6f);
		this.runningCoroutine = false;
	}

    void OnGUI(){
		int s = 4;
        float boxSizeInfected = (Screen.width / s) / (100f/infectedLevel+1);
        float boxSizeHP = Screen.width / s/ ((float)HP/(currentHP+1));
        GUI.color = Color.black;
        GUI.Box(new Rect(10, 10, Screen.width / s, 20), "HP:" + currentHP + "/" + HP);
        GUI.Box(new Rect(10, 30, Screen.width / s, 20), "Infection:" + infectedLevel + "%");
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
