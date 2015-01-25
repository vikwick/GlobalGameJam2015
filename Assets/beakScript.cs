using UnityEngine;
using System.Collections;
using System;

public class beakScript : MonoBehaviour {
    GameObject enemy;
    EnemyController _enemy;
    // Use this for initialization
    void Start () {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            _enemy = enemy.GetComponent<EnemyController>();
    }
    
    void OnTriggerExit2D(Collider2D b){
        Debug.Log(b.gameObject.tag);
        if(b.gameObject.tag == "Player"){
            _enemy.HP -= (int)5;
        }
    }
}   