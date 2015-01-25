using UnityEngine;
using System.Collections;

public class AttackUpCollectable : Collectable {
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
=======

>>>>>>> ppleeease
		player = GameManager.player;
        _player = player.GetComponent<OGChickenController>();
        anim = player.GetComponent<Animator>();
        _player.ATK += 5f;
        Destroy(gameObject);
    }
}