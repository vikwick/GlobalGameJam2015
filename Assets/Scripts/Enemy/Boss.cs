using UnityEngine;
using System.Collections;

public class Boss : EnemyController
{
	void OnEnable ()
	{
		base.Init("Boss/Ogre1", "");
		base.setStats(20,2,20,2,5);
	}
}
