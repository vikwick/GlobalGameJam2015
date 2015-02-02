using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	public static float xv;
	public static float yv;
    Animator anim;
    public static float rotationangle = 0f;
	int walls = 0;

	void Start ()
	{
//		anim = GetComponent<Animator>();
		Vector2 v = (new Vector2 (ProjectileScript.xv, ProjectileScript.yv));
		v.Normalize();
		rigidbody2D.velocity = v*GameManager._player.maxProjSpeed;

//        if (_player.OGChickenVec.x == 0 && _player.OGChickenVec.y == 0){
//            rigidbody2D.velocity = _player.maxProjSpeed * new Vector2 (0, -1);
//            transform.rotation = Quaternion.Euler (new Vector3(0, 0, -90));
//        }
//        else {
//	    rotationangle = Mathf.Rad2Deg*Mathf.Atan2(_player.OGChickenVec.y, _player.OGChickenVec.x);
	    transform.rotation = Quaternion.Euler (new Vector3(0, 0, rotationangle));
    }

	void OnTriggerEnter2D(Collider2D c)
	{
        if(c.gameObject.tag == "Enemy")
        {
			EnemyController _enemy = c.gameObject.GetComponent<EnemyController>();
			if(_enemy != null)	_enemy.HP -= GameManager._player.ATK;
		}
		if (GameManager._player.projName != "SpiritBomb" && c.gameObject.tag != "Player" && c.gameObject.tag != "PlayerProjectile" && c.gameObject.tag != "EnemyProjectile")
		{
			Destroy(gameObject);
        }
    }

	void OnTriggerExit2D(Collider2D c)
	{
		if(GameManager._player.projName == "SpiritBomb" && c.gameObject.tag == "Walls" && walls == 2)
			Destroy(gameObject);
		else
			walls += 1;
		StartCoroutine(FinishHim());
	}

	/*
	 * Specifically for SpiritBomb. If a SB exits only one wall collider
	 * (thus not satisfying the conditional in OnTriggerExit2D), this
	 * time destruction method ensures its removal from the scene.
	 */
	IEnumerator FinishHim()
	{
		yield return new WaitForSeconds(1.75f);
		Destroy (gameObject);
	}
}
