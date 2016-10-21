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
			rb.velocity = new Vector2 (0, 0);
			rb.gravityScale = -8;
		}
	}
	void FixedUpdate ()
	{
		Physics2D.IgnoreLayerCollision (12, 10, true);
		Physics2D.IgnoreLayerCollision (12, 8, true);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if (collider.tag == "enemy") {
			Destroy (gameObject);
		}
	}
}
