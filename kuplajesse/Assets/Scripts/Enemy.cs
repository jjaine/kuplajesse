using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float speed = 2f;		
	public int HP = 1;		
	public GameObject player;
	public GameObject bubble;
	public GameObject deadEnemy1;
	public GameObject deadEnemy2;
	public bool grounded = true;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform front;			// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private bool flip = false;
	private bool collide = false;
	private int random;


	void Awake()
	{
		ren = gameObject.GetComponent<SpriteRenderer> ();
//		front = transform.Find("frontCheck").transform;
		Physics2D.IgnoreCollision(bubble.GetComponent<Collider2D>(), GetComponent<Collider2D>(), bubble.tag=="nobubble");
		random = (int)Random.Range(0.0f, 1.9f);
		if (random == 1)
			Flip ();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if (collider.tag == "obstacle") { 
				Flip ();
				flip = true;
		}
		if (collider.tag == "bubble") {
			dead = true;
		}

		if(GetComponent<Rigidbody2D>().velocity.y == 0)
			grounded = true;
	}
		
	void FixedUpdate ()
	{
		Physics2D.IgnoreLayerCollision (9, 9, true);
		Physics2D.IgnoreLayerCollision (9, 10, GetComponent<Rigidbody2D> ().velocity.y > 0);


			if (GetComponent<Rigidbody2D> ().position.y < -6)
				GetComponent<Rigidbody2D> ().gameObject.transform.position = new Vector2 (GetComponent<Rigidbody2D> ().position.x, 6);
			if (GetComponent<Rigidbody2D> ().position.x < -8.6f)
				GetComponent<Rigidbody2D> ().gameObject.transform.position = new Vector2 (0, 6);

			if (!dead && grounded && GetComponent<Rigidbody2D> ().position.y < player.transform.position.y &&
			   (GetComponent<Rigidbody2D> ().position.x - player.transform.position.x < 0.05f && GetComponent<Rigidbody2D> ().position.x - player.transform.position.x > -0.05f)) {
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, 375f));
				grounded = false;
			}
		

			// move
			if (flip) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);	
				flip = false;
			}

			if (!dead && GetComponent<Rigidbody2D> ().velocity.y == 0)
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.localScale.x * speed, GetComponent<Rigidbody2D> ().velocity.y);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);

			// kill
			if (dead)
				Death ();
		}
		

	void Death()
	{
		Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), bubble.GetComponent<Collider2D>());
		//gameObject.transform.position = new Vector3(0,0,0);

		//change sprite to dead, later
		ren.enabled = true;
		Destroy (gameObject);

		if (ren.name == "blob" || ren.name == "blob2") {
			GameObject deadEnemyInstance = Instantiate (deadEnemy1, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		} else { 
			GameObject deadEnemyInstance = Instantiate (deadEnemy2, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		}
		// Set dead to true.
		dead = true;
		gameObject.tag = "deadenemy";

	}


	public void Flip()
	{
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
