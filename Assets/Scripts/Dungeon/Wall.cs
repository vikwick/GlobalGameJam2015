using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour {

	void OnEnable()
	{
		setSprite("walls");
		scale(1,1);
	}

	public void setSprite(string spritestring)
	{
		GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Environment/Walls/" + spritestring, typeof(Sprite));
	}

	public void scale(int x, int y)
	{
		transform.localScale = new Vector3(x, y, 0);
	}
}
