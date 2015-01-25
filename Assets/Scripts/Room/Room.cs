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
		int n = 16;
		this.f = Floor.createFloor(n*x, n*y);
		f.transform.parent = transform;
		GameObject wall = Instantiate(Resources.Load ("Prefabs/Wall", typeof(GameObject)), new Vector2(n*x,n*y), transform.rotation) as GameObject;
		wall.transform.parent = transform;
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
