using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    GameObject player;
    //GameObject enemy;
    //EnemyController _enemy;
    //GameObject door;
    OGChickenController _player;
    //anim = GetComponent<Animator>();
	// Use this for initialization
	void Start () {
	    player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<OGChickenController>();
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        //door = GameObject.FindGameObjectWithTag("Door");

   }
	
	// Update is called once per frame
	void Update () {
	    if(this.died()) {
			this.gameOver();
        }
        //if ((int)_enemy.enemyHP == 0){
            //Destroy (enemy);
        //}
        //if ((int)_enemy.currentEnemyCount == 0){
            //Destroy(door);
            //anim.SetBool(enemiesClear, true);
        //}
	}


	bool died(){
		return ((int)_player.currentHP == 0 || (int)_player.infectedLevel == 10);
	}


	void gameOver(){
		//kill player
		Destroy(player);
		// make ui show up
		this.UIMessage("You Were Murdered");
	}

	void UIMessage(string m){
		Message.instance.message = m;
	}
}
