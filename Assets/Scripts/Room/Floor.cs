using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	int n = 6;
	// Use this for initialization
	void Start () {
		this.setSprite("Environment/floor");
		this.scale(1,1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void scale(int x, int y){
		this.gameObject.transform.localScale = new Vector3(x, y, 0);
	}

	public void setSprite(string spritestring){
		this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/" + spritestring, typeof(Sprite));
	}

	public float getHeight(){
		return renderer.bounds.extents.y ;
	}
	
	public float getWidth(){
		return renderer.bounds.extents.x ;
		
	}

	public Vector2 getSize(){
		return new Vector2(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);
	}

	public Vector2 top(){
		Vector2 v = (Vector2)((Vector2)transform.position + n*Vector2.up);
		return v;
	}

	public Vector2 bottom(){
		return (Vector2)((Vector2)transform.position - n*Vector2.up);
	}

	public Vector2 right(){
		return (Vector2)((Vector2)transform.position + n*Vector2.right);
	}

	public Vector2 left(){
		return (Vector2)((Vector2)transform.position - n*Vector2.right);
	}
}
