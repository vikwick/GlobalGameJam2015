using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public int ATK;
	public int HP;
	public int SPD;
	public int RNG;
	public Vector3 attackpath;
	GameObject p;
	OGChickenController _player;
	Animator anim;


	// Use this for initialization
	void Start () {
		p = GameObject.FindGameObjectWithTag("Player");
        _player = p.GetComponent<OGChickenController>();
        anim = GetComponent<Animator>();
		this.SPD = 1;
		this.RNG = 5;
		this.HP = 10;
	}
	
	// Update is called once per frame
	void Update () {
		this.kill();
	}



	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			_player.currentHP -= 10f;
		}	
	}

	void kill()
	{
		Vector3 pos = p.transform.position;
		rigidbody2D.AddForce((pos - transform.position)*SPD);
		if(pos.magnitude < RNG) // player in range
		{
			attack();
		}
		else{
			anim.SetBool("attack", false);
		}
		attackpath = (pos - transform.position);
	}

	void attack()
	{
		this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		rigidbody2D.velocity = new Vector2(0,0);
		transform.position = transform.position;
		anim.SetBool("attack",true);
		//(pos - transform.position)

	}
}
