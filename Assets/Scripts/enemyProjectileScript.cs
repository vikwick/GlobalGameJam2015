using UnityEngine;
using System.Collections;

public class enemyProjectileScript : MonoBehaviour {
    GameObject player;
    OGChickenController _player;
    GameObject enemy;
    EnemyController _enemy;
    Animator anim;
    public int maxProjSpeed = 0;
    float rotationangle = 0f;
    // Use this for initialization
    void Start () {
        enemy = GameObject.FindGameObjectWithTag("Enemy");

		if(enemy!= null){ 
			_enemy = enemy.GetComponent<EnemyController>();
			if(_enemy!=null){
        anim = GetComponent<Animator>();
        Vector2 projVector = new Vector2 (_enemy.attackpath.x , _enemy.attackpath.y );
        projVector.Normalize();
        rotationangle = Mathf.Rad2Deg*Mathf.Atan2(_enemy.attackpath.y,_enemy.attackpath.x );
        transform.rotation = Quaternion.Euler (new Vector3(0, 0, rotationangle));
        rigidbody2D.velocity = maxProjSpeed * projVector;
        }
		}
	}

    void OnTriggerExit2D(Collider2D b){
        if(b.gameObject.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            _player = player.GetComponent<OGChickenController>();
            _player.currentHP -= 5;
        } 
        if (b.gameObject.tag != "Enemy"){
            Destroy(gameObject);
        }  

    }
    // Update is called once per frame
    void Update () {
    
    }
}
