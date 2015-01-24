using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void scale(int x, int y){
		this.gameObject.transform.localScale = new Vector3(x, y, 0);
	}

	void setSprite(string spritestring){
		this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/" + spritestring, typeof(Sprite));
	}
}
