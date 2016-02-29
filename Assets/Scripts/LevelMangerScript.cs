using UnityEngine;
using System.Collections;

public class LevelMangerScript : MonoBehaviour {

	public GameObject currentCheckpoint;
	public static LevelMangerScript levelManager;
	private PlayerControllerScript player;
	private PlayerHealth playerHealth;

  void Awake (){
		levelManager = this;
	}
	void Start () {
		player = FindObjectOfType<PlayerControllerScript>();
		playerHealth = FindObjectOfType<PlayerHealth>();
	}

	public void RespawnPlayer(){
		player.transform.position = currentCheckpoint.transform.position;
		playerHealth.isNotDead();
	}
}
