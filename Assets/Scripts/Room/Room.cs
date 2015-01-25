using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	private Floor floorScript;
	private GameObject f;
	public ArrayList doors;
	public GameObject[] dirs = {null, null, null, null}; // UP DOWN LEFT RIGHT
	bool satisfied;
	public int x;
	public int y;

	// Use this for initialization
	void Start () {
		this.f = Floor.createFloor(x, y);
		f.transform.parent = gameObject.transform;
//		this.doors = new ArrayList();
//		Door.createDoors(this.f, this.doors);
		GameObject wall = Instantiate(Resources.Load ("Prefabs/Wall", typeof(GameObject)), transform.position, transform.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void createDoor(){


	}

	public static void createRoom()
	{

	}
}
