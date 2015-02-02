using UnityEngine;
using System.Collections;

public class SpeedUpCollectable : Collectable {

	protected override void OnTriggerEnter2D(Collider2D c)
	{
        GameManager._player.maxSpeed += 3f;
		base.OnTriggerEnter2D(c);
    }
}