using UnityEngine;
using System.Collections;

public class SSCollectable : MonoBehaviour {
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
        anim.SetBool("SSIdle", true);
        _player.ATK += 20;
        _player.maxProjSpeed -= 3;
        _player.maxSpeed += 4f;
        _player.projectile = "SpiritBomb";
        Destroy(gameObject);
    }
}