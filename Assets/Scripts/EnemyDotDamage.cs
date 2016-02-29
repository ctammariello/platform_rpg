using UnityEngine;
using System.Collections;

public class EnemyDotDamage : MonoBehaviour {
	public int playerDotDamage = 2;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" ){
			other.GetComponent<PlayerHealth>().TakeDotDamage(playerDotDamage);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player" ){
			other.GetComponent<PlayerHealth>().IsNotDot();
		}
	}
}
