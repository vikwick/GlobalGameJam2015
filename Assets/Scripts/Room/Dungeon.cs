using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

	int difficulty;
	int maxRooms;
	ArrayList rooms = new ArrayList();
	Room[,] map;

	// Use this for initialization
	void Start () {
		generateDungeon(4);
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
		GameObject start = generateRoom(x,y);
		Room _room = start.GetComponent<Room>();
		_room.x = x;
		_room.y = y;
		map[x,y] = _room;
		rooms.Add(start);
		_room.transform.parent = transform;

		for(int i=1; i<maxRooms; i++)
		{
			int d = Random.Range(0,4);
//			Debug.Log (x + " " + y);

			while(!contains(x,y) || (contains(x,y) && map[x,y] != null && !surrounded(x,y)))
			{
				x = _room.x;
				y = _room.y;
				d = Random.Range(0,6);

				if(d==0)		// UP
					y++;
				else if(d>0 && d<3)	// DOWN
					y--;
				else if(d>2 && d<5)	// LEFT
					x--;
				else if(d==5)	// RIGHT
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
//		r.transform.parent = GameManager.dungeon.transform;
	}

	public bool surrounded(int x, int y)
	{
		if(contains(x,y) && map[x+1,y]!=null && map[x-1,y]!=null && map[x,y+1]!=null && map[x,y-1]!=null && map[x+1,y+1]!=null && map[x-1,y+1]!=null && map[x+1,y-1]!=null && map[x-1,y-1]!=null)
			return true;
		return false;
	}

	public bool contains(int x, int y)
	{
		return x < maxRooms && x >= 0 && y < maxRooms && y >= 0;
	}

	public GameObject generateRoom(int x, int y)
	{
		GameObject r = Instantiate(Resources.Load ("Prefabs/Room", typeof(GameObject)), new Vector2(x,y), GameManager.dungeon.transform.rotation) as GameObject;
		Room _r = r.GetComponent<Room>();
		return r;
	}
	
	void generateEnemies()
	{
		int r = Random.Range(1,8);
		for(int i=0; i<r; i++)
		{
			// instantiate random enemies
			// set currentRoom's enemies[]
		}
	}
	
	void generateItems()
	{
		
	}
}
