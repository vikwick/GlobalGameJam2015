using UnityEngine;
using System.Collections;

public abstract class EnemyController : MonoBehaviour {
	
	public int ATK;
	public int ATKSpd;
	public int HP;
	public int MaxHP;
	public int SPD;
	public int RNG;
	public Vector3 attackpath;
	public Animator anim;
	public GameObject projectile;
	public Coroutine runningCoroutine;
	public Room r;
	public string projName;

	protected virtual void Init(string sName, string pName)
	{
		if(pName != "")
			projName = pName;
			projectile = Resources.Load ("Prefabs/"+projName) as GameObject;
		anim = GetComponent<Animator>();
		setSprite(sName);
	}

	void Update ()
	{
		if (HP <= 0)
		{
			r.enemies.Remove(gameObject);
			Destroy (gameObject);
		}
		if(projName != null && runningCoroutine != null)
		{
			Move();
		}
		Kill();
	}

	public virtual void setStats(int atk, int atkspd, int hp, int spd, int rng)
	{
		ATK = atk;
		ATKSpd = atkspd;
		HP = hp;
		MaxHP = hp;
		SPD = spd;
		RNG = rng;
	}

	public virtual void setSprite(string s)
	{
		GetComponent<SpriteRenderer>().sprite = (Sprite)(Resources.Load("Sprites/"+s) as Sprite);
		GetComponent<SpriteRenderer>().sortingOrder = 1;
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
			GameManager._player.HP -= ATK;
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
		}
	}

	public virtual IEnumerator Attack()
	{
		anim.SetBool("attack",true);
		GetComponent<SpriteRenderer>().color = Color.red;
		rigidbody2D.velocity = new Vector2(0,0);
		if(projectile!=null)
		{
			GameObject proj = Instantiate(projectile, transform.position + new Vector3(1,-2, 0), transform.rotation) as GameObject;
			proj.transform.parent = transform;
		}
		yield return new WaitForSeconds(ATKSpd);
		this.runningCoroutine = null;
		anim.SetBool("attack",false);
	}

	public void reset()
	{
		HP = MaxHP;
	}
}
