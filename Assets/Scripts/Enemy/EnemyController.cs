using UnityEngine;
using System.Collections;

public abstract class EnemyController : MonoBehaviour {
	
	public int ATK;
	public int ATKSpd;
	public int HP;
	public int SPD;
	public int RNG;
	public Vector3 attackpath;
	public Animator anim;
	public GameObject projectile;
	public Coroutine runningCoroutine;
	public Room r;
	public string spriteName;
	public string projName;

	protected virtual void Init(string sName, string pName)
	{
		spriteName = sName;
		projName = pName;
		anim = GetComponent<Animator>();
		projectile = Resources.Load ("Prefabs/"+projName) as GameObject;
		setSprite(spriteName);
	}

	public virtual void setStats(int atk, int atkspd, int hp, int spd, int rng)
	{
		ATK = atk;
		ATKSpd = atkspd;
		HP = hp;
		SPD = spd;
		RNG = rng;
	}

	public virtual void setSprite(string s)
	{
		GetComponent<SpriteRenderer>().sprite = (Sprite)(Resources.Load("Sprites/"+s) as Sprite);
		GetComponent<SpriteRenderer>().sortingOrder = 1;
	}

	void Update ()
	{
		if (HP <= 0)
		{
			r.enemies.Remove(gameObject);
			Destroy (gameObject);
		}
		Move();
		Kill();
	}


	void FixedUpdate()
	{
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		anim.SetFloat("Speed", rigidbody2D.velocity.x);
	}

	public virtual void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			GameManager._player.currentHP -= 10;
		}
	}

	void OnTriggerExit2D(Collider2D b)
	{
		if(b.gameObject.tag == "PlayerProjectile")
		{
			HP -= GameManager._player.ATK;
		}
	}

	public virtual void Move()
	{
		rigidbody2D.velocity = (GameManager.player.transform.position - transform.position)*SPD;
	}

	public virtual void Kill()
	{
		if(GameManager.player!= null)
		{
			attackpath = GameManager.player.transform.position - transform.position;
			if(attackpath.magnitude < RNG )
			{
				if(runningCoroutine==null)
				{
					runningCoroutine = StartCoroutine(Attack());
				}
			}
//			else
//			{
//				anim.SetBool("attack", false);
//			}
		}
	}

	public virtual IEnumerator Attack()
	{
		anim.SetBool("attack",true);
		GetComponent<SpriteRenderer>().color = Color.red;
		rigidbody2D.velocity = new Vector2(0,0);
		GameObject proj = Instantiate(projectile, transform.position + new Vector3(1,-2, 0), transform.rotation) as GameObject;
		proj.transform.parent = transform;
		yield return new WaitForSeconds(2);
		this.runningCoroutine = null;
		anim.SetBool("attack",false);
	}
}
