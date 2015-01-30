using UnityEngine;
using System.Collections;

public class Scientist : EnemyController {

	void Start()
	{
		base.Init("Scientist", "AcidBolt");
		base.setStats(1,2,10,1,5);
	}

//	public override void Kill ()
//	{
//		Vector3 pos = player.transform.position - transform.position;
//		if(pos.magnitude < RNG )
//		{
//			if(this.runningCoroutine==null)
//			{
//				this.runningCoroutine = StartCoroutine(this.Attack());
//			}
//		}
//		else
//		{
//			anim.SetBool("attacking", false);	
//		}
//		attackpath = (pos - transform.position);
//	}

//	public override IEnumerator Attack()
//	{
//		anim.SetBool("attacking",true);
//		rigidbody2D.velocity = new Vector2(0,0);
//		GameObject head = Instantiate(projectile, transform.position + Vector3.right, transform.rotation) as GameObject;
//		head.transform.parent = this.transform;
//		head.AddComponent<BoxCollider2D>().isTrigger = true;
//
//		yield return new WaitForSeconds(1);
//		runningCoroutine = null;
//		Destroy(head);
//	}
}
