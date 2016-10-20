using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float speed = 2f;		
	public int HP = 1;		
	public GameObject player;
	public bool grounded = true;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform front;			// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private bool flip = false;
	private bool collide = false;



	void Awake()
	{
		ren = gameObject.GetComponent<SpriteRenderer>();
//		front = transform.Find("frontCheck").transform;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if (collider.tag == "obstacle") { 
				Flip ();
				flip = true;
		}
		if(GetComponent<Rigidbody2D>().velocity.y == 0)
			grounded = true;
	}
		
	void FixedUpdate ()
	{
		Physics2D.IgnoreLayerCollision (9, 9, true);
		Physics2D.IgnoreLayerCollision (9, 10, GetComponent<Rigidbody2D> ().velocity.y > 0);


		if (GetComponent<Rigidbody2D>().position.y < -6)
			GetComponent<Rigidbody2D>().gameObject.transform.position = new Vector2(GetComponent<Rigidbody2D>().position.x, 6);

		if (grounded && GetComponent<Rigidbody2D> ().position.y < player.transform.position.y &&
		   (GetComponent<Rigidbody2D> ().position.x - player.transform.position.x < 0.05f && GetComponent<Rigidbody2D> ().position.x - player.transform.position.x > -0.05f)) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, 375f));
			grounded = false;
		}
		

		// move
		if (flip) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D>().velocity.y);	
			flip = false;
		}

		if(GetComponent<Rigidbody2D> ().velocity.y == 0)
			GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * speed, GetComponent<Rigidbody2D>().velocity.y);	
		else
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

		// kill
		//if(HP <= 0 && !dead)
		//	Death ();
	}
		

	void Death()
	{
		//change sprite to dead, later
		//ren.enabled = true;
		//ren.sprite = deadEnemy;

		// Set dead to true.
		dead = true;

	}


	public void Flip()
	{
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
