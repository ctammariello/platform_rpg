using UnityEngine;
using System.Collections;

public class LevelMangerScript : MonoBehaviour {

	public GameObject currentCheckpoint;
	public static LevelMangerScript levelManager;
	private PlayerControllerScript player;

    void Awake (){
		levelManager = this;
	}
	void Start () {
		player = FindObjectOfType<PlayerControllerScript>();
	}

	public void RespawnPlayer(){
		player.transform.position = currentCheckpoint.transform.position;
	}
}
