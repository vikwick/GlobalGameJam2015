﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	public Floor floorScript;
	public GameObject f;
	public ArrayList doors = new ArrayList();
	public GameObject[] dirs = {null, null, null, null}; // UP DOWN LEFT RIGHT
	public bool start = false;
	public int x;
	public int y;
	public GameObject item = null;
	bool beaten = false;
	ArrayList enemies = new ArrayList();

	// Use this for initialization
	void Start () {
		int n = 16;
		GameObject wall = Instantiate(Resources.Load ("Prefabs/Wall", typeof(GameObject)), new Vector2(n*x,n*y), transform.rotation) as GameObject;
		wall.transform.parent = transform;
	}

	void Update()
	{
		if(enemies.Count==0)
		{
			beaten = true;
			for(int i=0; i<doors.Count; i++)
			{
				((GameObject)doors[i]).GetComponent<Door>().anim.SetBool("Locked", false);
			}
		}
		if(item!=null && beaten)
		{
			item.SetActive(true);
		}
	}

	public void createFloor(int x, int y) {
		f = Instantiate(Resources.Load("Prefabs/Floor", typeof(GameObject)), new Vector2(x,y), Quaternion.identity) as GameObject;
		floorScript = f.GetComponent<Floor>();
		f.transform.parent = transform;
	}

	public void createDoor(int type)
	{
		GameObject doorp = (GameObject)Resources.Load("Prefabs/Door");
		GameObject door = null;
		if(type == 0)
		{
			door = Instantiate(doorp, floorScript.top () , f.transform.rotation) as GameObject;
		}
		else if(type == 1)
		{
			door = Instantiate(doorp, floorScript.bottom() , f.transform.rotation) as GameObject;
		}
		else if(type == 2)
		{
			door = Instantiate(doorp, floorScript.left() , f.transform.rotation) as GameObject;
		}
		else if(type == 3)
		{
			door = Instantiate(doorp, floorScript.right() , f.transform.rotation) as GameObject;
		}
		dirs[type] = door;
		door.transform.parent = transform;
		Door _door = door.GetComponent<Door>();
		_door.positionType = type;
		doors.Add(door);
	}

//	public void linkDoors(Door src, Door dst)
//	{
//		src.dst = dst;
//	}

	public void populateEnemies(int num)
	{
		for(int i=0; i< num; i++)
		{
			int r = Random.Range(0,GameManager.numEnemies);
			if(r==0)
			{
//				enemies.Add(Instantiate(Resources.Load ("Prefabs/BusinessEnemy", typeof(GameObject)), transform.position, transform.rotation) as GameObject);
			}
			else if(r==1)
			{
//				enemies.Add(Instantiate(Resources.Load ("Prefabs/DemonEgg", typeof(GameObject)), transform.position, transform.rotation) as GameObject);
			}
			else if(r==2)
			{
//				enemies.Add(Instantiate(Resources.Load ("Prefabs/DemonChicken", typeof(GameObject)), transform.position, transform.rotation) as GameObject);
			}
			else if(r==3)
			{
//				enemies.Add(Instantiate(Resources.Load ("Prefabs/LabCoat", typeof(GameObject)), transform.position, transform.rotation) as GameObject);
			}
		}
	}

	public void populateItem()
	{
		if(Random.Range(0,100)<10)
		{
			int r = Random.Range(0,10);
			if(r < 2)
				item = Instantiate(Resources.Load ("Prefabs/AttackSpeedCollectable", typeof(GameObject)), transform.position, transform.rotation) as GameObject;
			else if(r < 4)
				item = Instantiate(Resources.Load ("Prefabs/AttackUpCollectable", typeof(GameObject)), transform.position, transform.rotation) as GameObject;
			else if(r < 6)
				item = Instantiate(Resources.Load ("Prefabs/MustacheCollectable", typeof(GameObject)), transform.position, transform.rotation) as GameObject;
			else if(r < 8)
				item = Instantiate(Resources.Load ("Prefabs/SpeedUpCollectable", typeof(GameObject)), transform.position, transform.rotation) as GameObject;
			else
				item = Instantiate(Resources.Load ("Prefabs/SSCollectable", typeof(GameObject)), transform.position, transform.rotation) as GameObject;
			item.SetActive(false);
		}
	}

}
