using UnityEngine;
using System.Collections;

public class DeadEnemyController : MonoBehaviour {

	public GameObject player;

	void Awake () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 1);
	}
	
	void FixedUpdate () {
		if (GetComponent<Rigidbody2D> ().position.y > 3.5f)
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if (collider.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
