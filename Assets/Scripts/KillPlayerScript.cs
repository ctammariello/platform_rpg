using UnityEngine;
using System.Collections;

public class KillPlayerScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
		//levelManager = FindObjectOfType<LevelMangerScript>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			LevelMangerScript.levelManager.RespawnPlayer();
		}
	}
}
