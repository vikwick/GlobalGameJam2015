using UnityEngine;
using System.Collections;
using System;

public class OGChickenController : MonoBehaviour {

	Coroutine runningCoroutine;
	public float maxSpeed = 10f;
	public int maxProjSpeed = 10;
	public int HP = 100;
	public int infectedLevel = 0;
	public int ATK = 5;
	public int currentHP = 100;
	private float idleTime = 0f;
	public Vector2 OGChickenVec;
	GameObject projectile;
	public string projName;
	public bool ATKUp = false;
	public bool ATKSPDUp = false;
	public bool SPDUp = false;
	bool timesUp = false;
	GameObject enemy;
	EnemyController _enemy;
	Animator anim;
	public float attackWait = 0.5f;
	bool tab = false;
	
	void Start () {
		anim = GetComponent<Animator>();
		//            enemy = GameObject.FindGameObjectWithTag("Enemy");
		//            _enemy = enemy.GetComponent<EnemyController>();
	}
	
	/*
	 * Necessary so that projectile isn't null for the first call to FixedUpdate
	 */
	void OnEnable()
	{
		setProjectile("Beak");
	}
	
	// Update is called once per frame
	void Update ()
	{
		Move();
	}

	void Move()
	{
		OGChickenVec = new Vector2 ((maxSpeed * Input.GetAxis("Horizontal")), maxSpeed * Input.GetAxis("Vertical"));
		rigidbody2D.velocity = OGChickenVec;
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		anim.SetFloat("Speed", rigidbody2D.velocity.x);
	}
	void FixedUpdate ()
	{
		attackWait += Time.deltaTime;
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
//		if (Input.GetButtonDown("Fire1"))
//		{
//			Debug.Log(Input.GetButtonDown("Fire1"));
//		}
//		if(Input.GetKey("space") && attackWait >= 0.5f && !runningCoroutine){
//			//            anim.SetBool("attacking", true);
//			//            GameObject beak = Instantiate(Resources.Load (trueproj, typeof(GameObject)), new Vector2(0,transform.position.y), transform.rotation) as GameObject;
//			//            attackWait = 0f;
//			StartCoroutine(fire ());
//			
//		}

		bool f = false;

		if(Input.GetKey(KeyCode.UpArrow) && runningCoroutine==null)
		{
			ProjectileScript.yv = Mathf.Abs(Input.GetAxis("Vertical"))+1;
			ProjectileScript.rotationangle = 90f;
			f = true;
		}			
		if(Input.GetKey(KeyCode.DownArrow) && runningCoroutine==null)
		{
			ProjectileScript.yv = -1*(Mathf.Abs(Input.GetAxis("Vertical"))+1);
			ProjectileScript.rotationangle = -90f;
			f = true;
		}
		if(Input.GetKey(KeyCode.LeftArrow) && runningCoroutine==null)
		{
			ProjectileScript.xv = -1;
			ProjectileScript.rotationangle = 180f;
			f = true;
		}			
		if(Input.GetKey(KeyCode.RightArrow) && runningCoroutine==null)
		{
			ProjectileScript.xv = 1;
			ProjectileScript.rotationangle = 0f;
			f = true;
		}

		if(f && runningCoroutine==null)
			runningCoroutine = StartCoroutine(Fire());
		else
		{
			anim.SetBool("attacking", false);
			ProjectileScript.yv = 0f;
			ProjectileScript.xv = 0f;
			f = false;
		}
	}
	
	public IEnumerator Fire()
	{
		anim.SetBool("attacking", true);
		GameObject g = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
		if (ATKUp)
		{
			g.GetComponent<SpriteRenderer>().color = Color.red;
		}
		else if (ATKSPDUp)
		{
			g.GetComponent<SpriteRenderer>().color = (new Color (0, 0, 255, 255));
			ATKUp = false;
		}
		else if (SPDUp)
		{
			g.GetComponent<SpriteRenderer>().color = Color.green;
			ATKUp = false;
			ATKSPDUp = false;
		}
		yield return new WaitForSeconds(.6f);
		runningCoroutine = null;
	}

	public void setProjectile(string p)
	{
		projName = p;
		projectile = Resources.Load("Prefabs/"+projName) as GameObject;
	}
	
	void OnGUI()
	{
		int s = 4;
		float boxSizeInfected = (Screen.width / s) / (100f/infectedLevel+1);
		float boxSizeHP = Screen.width / s/ ((float)HP/(currentHP+1));
		GUI.color = Color.black;
		GUI.Box(new Rect(10, 10, Screen.width / s, 20), "HP:" + currentHP + "/" + HP);
		GUI.Box(new Rect(10, 30, Screen.width / s, 20), "Infection:" + infectedLevel + "%");
		if ((int)currentHP > 3)
		{
			GUI.color = Color.green;
			GUI.Button(new Rect(10, 10, boxSizeHP, 20), "");
		}
		if ((int)infectedLevel > 3)
		{
			GUI.color = Color.magenta;
			GUI.Button(new Rect (10, 30, boxSizeInfected, 20), "");
		}
		if(tab)
		{

		}
	}
}
