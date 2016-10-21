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
}
