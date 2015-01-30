using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour {

	void Update()
	{
		if(transform.parent!=null && GameManager.player!=null)
		{
			Vector3 parentPos = transform.parent.transform.position;
			Vector3 playerPos = GameManager.player.transform.position;
			Vector3 u = (playerPos - parentPos).normalized;
			this.rigidbody2D.AddForce(new Vector2(u.x, u.y)*50f);
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			GameManager._player.currentHP -= 15;
		}
		if(c.gameObject.tag != "Enemy" && c.gameObject.tag != "EnemyProjectile")
		{
			Destroy(gameObject);
		}
	}
}