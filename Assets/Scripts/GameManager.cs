using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	
	public static GameObject dungeon;
	public static Dungeon _dungeon;
	public static int numEnemies = 4;
	public static int difficulty = 2;
	public static GameObject player;
	GameObject cam;
	public static OGChickenController _player;
	public static GameObject currRoom;
	float zoom = 3.5f;
	GameObject stats;
	
	void Start ()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		stats = GameObject.FindGameObjectWithTag("StatsUI");
		player = Instantiate (Resources.Load("Prefabs/OGChicken") as GameObject, transform.position, transform.rotation) as GameObject;
		currRoom = player;
		_player = player.GetComponent<OGChickenController>();
		dungeon = GameObject.FindGameObjectWithTag("Dungeon");
		_dungeon = dungeon.GetComponent<Dungeon>();
		StartGame(GameManager.difficulty);
	}
	
	void StartGame(int diff)
	{
		_dungeon.generateDungeon(diff);
		currRoom = _dungeon.start;
		_dungeon.placePlayer(currRoom);
	}

	void Update ()
	{
		if(this.died()) {
			this.gameOver();
		}

		/* Set stats UI to active or unactive */
		if(Input.GetKeyDown(KeyCode.Tab))
			stats.SetActive(!stats.activeInHierarchy);
		
		cam.transform.position = new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -6f);
	}
	
	
	bool died()
	{
		return (_player.HP <= 0 || _player.infectedLevel == 100);
	}
	
	
	void gameOver()
	{
		Destroy(player);
		currRoom.GetComponent<Room>().reset();
//		this.deathMessage("You Were Murdered");
		currRoom = _dungeon.start;
		player = Instantiate (Resources.Load("Prefabs/OGChicken") as GameObject, currRoom.transform.position, currRoom.transform.rotation) as GameObject;
		_player = player.GetComponent<OGChickenController>();
		_dungeon.placePlayer(currRoom);
	}
	
	void deathMessage(string m){
		/* REDO? */
	}
	
	//	void restartGame(){
	//		Application.LoadLevel("default");
	//	}
}
