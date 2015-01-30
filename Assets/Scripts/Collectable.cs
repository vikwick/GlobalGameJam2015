using UnityEngine;
using System.Collections;

public abstract class Collectable : MonoBehaviour
{
	Animator anim;
	protected bool mustache;
	protected bool supersaiyan;
	protected bool circuit;
	// Use this for initialization
	
	protected void Init(bool mc, bool ssc, bool circ)
	{
		mustache = mc;
		supersaiyan = ssc;
		circuit = circ;
	}

	public virtual void OnCollisionEnter2D(Collision2D c)
	{
		anim = GameManager.player.GetComponent<Animator>();
		anim.SetBool("Circuit", true);
		anim.SetBool("MustacheIdle", mustache);
		anim.SetBool("SSIdle", supersaiyan);
	}

	public virtual void OnCollisionExit2D(Collision2D b)
	{
		anim.SetBool("Circuit", false);
		Destroy(gameObject);
	}
}