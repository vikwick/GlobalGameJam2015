using UnityEngine;
using System.Collections;

public class BusinessEnemy : EnemyController {

	void Start()
	{
		base.Init("BusinessEnemy", "Head");
		base.setStats(1,4,15,2,5);
	}
}
