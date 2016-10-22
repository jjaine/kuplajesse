using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {

    public bool jump = false;
    public bool facing = true; // use later for setting the player character to correct direction
	public Vector3 originalPosition;
	public float GroundDistance;
	public bool grounded = true;
	public bool killed = false;
	public float killTime = 3f;
	float killTimeRemaining;


    //forces for moving
    public float jumpForce = 500f;
    public float moveForce = 200f;
    public float maxSpeed = 3f;

	public AudioSource audio;
	public AudioSource audio2;


    // Use this for initialization
    void Start () {
		originalPosition = gameObject.transform.position;
		killTimeRemaining = killTime;
	}

	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetButtonDown ("Jump")) {
			audio.Play ();
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
			grounded = false;
		}    
		if (killed && killTimeRemaining > 0)
			killTimeRemaining -= Time.deltaTime;
		else {
			killTimeRemaining = killTime;
			killed = false;
		}
	}

    void FixedUpdate ()
    {
		Physics2D.IgnoreLayerCollision (8, 10, GetComponent<Rigidbody2D> ().velocity.y > 0);
		Physics2D.IgnoreLayerCollision (8, 9, killed);


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

	//kill player on enemy contact, check if grounded 

	void OnCollisionEnter2D(Collision2D col)
	{
		if(GetComponent<Rigidbody2D>().velocity.y == 0)
			grounded = true;
		Collider2D collider = col.collider;

		if (collider.tag == "enemy") { 
			audio2.Play ();
			killed = true;
			gameObject.transform.position = originalPosition;
		}
	}


}
