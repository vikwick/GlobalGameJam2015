using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {
    GameObject p;
    OGChickenController _player;
    Animator anim;
    float rotationangle = 0f;
	// Use this for initialization
	void Start () {
        p = GameObject.FindGameObjectWithTag("Player");
        _player = p.GetComponent<OGChickenController>();
        anim = GetComponent<Animator>();
        rigidbody2D.velocity = 3 * _player.OGChickenVec;
        rotationangle = Mathf.Rad2Deg*Mathf.Atan2(_player.OGChickenVec.y, _player.OGChickenVec.x);
        transform.rotation = Quaternion.Euler (new Vector3(0, 0, rotationangle));
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
