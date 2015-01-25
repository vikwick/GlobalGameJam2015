using UnityEngine;
using System.Collections;

public class SSCollectable : Collectable {
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
        anim.SetBool("MustacheIdle", false);
        anim.SetBool("SSIdle", true);
<<<<<<< HEAD
        anim.SetBool("Circuit", true);
        _player.ATK += 20;
=======
		_player.ATK += 20;
>>>>>>> tylerbranch
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