using UnityEngine;
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
	public bool beaten = false;
	public bool visited = false;
	public ArrayList enemies = new ArrayList();
	public int roomNum;

	// Use this for initialization
	void Start () {
		int n = 14;
		GameObject wall = Instantiate(Resources.Load ("Prefabs/Wall", typeof(GameObject)), transform.position, transform.rotation) as GameObject;
		wall.transform.parent = transform;
	}

	void Update()
	{
		if(enemies.Count==0 && doors.Count>0 && ((GameObject)doors[0]).GetComponent<Door>().anim.GetBool("Locked"))
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

	public void alarm()
	{
		visited = true;
		foreach(GameObject d in doors)
		{
			d.GetComponent<Door>().anim.SetBool("Locked", true);
		}
	}

	public void reset()
	{
		foreach(GameObject d in doors)
		{
			d.GetComponent<Door>().anim.SetBool("Locked", false);
		}
	}

	public void createFloor(int x, int y) {
		f = Instantiate(Resources.Load("Prefabs/Floor", typeof(GameObject)), new Vector2(x,y), transform.rotation) as GameObject;
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
			float ran = Random.Range(-1f,1f);
			float p = ran*5;
			Vector2 v = new Vector2(transform.position.x-p, transform.position.y-p);

			if(r==0)
			{
				GameObject en = Instantiate(Resources.Load ("Prefabs/BusinessEnemy", typeof(GameObject)), v, transform.rotation) as GameObject;
				enemies.Add(en);
				en.GetComponent<EnemyController>().r = this;
			}
			else if(r==1)
			{
				GameObject en = Instantiate(Resources.Load ("Prefabs/DemonEgg", typeof(GameObject)), v, transform.rotation) as GameObject;
				enemies.Add(en);
				en.GetComponent<DemonEgg>().r = this;
			}
			else if(r==2)
			{
				GameObject en = Instantiate(Resources.Load ("Prefabs/DemonChicken", typeof(GameObject)), v, transform.rotation) as GameObject;
				enemies.Add(en);
				en.GetComponent<DemonChicken>().r = this;
			}
			else if(r==3)
			{
				GameObject en = Instantiate(Resources.Load ("Prefabs/Scientist", typeof(GameObject)), v, transform.rotation) as GameObject;
				enemies.Add(en);
				en.GetComponent<Scientist>().r = this;
			}
		}
	}

	public void populateItem()
	{
		if(Random.Range(0f,1f)<.9f)
		{
			float r = Random.Range(0f,1f);
			Vector2 v = transform.position;
			if(r < .9f)
				item = Instantiate(Resources.Load ("Prefabs/SSCollectable", typeof(GameObject)), v, transform.rotation) as GameObject;
			else if(r < .2f)
				item = Instantiate(Resources.Load ("Prefabs/MustacheCollectable", typeof(GameObject)), v, transform.rotation) as GameObject;
			else if(r < .5f)
				item = Instantiate(Resources.Load ("Prefabs/AttackUpCollectable", typeof(GameObject)), v, transform.rotation) as GameObject;
			else if(r < .7f)
				item = Instantiate(Resources.Load ("Prefabs/SpeedUpCollectable", typeof(GameObject)), v, transform.rotation) as GameObject;
			else
				item = Instantiate(Resources.Load ("Prefabs/AttackSpeedCollectable", typeof(GameObject)), v, transform.rotation) as GameObject;
			item.SetActive(false);
			item.transform.parent = transform;
		}
	}

	/*
	 * Warns the room that the player has entered, 
	 */
	public void playerEntered()
	{
		if(!beaten)
		{
			if (!visited)
				populateEnemies(Random.Range(GameManager.difficulty,GameManager.difficulty*2));
			alarm ();
		}
	}
}
