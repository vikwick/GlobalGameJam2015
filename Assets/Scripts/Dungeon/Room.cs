using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{

	public Floor _floor;
	public GameObject floor;
	public GameObject walls;
	public Wall _walls;
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
	public bool bossRoom = false;
	int n = 16;

	void OnEnable ()
	{
		createFloor();
		createWalls();
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
		foreach(GameObject e in enemies)
		{
			e.GetComponent<EnemyController>().reset();
			e.SetActive(false);
		}
	}

	void reactivateEnemies()
	{
		foreach(GameObject e in enemies)
		{
			e.SetActive(true);
		}
	}

	public void convertToBossRoom()
	{
		floor.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Environment/Floors/" + "floor2", typeof(Sprite)) as Sprite;
		walls.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Environment/Walls/" + "walls2", typeof(Sprite)) as Sprite;
		bossRoom = true;
	}

	void createFloor()
	{
		floor = Instantiate(Resources.Load("Prefabs/Floor", typeof(GameObject)), new Vector2(n*x,n*y), transform.rotation) as GameObject;
		_floor = floor.GetComponent<Floor>();
		floor.transform.parent = transform;
	}

	void createWalls()
	{
		walls = Instantiate(Resources.Load("Prefabs/Walls", typeof(GameObject)), new Vector2(n*x,n*y), transform.rotation) as GameObject;
		_walls = walls.GetComponent<Wall>();
		walls.transform.parent = transform;
	}

	public void createDoor(int type)
	{
		GameObject door = (GameObject)Resources.Load("Prefabs/Door");

		if(type == 0)
		{
			door = Instantiate(door, _floor.top () , floor.transform.rotation) as GameObject;
		}
		else if(type == 1)
		{
			door = Instantiate(door, _floor.bottom() , floor.transform.rotation) as GameObject;
		}
		else if(type == 2)
		{
			door = Instantiate(door, _floor.left() , floor.transform.rotation) as GameObject;
		}
		else if(type == 3)
		{
			door = Instantiate(door, _floor.right() , floor.transform.rotation) as GameObject;
		}

		dirs[type] = door;
		door.transform.parent = transform;
		Door _door = door.GetComponent<Door>();
		_door.positionType = type;
		doors.Add(door);
	}

	public void populateEnemies(int num)
	{
		for(int i=0; i< num; i++)
		{
			int r = Random.Range(0,GameManager.numEnemies);
			float ran = Random.Range(-1f,1f);
			float p = ran*5;
			Vector2 v = new Vector2(transform.position.x-p, transform.position.y-p);
			GameObject en = null;

			if(bossRoom)
			{
				en = Resources.Load ("Prefabs/OgreBoss", typeof(GameObject)) as GameObject;
				en.GetComponent<Boss>().r = this;	/* REPLACE WITH BOSS SCRIPT */
				en = Instantiate(en, v, transform.rotation) as GameObject;
				enemies.Add(en);
				return;
			}
			else if(r==0)
			{
				en = Resources.Load ("Prefabs/BusinessEnemy", typeof(GameObject)) as GameObject;
				en.GetComponent<BusinessEnemy>().r = this;
			}
			else if(r==1)
			{
				en = Resources.Load ("Prefabs/DemonEgg", typeof(GameObject)) as GameObject;
				en.GetComponent<DemonEgg>().r = this;
			}
			else if(r==2)
			{
				en = Resources.Load ("Prefabs/DemonChicken", typeof(GameObject)) as GameObject;
				en.GetComponent<DemonChicken>().r = this;
			}
			else if(r==3)
			{
				en = Resources.Load ("Prefabs/Scientist", typeof(GameObject)) as GameObject;
				en.GetComponent<Scientist>().r = this;
			}
			en = Instantiate(en, v, transform.rotation) as GameObject;
			enemies.Add(en);
			en.transform.parent = transform;
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
			else
				reactivateEnemies();
			alarm ();
		}
		Debug.Log (enemies.Count + " enemies.");
	}
}
