using UnityEngine;
using System.Collections;

public class DemonChicken : EnemyController {

	void Start()
	{
		base.Init("DemonChicken", "DemonEgg");
		base.setStats(2,2,10,1,5);
	}

//	public override void Kill ()
//	{
//		if(p!= null){
//			attackpath = p.transform.position-transform.position;
//			if(attackpath.magnitude < RNG ) // player in range
//			{	//this.attacking = true;
//				
//				if(this.runningCoroutine==null){
//					this.runningCoroutine = StartCoroutine(this.Attack());
//				}
//			}
//			else{
//				anim.SetBool("attacking", false);
//			}
//		}
//	}
	
//	public override IEnumerator Attack()
//	{
//		anim.SetBool("attacking",true);
//		rigidbody2D.velocity = new Vector2(0,0);
//		GameObject egg = Resources.Load ("Prefabs/DemonChickenProjectile")as GameObject;
//		EnemyProjectileScript ep = egg.GetComponent<EnemyProjectileScript>();
//		ep._enemy = this;
//		GameObject head = Instantiate(egg, transform.position + Vector3.right*-.5f, transform.rotation) as GameObject;
//		head.transform.parent = transform;
//		head.AddComponent<BoxCollider2D>().isTrigger = true;
//
//		yield return new WaitForSeconds(1);
//		this.runningCoroutine = null;
//		Destroy(head);
//	}
}