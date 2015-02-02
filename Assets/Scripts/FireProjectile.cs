using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour
{
	GameObject player;
	OGChickenController _player;
	public GameObject boss;
	ChargeBossController _boss;
	Animator anim;
	float maxProjSpeed = 10;
	float   ranX = 0f;
	float   ranY = 0f;
	float rotationangle = 0f;
	
	void OnEnable ()
	{
		_boss = boss.GetComponent<ChargeBossController>();
		ranX = Random.Range(-1f,1f);
		ranY = Random.Range(-1f,1f);
		Vector2 projVector = new Vector2 (ranX, ranY);
		projVector.Normalize();
		rotationangle = Mathf.Rad2Deg*Mathf.Atan2(ranX, ranY);
		transform.rotation = Quaternion.Euler (new Vector3(0, 0, rotationangle));
		rigidbody2D.velocity = maxProjSpeed * projVector;
	}
	
	void OnTriggerExit2D(Collider2D b)
	{
		if(b.gameObject.tag == "Player")
		{
			player = GameObject.FindGameObjectWithTag("Player");
			_player = player.GetComponent<OGChickenController>();
			_player.HP -= 30;
		} 
		if (b.gameObject.tag != "PlayerProjectile" && b.gameObject.tag != "EnemyProjectile" && b.gameObject.tag != "Boss" && b.gameObject.tag != "Enemy"){
			Destroy(gameObject);
		}  
		
	}
	
	public void setXY()
	{
		ranX = Random.Range(-1f,1f);
		ranY = Random.Range(-1f,1f);
	}
}