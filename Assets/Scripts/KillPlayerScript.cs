using UnityEngine;
using System.Collections;

public class KillPlayerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			LevelMangerScript.levelManager.RespawnPlayer();
		}
	}
}
