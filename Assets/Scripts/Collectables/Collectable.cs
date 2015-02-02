using UnityEngine;
using System.Collections;

public abstract class Collectable : MonoBehaviour
{
	Animator anim;
	protected bool mustache = false;
	protected bool supersaiyan = false;
	
	protected void Init(bool mc, bool ssc)
	{
		mustache = mc;
		supersaiyan = ssc;
	}

	protected virtual void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			anim = GameManager.player.GetComponent<Animator>();
			anim.SetBool("Circuit", true);
			anim.SetBool("MustacheIdle", mustache);
			anim.SetBool("SSIdle", supersaiyan);
			StartCoroutine(Poof());
		}
	}

	IEnumerator Poof()
	{
		yield return new WaitForSeconds(.1f);
		if(anim!=null)
			anim.SetBool("Circuit", false);
		Destroy(gameObject);
	}
}