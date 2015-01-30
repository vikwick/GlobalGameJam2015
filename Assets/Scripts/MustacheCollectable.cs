using UnityEngine;
using System.Collections;

public class MustacheCollectable : Collectable {

    void Start ()
	{
		base.Init(true,false,true);
    }
    
	void OnCollisionEnter2D(Collision2D c)
	{
        GameManager._player.maxSpeed += 6f;
		GameManager._player.maxProjSpeed += 4;
		GameManager._player.setProjectile("Taco");
		base.OnCollisionEnter2D(c);
    }
}