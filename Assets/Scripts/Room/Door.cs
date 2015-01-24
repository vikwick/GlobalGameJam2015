using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	//change door pivot to be bottom

	public int positionType;
	void OnMouseDown(){
		Debug.Log ("click");
	}
	// Use this for initialization
	void Start () {
		this.setSprite("door");
		this.gameObject.AddComponent<BoxCollider2D>();

		if(positionType == 1){
			gameObject.transform.Rotate(new Vector3(0,0,-90));
			gameObject.transform.position = gameObject.transform.position + this.getWidth()*Vector3.right;
		}

		if(positionType == 2){
			gameObject.transform.Rotate(new Vector3(0,0,90));
			gameObject.transform.position = gameObject.transform.position + this.getWidth()*-1f*Vector3.right;
		}
		if(positionType == 4){
			Vector3 newScale = gameObject.transform.localScale;
			newScale.x *= -1;
			newScale.y *= -1;
			this.gameObject.transform.localScale = newScale;
			this.gameObject.transform.position = gameObject.transform.position + this.getHeight()*1f*Vector3.down;
		}

		this.gameObject.transform.position = gameObject.transform.position - new Vector3(0,0,1);
	}

	public static void createDoors(GameObject floor, ArrayList doors){
		GameObject doorp = (GameObject)Resources.Load("Prefabs/door");
		Floor fscript =  floor.GetComponent<Floor>();
		GameObject door = Instantiate(doorp,fscript.top () , floor.transform.rotation) as GameObject;
		doors.Add(door);

		if(Random.Range(0,2)== 0){
			GameObject doorBottom = Instantiate(doorp, fscript.bottom() , floor.transform.rotation) as GameObject;
			doorBottom.GetComponent<Door>().positionType = 4;
			doors.Add(doorBottom);
		}
		if(Random.Range(0,2)== 0){
			GameObject doorRight= Instantiate(doorp, fscript.right() , floor.transform.rotation) as GameObject;
			doorRight.GetComponent<Door>().positionType = 1;
			doors.Add(doorRight);
		}
		if(Random.Range(0,2)== 0){
			GameObject doorLeft= Instantiate(doorp, fscript.left() , floor.transform.rotation) as GameObject;
			doorLeft.GetComponent<Door>().positionType = 2;
			doors.Add(doorLeft);
		}
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
		this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/" + spritestring, typeof(Sprite));
	}
}
