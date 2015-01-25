using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

	int difficulty;
	int maxRooms;
	ArrayList rooms = new ArrayList();
	Room[,] map;
	public GameObject start;

	// Use this for initialization
	void Start () {
//		generateDungeon(2);
//		GameManager.dungeon = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	 * @param start True if a start room, false if a normal room
	 */
	public void generateDungeon(int diff)
	{
		difficulty = diff;
		maxRooms = 8*difficulty;
		map = new Room[maxRooms, maxRooms];
		int x = Random.Range(0,maxRooms);
		int y = Random.Range(0,maxRooms);
		start = generateRoom(x,y);
		Room _room = start.GetComponent<Room>();
		_room.beaten = true;
		_room.start = true;
		_room.x = x;
		_room.y = y;
		map[x,y] = _room;
		rooms.Add(start);
		_room.transform.parent = transform;

		for(int i=1; i<maxRooms; i++)
		{
			int d = Random.Range(0,4);

			while(!contains(x,y) || (contains(x,y) && map[x,y] != null))
			{
				x = _room.x;
				y = _room.y;
				d = Random.Range(0,6);

				if(d==0)			// UP
					y++;
				else if(d>0 && d<3)	// DOWN
					y--;
				else if(d>2 && d<5)	// LEFT
					x--;
				else if(d==5)		// RIGHT
					x++;
			}
			rooms.Add(generateRoom(x, y));
			Room _newRoom = ((GameObject)rooms[i]).GetComponent<Room>();
			_newRoom.x = x;
			_newRoom.y = y;
			map[x,y] = _newRoom;
			_newRoom.transform.parent = transform;
			_room = _newRoom;
		}

		ArrayList deadRooms = new ArrayList();
		for(int i=0; i<rooms.Count; i++)
		{
			GameObject r = (GameObject)rooms[i];
			Room _r = r.GetComponent<Room>();
			if(surrounded (_r.x,_r.y) && r != start)
			{
				deadRooms.Add(r);
				rooms.Remove(r);
				map[_r.x,_r.y] = null;
				maxRooms--;
			}
		}
		int n = deadRooms.Count;
		for(int i=0; i<n; i++)
		{
			Destroy((GameObject)deadRooms[0]);
		}
		GameObject rap = (GameObject) rooms[0];
		Room _rap = rap.GetComponent<Room>();

		foreach(GameObject r in rooms)
		{
			Room _r = r.GetComponent<Room>();
			x = _r.x;
			y = _r.y;

			if(contains(x,y+1) && map[x,y+1]!=null)
			{
//				_r.createDoor(0, _r, map[x,y+1]);
				_r.createDoor(0);
			}
			if(contains(x,y-1) && map[x,y-1]!=null)
			{
//				_r.createDoor(1, _r, map[x,y-1]);
				_r.createDoor(1);
			}
			if(contains(x-1,y) && map[x-1,y]!=null)
			{
//				_r.createDoor(2, _r, map[x-1,y]);
				_r.createDoor(2);
			}
			if(contains(x+1,y) && map[x+1,y]!=null)
			{
//				_r.createDoor(3, _r, map[x+1,y]);
				_r.createDoor(3);
			}
			_r.populateItem();
		}
		foreach(GameObject r in rooms)
		{
			Room _r = r.GetComponent<Room>();
			x = _r.x;
			y = _r.y;
			for(int i=0; i<4; i++)
			{
				Door _door = _r.dirs[i]!=null ? _r.dirs[i].GetComponent<Door>() : null;
				if (_door != null) {

					if(i==0 && contains(x,y+1) && map[x,y+1]!=null)
					{
						_door.dst = map[x,y+1].dirs[1].GetComponent<Door>();
					}
					if(i==1 && contains(x,y-1) && map[x,y-1]!=null)
					{
						Debug.Log(map[x,y-1] + " " + map[x,y-1].dirs[0]);
						_door.dst = map[x,y-1].dirs[0].GetComponent<Door>();
					}
					if(i==2 && contains(x-1,y) && map[x-1,y]!=null)
					{
						_door.dst = map[x-1,y].dirs[3].GetComponent<Door>();
					}
					if(i==3 && contains(x+1,y) && map[x+1,y]!=null)
					{
						_door.dst = map[x+1,y].dirs[2].GetComponent<Door>();
					}
				}
			}
		}
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
		GameManager.player.transform.position = pos;
		Room r = d.transform.parent.GetComponent<Room>();
		if(!r.beaten)
		{
			Debug.Log ("alarm");
			r.populateEnemies(Random.Range(difficulty,difficulty*3));
			r.alarm ();
		}

	}

	public void placePlayer(GameObject r)
	{
		GameManager.player.transform.position = r.transform.position;
	}
}
