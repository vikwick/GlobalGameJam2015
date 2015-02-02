using UnityEngine;
using System.Collections;

public class MustacheCollectable : Collectable {

    void Start ()
	{
		base.Init(true,false);
    }

	void OnTriggerEnter2D(Collider2D c)
	{
		GameManager._player.maxSpeed += 3f;
		GameManager._player.maxProjSpeed += 3;
		GameManager._player.setProjectile("Taco");
		base.OnTriggerEnter2D(c);
	}
}