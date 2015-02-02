using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	int n = 6;
	// Use this for initialization
	void OnEnable ()
	{
		setSprite("floor");
		scale(1,1);
	}

	public void scale(int x, int y)
	{
		this.gameObject.transform.localScale = new Vector3(x, y, 0);
	}

	public void setSprite(string spritestring)
	{
		GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Environment/Floors/" + spritestring, typeof(Sprite)) as Sprite;

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
