using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public static GameObject dungeon;
	public static Dungeon _dungeon;
	public static int numEnemies = 4;
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
	    player = GameObject.FindGameObjectWithTag("Player");
		currRoom = player;
        _player = player.GetComponent<OGChickenController>();
		dungeon = GameObject.FindGameObjectWithTag("Dungeon");
<<<<<<< HEAD
		_dungeon = dungeon.GetComponent<Dungeon>();
//        enemy = GameObject.FindGameObjectWithTag("Enemy");
//        _enemy = enemy.GetComponent<EnemyController>();
        //door = GameObject.FindGameObjectWithTag("Door");
//		this.restart = GameObject.FindGameObjectWithTag("Restart");
//		this.restart.SetActive(false);
=======
       enemy = GameObject.FindGameObjectWithTag("Enemy");
       if (enemy != null){
            _enemy = enemy.GetComponent<EnemyController>();
        }

        //door = GameObject.FindGameObjectWithTag("Door");
		this.restart = GameObject.FindGameObjectWithTag("Restart");
        if (this.restart != null){
            this.restart.SetActive(false);     
        }


>>>>>>> CrisBranch

		int difficulty = 1;
		StartGame(difficulty);
   }

	void StartGame(int diff)
	{
		_dungeon.generateDungeon(diff);
		dungeon.transform.position = new Vector3(0,0,0);
		currRoom = _dungeon.start;
		_dungeon.placePlayer(currRoom);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currRoom.GetComponent<Room>().enemies.Count);
	    if(this.died()) {
<<<<<<< HEAD

=======
>>>>>>> ppleeease
			this.gameOver();
        }


		if(this.enemy!= null){
	        if (_enemy.HP <= (int)0){
	            Destroy (enemy);
	        }
		}

<<<<<<< HEAD
		cam.transform.position = new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -6f);
}
=======
//			this.gameOver();
		cam.transform.position = new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -6f);
//        if (_enemy.HP <= (int)0){
//            Destroy (enemy);
//        }
>>>>>>> ppleeease







	bool died(){
		return ((int)_player.currentHP <= 0 || (int)_player.infectedLevel == 100);
	}


	void gameOver(){
		//kill player
		Destroy(player);
		// death message
		this.deathMessage("You Were Murdered");
	}

	void deathMessage(string m){
		Message.instance.message = m;
		this.restart.SetActive(true);
	}

	void restartGame(){
		Application.LoadLevel("default");
	}
}
