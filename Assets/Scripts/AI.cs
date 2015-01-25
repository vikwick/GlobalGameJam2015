using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	Dungeon dungeon;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * @param start True if a start room, false if a normal room
	 */
	void generateDungeon()
	{
		// generate start room
		
		// instantiate specific prefab for room
		// set GameManager currentRoom
		generateEnemies();
		// conditionally add items
		
		
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
