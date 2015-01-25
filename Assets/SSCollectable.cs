using UnityEngine;
using System.Collections;

public class SSCollectable : Collectable {
    
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
>>>>>>> ppleeease
        _player = player.GetComponent<OGChickenController>();
        anim = player.GetComponent<Animator>();
        anim.SetBool("MustacheIdle", false);
        anim.SetBool("SSIdle", true);
        anim.SetBool("Circuit", true);
<<<<<<< HEAD
        _player.ATK += 20;
=======
		_player.ATK += 20;
>>>>>>> ppleeease
        _player.maxProjSpeed -= 3;
        _player.maxSpeed += 4f;
        _player.projectile = "SpiritBomb";
        anim.SetBool("Circuit", true);
    }
    void OnCollisionExit2D(Collision2D b){
        player = GameManager.player;
        _player = player.GetComponent<OGChickenController>();
        anim = player.GetComponent<Animator>();
        anim.SetBool("Circuit", false);
        Destroy(gameObject);
    }
}