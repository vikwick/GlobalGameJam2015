using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour {

	void Start ()
	{
		setSprite("walls");
		scale(1,1);
//		gameObject.AddComponent<PolygonCollider2D>();
	}

	void OnMouseDown()
	{
		Debug.Log ("Clicked Wall");
	}

	void setSprite(string spritestring)
	{
		this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Environment/Walls/" + spritestring, typeof(Sprite));
	}

	public void scale(int x, int y)
	{
		this.gameObject.transform.localScale = new Vector3(x, y, 0);
	}
}
