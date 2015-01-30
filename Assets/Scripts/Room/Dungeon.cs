using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour
{

	int difficulty;
	int maxRooms;
	ArrayList rooms = new ArrayList();
	Room[,] map;
	public GameObject start;

	/*
	 * Completely generates a new dungeon based on the
	 * difficulty setting.
	 * 
	 * @param diff Difficulty level of the dungeon.
	 */
	public void generateDungeon(int diff)
	{
		difficulty = diff;
		maxRooms = 8*difficulty;
		map = new Room[maxRooms, maxRooms];

		createRooms();
		pruneRooms();
		createDoors();
		linkDoors();
		positionDungeonAtOrigin();
	}

	/*
	 * Creates all of the rooms, giving them legal positions based
	 * on the 2D map[,].
	 */
	void createRooms()
	{
		int x = Random.Range(0,maxRooms);
		int y = Random.Range(0,maxRooms);
		GameObject room = generateRoom(x,y);
		Room _room = room.GetComponent<Room>();
		_room.roomNum = 0;
		_room.x = x;
		_room.y = y;
		map[x,y] = _room;
		rooms.Add(room);
		_room.transform.parent = transform;
//		Debug.Log(maxRooms + " max rooms");
		for(int i=1; i<maxRooms; i++)
		{
			while(!contains(x,y) || (contains(x,y) && map[x,y] != null))
			{
				x = _room.x;
				y = _room.y;
				int d = Random.Range(0,4);
				
				if(d==0)			// UP
					y++;
				else if(d==1)		// DOWN				
					y--;
				else if(d==2)		// LEFT
					x--;
				else if(d==3)		// RIGHT
					x++;
			}
			rooms.Add(generateRoom(x, y));
			Room _newRoom = ((GameObject)rooms[i]).GetComponent<Room>();
			_newRoom.roomNum = i;
			_newRoom.x = x;
			_newRoom.y = y;
			map[x,y] = _newRoom;
			_newRoom.transform.parent = transform;
			_room = _newRoom;
		}
	}

	/*
	 * Prunes any undesirable rooms, removing them from all
	 * data structures and destroying them.
	 */
	void pruneRooms()
	{
		for(int i=0; i<rooms.Count; i++)
		{
			GameObject r = (GameObject)rooms[i];
			Room _r = r.GetComponent<Room>();
			if(surrounded (_r.x,_r.y))
			{
				rooms.Remove(r);
				map[_r.x,_r.y] = null;
//				maxRooms--;
				i--;
				Destroy(r);
			}
		}
//		for(int i=0; i<maxRooms; i++)
//		{
//			for(int j=0; j<maxRooms; j++)
//			{
//				if(map[i,j] != null)
//				{
//					Debug.Log(map[i,j].roomNum + " at x = " + i + ", y = " + j);
//				}
//			}
//		}
	}

	/*
	 * Creates doors based on the positions of rooms in map[,] and
	 * also allows for items to randomly spawn in rooms (necessary to
	 * do here for efficiency).
	 */
	void createDoors()
	{
		start = (GameObject)rooms[Random.Range(0,rooms.Count)];
		start.GetComponent<Room>().beaten = true;
		start.GetComponent<Room>().start = true;
		foreach(GameObject r in rooms)
		{
			Room _r = r.GetComponent<Room>();
			int x = _r.x;
			int y = _r.y;
			
			if(contains(x,y+1) && map[x,y+1]!=null)
			{
				_r.createDoor(0);
			}
			if(contains(x,y-1) && map[x,y-1]!=null)
			{
				_r.createDoor(1);
			}
			if(contains(x-1,y) && map[x-1,y]!=null)
			{
				_r.createDoor(2);
			}
			if(contains(x+1,y) && map[x+1,y]!=null)
			{
				_r.createDoor(3);
			}
			if(r != start)
				_r.populateItem();
		}
	}

	/*
	 * Links all adjacent doors to one another, effectively completing
	 * the dungeon.
	 */
	void linkDoors()
	{
		foreach(GameObject r in rooms)
		{
			Room _r = r.GetComponent<Room>();
			int x = _r.x;
			int y = _r.y;
			for(int i=0; i<4; i++)
			{
				Door _door = _r.dirs[i]!=null ? _r.dirs[i].GetComponent<Door>() : null;
//				Debug.Log("Source: " + _r.roomNum);
				if (_door != null) {
					
					if(i==0 && contains(x,y+1) && map[x,y+1]!=null)
					{
//						Debug.Log("i = " + i + ", x = " + x + ", y = " + y);
//						Debug.Log(map[x,y+1].roomNum);
						_door.dst = map[x,y+1].dirs[1].GetComponent<Door>();
					}
					else if(i==1 && contains(x,y-1) && map[x,y-1]!=null)
					{
//						Debug.Log("i = " + i + ", x = " + x + ", y = " + y);
//						Debug.Log(map[x,y-1].roomNum);
						_door.dst = map[x,y-1].dirs[0].GetComponent<Door>();
					}
					else if(i==2 && contains(x-1,y) && map[x-1,y]!=null)
					{
//						Debug.Log("i = " + i + ", x = " + x + ", y = " + y);
//						Debug.Log(map[x-1,y].roomNum);
						_door.dst = map[x-1,y].dirs[3].GetComponent<Door>();
					}
					else if(i==3 && contains(x+1,y) && map[x+1,y]!=null)
					{
//						Debug.Log("i = " + i + ", x = " + x + ", y = " + y);
//						Debug.Log(map[x+1,y].roomNum);
						_door.dst = map[x+1,y].dirs[2].GetComponent<Door>();
					}
				}
			}
		}
	}
	
	void positionDungeonAtOrigin()
	{
		transform.position = new Vector2(-start.transform.position.x,-start.transform.position.y);
	}

	public bool surrounded(int x, int y)
	{
		if((!contains(x+1,y) || map[x+1,y]!=null) && (!contains(x-1,y) || map[x-1,y]!=null) && (!contains(x,y+1) || map[x,y+1]!=null) && 
		   (!contains(x,y-1) || map[x,y-1]!=null) && (!contains(x+1,y+1) || map[x+1,y+1]!=null) && (!contains(x-1,y+1) || map[x-1,y+1]!=null) && 
		   (!contains(x+1,y-1) || map[x+1,y-1]!=null) && (!contains(x-1,y-1) || map[x-1,y-1]!=null))
			return true;
		return false;
	}

	public bool contains(int x, int y)
	{
		return x < maxRooms && x >= 0 && y < maxRooms && y >= 0;
	}

	public GameObject generateRoom(int x, int y)
	{
		int n = 16;
		GameObject r = Instantiate(Resources.Load ("Prefabs/Room", typeof(GameObject)), new Vector2(n*x,n*y), transform.rotation) as GameObject;
		Room _r = r.GetComponent<Room>();
		_r.createFloor(n*x, n*y);
		return r;
	}

	/*
	 * Calculates correct position to place player at and
	 * alerts the room of the new "intruder".
	 * 
	 * @param d Door to spawn player at.
	 */
	public void placePlayer(Door d)
	{
		Vector2 pos = d.gameObject.transform.position;
		float dist = 1.8f;
		if(d.positionType==0)
			pos.y -= dist;
		else if(d.positionType==1)
			pos.y += dist;
		else if(d.positionType==2)
			pos.x += dist;
		else if(d.positionType==3)
			pos.x -= dist;

		Room r = d.transform.parent.GetComponent<Room>();
		GameManager.player.transform.position = pos;
		r.playerEntered();
	}

	public void placePlayer(GameObject r)
	{
		GameManager.player.transform.position = r.transform.position;
	}
}
