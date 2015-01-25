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
        _player.maxSpeed += 6f;
        _player.maxProjSpeed += 4;
        Destroy(gameObject);
    }
}