using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public int ATK;
	public int HP;
	public int SPD;
	public int RNG;
	GameObject p;

	// Use this for initialization
	void Start () {
		p = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollision2DEnter(Collision2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			// do damage
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
	}

	void attack()
	{
		// set attack bool to change animation

	}
}
