using UnityEngine;
using System.Collections;

public class DemonEgg : EnemyController {



	public override void setStats (int x, int y, int z)
	{
		base.setStats (3, 10, 10);


	}

	public override void setProjectile (string s)
	{
		//base.setProjectile (s);
	}

	public override void kill ()
	{
		base.kill ();
	}

	public override string getSpriteString(){
		return "Sprites/Demon Egg/DemonEgg";
	}

	public override IEnumerator Attack ()
	{
		//anim.SetBool("attack",true);
		//this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		rigidbody2D.velocity = new Vector2(0,0);
//		Debug.Log(Time.time);
		yield return new WaitForSeconds(2);
	//	Debug.Log(Time.time);
		this.runningCoroutine = null;


	}

	public override string getProjectileString(){
		return null;
	}

}
