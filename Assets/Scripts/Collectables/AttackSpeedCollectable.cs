using UnityEngine;
using System.Collections;

public class AttackSpeedCollectable : Collectable {

    protected override void OnTriggerEnter2D(Collider2D c)
	{
		GameManager._player.maxProjSpeed += 3;
		base.OnTriggerEnter2D(c);
    }
}
