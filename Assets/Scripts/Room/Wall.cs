using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour {
	



	// Use this for initialization
	void Start () {
		this.setSprite("Environment/walls");
		this.scale(2,2);
		this.gameObject.AddComponent<PolygonCollider2D>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Debug.Log ("Clicked Wall");
	}

	void setSprite(string spritestring){
		this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/" + spritestring, typeof(Sprite));
	}

	public void scale(int x, int y){
		this.gameObject.transform.localScale = new Vector3(x, y, 0);
	}




}
