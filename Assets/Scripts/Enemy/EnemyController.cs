using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	
	public int ATK {get;set;}
	public int HP {get;set;}
	public int SPD {get;set;}
	public int RNG {get;set;}
	public Vector3 attackpath {get;set;}
	public GameObject p {get;set;}
	public OGChickenController _player {get;set;}
	public Animator anim {get;set;}
	//GameObject head;
	public GameObject projectile {get;set;}
	
	public Coroutine runningCoroutine {get;set;}



	// Use this for initialization
	void Start () {
		this.setSprite(this.getSpriteString());
		this.anim = GetComponent<Animator>();
		this.p = GameObject.FindGameObjectWithTag("Player");
        this._player = p.GetComponent<OGChickenController>();
		this.setStats(1, 5, 10);
		this.setProjectile(this.getSpriteString());

	}

	public virtual string getProjectileString(){
		return "Prefabs/Head1";
	}

	public virtual void setProjectile(string s){
		if(s!=null){
			Debug.Log (s);
			//this.head = Resources.Load (s) as GameObject;
			this.projectile = (GameObject)(Resources.Load (s) as GameObject);
		}else{
			this.projectile = null;
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
		if (this.HP <= (int)0){
				Destroy (gameObject);
			}
	}


	void FixedUpdate(){
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		anim.SetFloat("Speed", rigidbody2D.velocity.x);
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
		
		GameObject head = Instantiate(Resources.Load ("Prefabs/Head1")as GameObject, transform.position + new Vector3(1,-2, 0), transform.rotation) as GameObject;
		head.transform.parent = this.transform;
		head.AddComponent<BoxCollider2D>().isTrigger = true;


		yield return new WaitForSeconds(2);
	
		this.runningCoroutine = null;
		Destroy(head);
	}


}
