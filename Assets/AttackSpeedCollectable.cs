using UnityEngine;
using System.Collections;

public class AttackSpeedCollectable : Collectable {
    GameObject player;
    OGChickenController _player;
    Animator anim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D c){
<<<<<<< HEAD
        player = GameManager.player;
=======
		player = GameManager.player;
>>>>>>> tylerbranch
        _player = player.GetComponent<OGChickenController>();
        anim = player.GetComponent<Animator>();
        _player.maxProjSpeed += 5;
        Destroy(gameObject);
    }
}
