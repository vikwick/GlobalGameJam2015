using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {
    GameObject p;
    OGChickenController _player;
    GameObject enemy;
    EnemyController _enemy;
    Animator anim;
    float rotationangle = 0f;
	// Use this for initialization
	void Start () {
        p = GameObject.FindGameObjectWithTag("Player");
        _player = p.GetComponent<OGChickenController>();
        anim = GetComponent<Animator>();
        Vector2 projVector = new Vector2 (_player.OGChickenVec.x , _player.OGChickenVec.y );
        projVector.Normalize();
        if (_player.OGChickenVec.x == 0 && _player.OGChickenVec.y == 0){
            rigidbody2D.velocity = _player.maxProjSpeed * new Vector2 (0, -1);
            transform.rotation = Quaternion.Euler (new Vector3(0, 0, -90));
        }
        else {
            rotationangle = Mathf.Rad2Deg*Mathf.Atan2(_player.OGChickenVec.y, _player.OGChickenVec.x);
            transform.rotation = Quaternion.Euler (new Vector3(0, 0, rotationangle));
            rigidbody2D.velocity = _player.maxProjSpeed * projVector;
        }

        //if (_player.OGChickenVec.x < 0 && _player.OGChickenVec.y < 0){
            //transform.rotation = Quaternion.Euler (new Vector3(0, 0, rotationangle));
        //}
        //else if (_player.OGChickenVec.x < 0 && _player.OGChickenVec.y > 0){
            //transform.rotation = new Quaternion(0, 0, rotationangle-180, 0);
        //}
        //else{
            //transform.rotation = new Quaternion(0, 0, rotationangle, 0);
        //}
	}
	void OnTriggerExit2D(Collider2D b){
        if(b.gameObject.tag == "Enemy")
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
//            enemy.GetComponent(enemy.tag).HP ;
//            _enemy.HP -= 5;
        } 
        if (b.gameObject.tag != "Player"){
            Destroy(gameObject);
        }  

    }
	// Update is called once per frame
	void Update () {
	
	}
}
