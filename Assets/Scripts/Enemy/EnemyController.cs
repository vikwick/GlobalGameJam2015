using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public int ATK;
	public int HP;
	public int SPD;
	public int RNG;
	public Vector3 attackpath;
	public GameObject p;
	public OGChickenController _player;
	public Animator anim;
	//GameObject head;
	public GameObject projectile;
	
	public Coroutine runningCoroutine;


	// Use this for initialization
	void Start () {
		this.setSprite(this.getSpriteString());
		anim = GetComponent<Animator>();
		p = GameObject.FindGameObjectWithTag("Player");
        _player = p.GetComponent<OGChickenController>();
		this.setStats(1, 5, 10);
		this.setProjectile(this.getSpriteString());

	}

	public virtual string getProjectileString(){
		return "Prefabs/Head1";
	}

	public virtual void setProjectile(string s){
		if(s!=null){
			//this.head = Resources.Load (s) as GameObject;
			this.projectile = Resources.Load (s) as GameObject;
		}
	}

	public virtual void setStats(int x, int y, int z){
		this.SPD = x;
		this.RNG = y;
		this.HP = z;
	}

	public virtual string getSpriteString(){
		return "Sprites/BusinessMan/Businessman1";
	}

	public virtual void setSprite(string s){
		this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)(Resources.Load(s) as Sprite);
		this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
	}
	
	// Update is called once per frame
	void Update () {


		this.kill();
	}

	void FixedUpdate(){
		//anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		//anim.SetFloat("Speed", rigidbody2D.velocity.x);
	}

	public virtual void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			_player.currentHP -= 10f;
		}

		if(c.gameObject.tag == "beak")
		{
			this.HP -= 10;
		}	
	}



	public virtual void kill()
	{
		if(p!= null){
			Vector3 pos = p.transform.position;
			rigidbody2D.AddForce((pos - transform.position)*SPD);
			if(pos.magnitude < RNG ) // player in range
			{	//this.attacking = true;

				if(this.runningCoroutine==null){
					this.runningCoroutine = StartCoroutine(this.Attack());
				}


			}
			else{
				anim.SetBool("attack", false);


			}
			attackpath = (pos - transform.position);
		}
	}

	public virtual IEnumerator Attack() {
		anim.SetBool("attack",true);
		//attack();
		this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		rigidbody2D.velocity = new Vector2(0,0);
		
		
		//(pos - transform.position)
		
		GameObject head = Instantiate(this.projectile, transform.position + new Vector3(1,-2, 0), transform.rotation) as GameObject;
		head.transform.parent = this.transform;
		head.AddComponent<BoxCollider2D>().isTrigger = true;

		Debug.Log(Time.time);
		yield return new WaitForSeconds(2);
		Debug.Log(Time.time);
		this.runningCoroutine = null;
		Destroy(head);
	}


}
