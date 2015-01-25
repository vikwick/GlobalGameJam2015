using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    GameObject player;
	public static GameObject dungeon;
    GameObject enemy;
    EnemyController _enemy;

    //GameObject door;
    OGChickenController _player;
    //anim = GetComponent<Animator>();
	// Use this for initialization
	GameObject restart;
	void Start () {
	    player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<OGChickenController>();
		dungeon = GameObject.FindGameObjectWithTag("Dungeon");
       enemy = GameObject.FindGameObjectWithTag("Enemy");
       _enemy = enemy.GetComponent<EnemyController>();
        //door = GameObject.FindGameObjectWithTag("Door");
		this.restart = GameObject.FindGameObjectWithTag("Restart");
		this.restart.SetActive(false);


   }
	
	// Update is called once per frame
	void Update () {
	    if(this.died()) {
			this.gameOver();
        }
        if (_enemy.HP <= (int)0){
            Destroy (enemy);
        }
        //if ((int)_enemy.currentEnemyCount == 0){
            //Destroy(door);
            //anim.SetBool(enemiesClear, true);
        //}
	}


	bool died(){
		return ((int)_player.currentHP == 0 || (int)_player.infectedLevel == 100);
	}


	void gameOver(){
		//kill player
		Destroy(player);
		// death message
		this.deathMessage("You Were Murdered");
	}

	void deathMessage(string m){
		Message.instance.message = m;
//		this.restart.SetActive(true);
	}

	void restartGame(){
		Application.LoadLevel("default");
	}
}
