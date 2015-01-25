using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameManager.player;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 p = gameObject.transform.parent.transform.position;
		Vector3 player2 = player.transform.position;
		Vector3 u = (player2 - p).normalized;
		this.rigidbody2D.AddForce(new Vector2(u.x, u.y)*50f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player"){
			Debug.Log ("Hit");
			GameManager._player.currentHP -= 15;
		}
	}
}
