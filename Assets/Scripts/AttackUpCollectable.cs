﻿using UnityEngine;
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
		player = GameManager.player;
        _player = player.GetComponent<OGChickenController>();
        anim = player.GetComponent<Animator>();
        _player.ATKUp = true;
        _player.ATKSPDUp = false;
        _player.SPDUp = false;
        _player.ATK += 5;
        Destroy(gameObject);
    }
}