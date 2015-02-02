using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsUI : MonoBehaviour {

	Text text;

	void Start ()
	{
		text = GetComponent<Text>();
	}

	void Update ()
	{
//		if(GameManager._player.tab)
			text.text = "HP: " + GameManager._player.HP + "\nATK: " + GameManager._player.ATK +
				"\nSPD: " + GameManager._player.maxSpeed + "\nATKSPD: " + GameManager._player.maxProjSpeed;
	}
}
