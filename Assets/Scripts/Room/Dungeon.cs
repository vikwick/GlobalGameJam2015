using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

	static int difficulty;
	static ArrayList rooms = new ArrayList();

	// Use this for initialization
	void Start () {
		Dungeon.generateDungeon(2);
		GameManager.dungeon = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * @param start True if a start room, false if a normal room
	 */
	public static void generateDungeon(int diff)
	{
		difficulty = diff;
		int maxRooms = 8*difficulty;
		Room[,] map = new Room[maxRooms, maxRooms];
		int x = Random.Range(0,maxRooms);
		int y = Random.Range(0,maxRooms);
		GameObject start = generateRoom(x,y);
		Room _room = start.GetComponent<Room>();
		_room.x = x;
		_room.y = y;
		map[x,y] = _room;
		rooms.Add(start);

		for(int i=1; i<maxRooms; i++)
		{
			int d = Random.Range(0,4);
//			Debug.Log (x + " " + y);

			while(!(x < maxRooms && x >= 0 && y < maxRooms && y >= 0) || ((x < maxRooms && x >= 0 && y < maxRooms && y >= 0) && map[x,y] != null))
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
			_room = _newRoom;
		}
		return;
	}

	public static GameObject generateRoom(int x, int y)
	{
		GameObject r = Instantiate(Resources.Load ("Prefabs/Room", typeof(GameObject)), new Vector2(x*15,y*10), GameManager.dungeon.transform.rotation) as GameObject;
		r.transform.parent = GameManager.dungeon.transform;
		Room _r = r.GetComponent<Room>();
		return r;
	}
	
	static void generateEnemies()
	{
		int r = Random.Range(1,8);
		for(int i=0; i<r; i++)
		{
			// instantiate random enemies
			// set currentRoom's enemies[]
		}
	}
	
	static void generateItems()
	{
		
	}
}
