using UnityEngine;
using System.Collections;

public class DemonEgg : EnemyController {

	void Start()
	{
		base.Init("DemonEgg", "");
		base.setStats(2,2,15,1,0);
	}

//	public override IEnumerator Attack ()
//	{
		//anim.SetBool("attack",true);
		//this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
//		rigidbody2D.velocity = new Vector2(0,0);
////		Debug.Log(Time.time);
//		yield return new WaitForSeconds(2);
//	//	Debug.Log(Time.time);
//		this.runningCoroutine = null;
//	}
}
