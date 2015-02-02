using UnityEngine;
using System.Collections;

public class EnemyProjectileScript : MonoBehaviour
{
    GameObject player;
    OGChickenController _player;
    GameObject enemy;
    public EnemyController _enemy;
    Animator anim;
    int maxProjSpeed = 10;
    float rotationangle = 0f;

    void Start ()
	{
        anim = GetComponent<Animator>();
		Vector2 projVector = GameManager.player.transform.position - transform.position;
        projVector.Normalize();
        rotationangle = Mathf.Rad2Deg*Mathf.Atan2(projVector.y,projVector.x );
        transform.rotation = Quaternion.Euler (new Vector3(0, 0, rotationangle));
		rigidbody2D.velocity = maxProjSpeed * projVector;
	}

    void OnTriggerExit2D(Collider2D c)
	{
        if(c.gameObject.tag == "Player")
        {
			EnemyController _enemy = c.gameObject.GetComponent<EnemyController>();
			if(_enemy!=null)
				GameManager._player.HP -= _enemy.ATK;
        } 
		if (c.gameObject.tag != "Enemy" && c.gameObject.tag != "PlayerProjectile" && c.gameObject.tag != "EnemyProjectile"){
			Destroy(gameObject);
        }
    }
}
