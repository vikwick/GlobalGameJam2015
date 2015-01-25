using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {


	// Use this for initialization
	void Start () {
		this.setSprite("Environment/floor");
		this.scale(1,1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static GameObject createFloor(int x, int y){
		GameObject floor = (GameObject)Resources.Load("Prefabs/floor");
		GameObject f = Instantiate(floor, new Vector2(x,y), Quaternion.identity) as GameObject;
		return f;
	}
	

	public void scale(int x, int y){
		this.gameObject.transform.localScale = new Vector3(x, y, 0);
	}

	public void setSprite(string spritestring){
		this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/" + spritestring, typeof(Sprite));
	}

	public Vector2 getLocation(){
		return new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
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
		return (Vector2)(this.getLocation() + this.getHeight()*Vector2.up);
	}

	public Vector2 bottom(){
		return (Vector2)(this.getLocation() - 1f*this.getHeight()*Vector2.up);
	}

	public Vector2 right(){
		return (Vector2)(this.getLocation() + this.getWidth()*Vector2.right);
	}

	public Vector2 left(){
		return (Vector2)(this.getLocation() + this.getWidth()*-1f*Vector2.right);
	}
}
