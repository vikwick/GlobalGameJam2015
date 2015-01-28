using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {
	protected GameObject player;
	protected OGChickenController _player;
	protected Animator anim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void OnCollisionExit2D(Collision2D b){
		player = GameManager.player;
		_player = player.GetComponent<OGChickenController>();
		anim = player.GetComponent<Animator>();
		Destroy(gameObject);
	}
}