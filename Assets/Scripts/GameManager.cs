using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public static GameObject dungeon;
	public static Dungeon _dungeon;
	public static int numEnemies = 4;
	public static int difficulty = 2;
    //GameObject enemy;
    //EnemyController _enemy;
    public static GameObject player;
    GameObject enemy;
    EnemyController _enemy;
	GameObject cam;
    //GameObject door;
    public static OGChickenController _player;
    //anim = GetComponent<Animator>();
	// Use this for initialization
	GameObject restart;
	public static GameObject currRoom;
	float zoom = 3.5f;

	void Start () {
		cam = GameObject.FindGameObjectWithTag("MainCamera");
//	    player = GameObject.FindGameObjectWithTag("Player");
		player = Instantiate (Resources.Load("Prefabs/OGChicken") as GameObject, transform.position, transform.rotation) as GameObject;
		currRoom = player;
        _player = player.GetComponent<OGChickenController>();
		dungeon = GameObject.FindGameObjectWithTag("Dungeon");
		_dungeon = dungeon.GetComponent<Dungeon>();
//        enemy = GameObject.FindGameObjectWithTag("Enemy");
//        _enemy = enemy.GetComponent<EnemyController>();
        //door = GameObject.FindGameObjectWithTag("Door");
//		this.restart = GameObject.FindGameObjectWithTag("Restart");
//		this.restart.SetActive(false);
		StartGame(GameManager.difficulty);
   }

	void StartGame(int diff)
	{
		_dungeon.generateDungeon(diff);
		currRoom = _dungeon.start;
		_dungeon.placePlayer(currRoom);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if(this.died()) {
			this.gameOver();
        }

		cam.transform.position = new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -6f);
	}


	bool died(){
		return (_player.currentHP <= 0 || _player.infectedLevel == 100);
	}


	void gameOver(){
		//kill player
		Destroy(player);
		currRoom.GetComponent<Room>().reset();
		// death message
		this.deathMessage("You Were Murdered");
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
