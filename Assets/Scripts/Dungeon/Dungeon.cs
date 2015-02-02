using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour
{
	int n = 16;		// necessary to space sprites out correctly (see: generateRoom())
	int difficulty;
	int maxRooms;
	ArrayList rooms = new ArrayList();
	Room[,] map;
	public GameObject start;
	Room bossRoom = null;

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
			GameObject room = (GameObject)rooms[i];
			Room _room = room.GetComponent<Room>();
			if(surrounded (_room.x,_room.y))
			{
				rooms.Remove(room);
				map[_room.x,_room.y] = null;
				i--;
				Destroy(room);
			}
			else
			{
				if((Random.Range(0f,1f)<.1f || i==rooms.Count-1) && bossRoom==null)
				{
					_room.convertToBossRoom();
					bossRoom = _room;
				}
			}
		}
	}

	/*
	 * Creates doors based on the positions of rooms in map[,],
	 * allows for items to randomly spawn in rooms (necessary to
	 * do here for efficiency), and also designates the start room
	 * (necessary to do here after rooms have been pruned and boss
	 * room has been spawned).
	 */
	void createDoors()
	{
		Room _start = null;
		while(_start==null || manhattanDistance(bossRoom, _start) != 1 )
		{
			start = (GameObject)rooms[Random.Range(0,rooms.Count)];
			_start = start.GetComponent<Room>();
		}
		_start.beaten = true;
		_start.GetComponent<Room>().start = true;

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

				if (_door != null) {
					
					if(i==0 && contains(x,y+1) && map[x,y+1]!=null)
					{
						_door.dst = map[x,y+1].dirs[1].GetComponent<Door>();
					}
					else if(i==1 && contains(x,y-1) && map[x,y-1]!=null)
					{
						_door.dst = map[x,y-1].dirs[0].GetComponent<Door>();
					}
					else if(i==2 && contains(x-1,y) && map[x-1,y]!=null)
					{
						_door.dst = map[x-1,y].dirs[3].GetComponent<Door>();
					}
					else if(i==3 && contains(x+1,y) && map[x+1,y]!=null)
					{
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

	int manhattanDistance(Room r1, Room r2)
	{
		return Mathf.Abs(r1.x-r2.x)+Mathf.Abs(r1.y-r2.y);
	}

	public bool contains(int x, int y)
	{
		return x < maxRooms && x >= 0 && y < maxRooms && y >= 0;
	}

	public GameObject generateRoom(int x, int y)
	{
		GameObject room = Resources.Load ("Prefabs/Room") as GameObject;
		room.GetComponent<Room>().x = x;
		room.GetComponent<Room>().y = y;
		room = Instantiate(room, new Vector2(n*x,n*y), transform.rotation) as GameObject;
		return room;
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
