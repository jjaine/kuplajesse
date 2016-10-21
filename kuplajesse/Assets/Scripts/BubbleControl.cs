using UnityEngine;
using System.Collections;

public class BubbleControl : MonoBehaviour {

	public Vector3 originalPosition;
	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		originalPosition = gameObject.transform.position;
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x - originalPosition.x < -3f || gameObject.transform.position.x - originalPosition.x > 3f) {
			rb.velocity = new Vector2 (0, 1);
			gameObject.tag = "nobubble";
		}
		if (gameObject.transform.position.y > 6) {
			Destroy (gameObject);
		}
			
	}
	void FixedUpdate ()
	{
		Physics2D.IgnoreLayerCollision (12, 12, true);
		Physics2D.IgnoreLayerCollision (12, 10, true);
		Physics2D.IgnoreLayerCollision (12, 9, gameObject.tag == "nobubble");
		Physics2D.IgnoreLayerCollision (12, 8, gameObject.tag == "bubble");
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;
		Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collider.GetComponent<Collider2D>(), collider.tag=="Player" && gameObject.tag == "nobubble");

		if (collider.tag == "enemy" && gameObject.tag == "bubble") {
			Destroy (gameObject);
		}
		if (collider.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
