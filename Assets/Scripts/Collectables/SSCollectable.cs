using UnityEngine;
using System.Collections;

public class SSCollectable : Collectable {
    
    void Start ()
	{
		base.Init(false, true);
    }

    void OnTriggerEnter2D(Collider2D c)
	{
		GameManager._player.ATK += 6;
        GameManager._player.maxProjSpeed -= 3;
        GameManager._player.maxSpeed += 3f;
        GameManager._player.setProjectile("SpiritBomb");
		base.OnTriggerEnter2D(c);
    }
}