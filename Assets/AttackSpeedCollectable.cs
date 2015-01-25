using UnityEngine;
using System.Collections;

public class AttackSpeedCollectable : MonoBehaviour {
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
        _player.ATKSPDUp = true;
        _player.ATKUp = false;
        _player.SPDUp = false;
        anim = player.GetComponent<Animator>();
        _player.maxProjSpeed += 5;
        Destroy(gameObject);
    }
}
