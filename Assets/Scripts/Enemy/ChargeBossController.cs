using UnityEngine;
using System.Collections;

public class ChargeBossController : EnemyController
{
    int maxFireShots = 5;
	FireProjectile _projectile;

	void OnEnable()
	{
		base.Init("Boss/Ogre1", "FireProjectile");
		base.setStats(20,2,50,2,6);
		_projectile = projectile.GetComponent<FireProjectile>();
		_projectile.boss = gameObject;
	}

	public override void Kill()
    {
        if(GameManager.player!= null){
            attackpath = (GameManager.player.transform.position - transform.position);
			rigidbody2D.AddForce(attackpath.normalized);
			if(attackpath.magnitude < RNG && runningCoroutine==null) 
			{
				int r = Random.Range(0,5);
				if(r<4)
					runningCoroutine = StartCoroutine(Attack());
				else
					runningCoroutine = StartCoroutine(Fire());
			}
        }
    }

	IEnumerator Fire()
	{
	    anim.SetBool("rangedattack",true);
//		this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		rigidbody2D.velocity = new Vector2(0,0);
	    for (int i=0; i<maxFireShots ; i++)
		{
	        GameObject FireShots = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
	    }
	    yield return new WaitForSeconds(1f);
		runningCoroutine = null;
		anim.SetBool("rangedattack",false);
	    //fix the beginning of these instantiations to be the fire projectile which has the other script i sent attached
	}

	public override IEnumerator Attack()
	{
//		anim.SetBool("attack", true);
		yield return new WaitForSeconds(1f);
	    attackpath = (GameManager.player.transform.position - transform.position);
	    rigidbody2D.AddForce(attackpath*SPD);
		anim.SetBool("attack", true);
		runningCoroutine = null;
		yield return new WaitForSeconds(.3f);
		anim.SetBool("attack", false);
	}
}