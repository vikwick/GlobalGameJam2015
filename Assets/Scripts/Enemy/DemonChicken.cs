using UnityEngine;
using System.Collections;

public class DemonChicken : EnemyController {

	void Start()
	{
		base.Init("DemonChicken", "DemonChickenProjectile");
		base.setStats(2,2,10,1,5);
	}
}