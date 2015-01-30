using UnityEngine;
using System.Collections;

public class SSCollectable : Collectable {
    
    void Start ()
	{
		base.Init(false, true, true);
    }

    void OnCollisionEnter2D(Collision2D c)
	{
		GameManager._player.ATK += 20;
        GameManager._player.maxProjSpeed -= 3;
        GameManager._player.maxSpeed += 4f;
        GameManager._player.setProjectile("SpiritBomb");
		base.OnCollisionEnter2D(c);
    }
}