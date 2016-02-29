using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

	public int playerDamage = 20;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" ){
			other.GetComponent<PlayerHealth>().TakeDamage(playerDamage);
		}
	}
}
