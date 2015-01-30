using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	//change door pivot to be bottom

	public int positionType;
	public Door src;
	public Door dst;
	public Animator anim;

	// Use this for initialization
	void Start () {
		setSprite("door");
		anim = GetComponent<Animator>();
		if(positionType == 0)
		{
			
		}
		else if(positionType == 1){
			Vector3 newScale = gameObject.transform.localScale;
			newScale.x *= -1;
			newScale.y *= -1;
			this.gameObject.transform.localScale = newScale;
			this.gameObject.transform.position = gameObject.transform.position + this.getHeight()*1f*Vector3.down;
		}
		else if(positionType == 2){
			gameObject.transform.Rotate(new Vector3(0,0,90));
			gameObject.transform.position = gameObject.transform.position + this.getWidth()*-1f*Vector3.right;
		}
		else if(positionType == 3){
			gameObject.transform.Rotate(new Vector3(0,0,-90));
			gameObject.transform.position = gameObject.transform.position + this.getWidth()*Vector3.right;
		}

		this.gameObject.transform.position = gameObject.transform.position - new Vector3(0,0,1);
	}

	// Update is called once per frame
	void Update () {
	
	}

	public float getHeight(){
		return renderer.bounds.extents.y ;
		
	}

	public float getWidth(){
		return renderer.bounds.extents.x ;
		
	}
	
	public void setSprite(string spritestring){
		this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Environment/Doors/" + spritestring, typeof(Sprite));
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Player" && !anim.GetBool("Locked"))
		{
			GameManager.currRoom = dst.transform.parent.gameObject;
			GameManager._dungeon.placePlayer(dst);
			// send player to dst
//			_r.populateEnemies(Random.Range (difficulty, difficulty*3));
		}
	}
}
