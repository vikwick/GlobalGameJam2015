using UnityEngine;
using System.Collections;

public class MustacheCollectable : Collectable {
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

        player = GameManager.player;

        _player = player.GetComponent<OGChickenController>();
        anim = player.GetComponent<Animator>();
        anim.SetBool("MustacheIdle", true);
        anim.SetBool("SSIdle", false);
        anim.SetBool("Circuit", true);
        _player.maxSpeed += 6f;
        _player.maxProjSpeed += 4;
        _player.projectile = "taco";
    }
    void OnCollisionExit2D(Collision2D b){
        player = GameManager.player;
        _player = player.GetComponent<OGChickenController>();
        anim = player.GetComponent<Animator>();
        anim.SetBool("Circuit", false);
        Destroy(gameObject);
    }
}