using UnityEngine;
using System.Collections;

public class EnemyProjectileScript : MonoBehaviour {
    GameObject player;
    OGChickenController _player;
    GameObject enemy;
    public EnemyController _enemy;
    Animator anim;
    int maxProjSpeed = 10;
    float rotationangle = 0f;
    // Use this for initialization
    void Start () {
//        enemy = GameObject.FindGameObjectWithTag("Enemy");
//
//		if(enemy!= null){ 
//			_enemy = enemy.GetComponent<EnemyController>();
//			if(_enemy!=null){
        anim = GetComponent<Animator>();
		Vector2 projVector = GameManager.player.transform.position - transform.position;
        projVector.Normalize();
        rotationangle = Mathf.Rad2Deg*Mathf.Atan2(projVector.y,projVector.x );
        transform.rotation = Quaternion.Euler (new Vector3(0, 0, rotationangle));
		rigidbody2D.velocity = maxProjSpeed * projVector;
	}

    void OnTriggerExit2D(Collider2D b){
		Debug.Log (b.gameObject.tag);
        if(b.gameObject.tag == "Player")
        {
			GameManager.player.GetComponent<OGChickenController>().currentHP -= 5;
        } 
		if (b.gameObject.tag != "Enemy" && b.gameObject.tag != "PlayerProjectile" && b.gameObject.tag != "EnemyProjectile"){
			Destroy(gameObject);
        }  

    }
    // Update is called once per frame
    void Update () {
    
    }
}
