using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {

    public bool jump = false;
    public bool facing = true; // use later for setting the player character to correct direction
	public Vector3 originalPosition;

    //forces for moving
    public float jumpForce = 500f;
    public float moveForce = 200f;
    public float maxSpeed = 3f;

	public AudioSource audio;

    // Use this for initialization
    void Start () {
		audio = GetComponent<AudioSource>();
		originalPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
            jump = true;
    }

    void FixedUpdate ()
    {
        if (GetComponent<Rigidbody2D>().position.y < -6)
            GetComponent<Rigidbody2D>().gameObject.transform.position = new Vector2(GetComponent<Rigidbody2D>().position.x, 6);

        float h = Input.GetAxis("Horizontal");

		if (h * GetComponent<Rigidbody2D> ().velocity.x < maxSpeed){
			if (GetComponent<Rigidbody2D> ().velocity.y == 0)
				GetComponent<Rigidbody2D> ().AddForce (Vector2.right * h * moveForce);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
			}
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) >= maxSpeed)
			if (GetComponent<Rigidbody2D> ().velocity.y == 0)
            	GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (h > 0 && !facing) 
            Flip();
        else if (h < 0 && facing) 
            Flip();

        if (jump)
        {
			audio.Play ();
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facing = !facing;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	//kill player on enemy contact
	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;
		Vector3 contactPoint = col.contacts[0].point;
		Vector3 center = collider.bounds.center;

		if (collider.tag == "enemy") { 
			gameObject.transform.position = originalPosition;
		}
	}

}
