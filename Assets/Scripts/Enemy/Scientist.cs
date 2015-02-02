using UnityEngine;
using System.Collections;

public class Scientist : EnemyController {

	void OnEnable()
	{
		base.Init("Scientist/Scientist1", "AcidBolt");
		base.setStats(5,2,15,1,5);
	}
}
