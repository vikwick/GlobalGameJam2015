using UnityEngine;
using System.Collections;

public class AttackUpCollectable : Collectable {

	protected override void OnTriggerEnter2D(Collider2D c)
	{
        GameManager._player.ATK += 3;
		base.OnTriggerEnter2D(c);
    }
}