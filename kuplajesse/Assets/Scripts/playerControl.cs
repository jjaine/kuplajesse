using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerControl : MonoBehaviour {

    public bool jump = false;
	public bool fall = false;
    public bool facing = true; 
	public Vector3 originalPosition;
	public float GroundDistance;
	public bool grounded = true;
	public bool killed = false;
	public float killTime = 3f;
	float killTimeRemaining;
	private SpriteRenderer ren;

    //forces for moving
    public float jumpForce = 500f;
    public float moveForce = 200f;
    public float maxSpeed = 3f;

	public AudioSource audio;
	public AudioSource audio2;
	public Animator anim;		
	public Text countText;
	private int count;
	public Text livesText;
	private int lives;

    // Use this for initialization
    void Start () {
		originalPosition = gameObject.transform.position;
		killTimeRemaining = killTime;
		ren = gameObject.GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator>();
		count = 0;
		countText.text = "Score: " + count.ToString ();
		lives = 2;
		livesText.text = lives.ToString ();
	}

	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetButtonDown ("Jump")) {
			audio.Play ();
			anim.SetBool ("Jump", true);
			anim.SetBool ("Grounded", false);

			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));

			grounded = false;
		}
		anim.SetFloat ("Velocity", GetComponent<Rigidbody2D> ().velocity.y);

		if (GetComponent<Rigidbody2D> ().velocity.y < 0) {
			anim.SetBool ("Jump", false);
		}
		if (grounded) {
			anim.SetBool ("Grounded", true);
			anim.SetBool ("Jump", false);
		}
			
		if (killed && killTimeRemaining > 0) {
			killTimeRemaining -= Time.deltaTime;
		}
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
		anim.SetFloat("Speed", Mathf.Abs(h));

		if (h * GetComponent<Rigidbody2D> ().velocity.x < maxSpeed){
				GetComponent<Rigidbody2D> ().AddForce (Vector2.right * h * moveForce);
			}
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) >= maxSpeed)
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
		Collider2D collider = col.collider;

		if(GetComponent<Rigidbody2D>().velocity.y == 0 && (collider.tag == "platform" || collider .tag == "ground"))
			grounded = true;

		if (collider.tag == "enemy") { 
			audio2.Play ();
			killed = true;
			anim.SetBool ("Die", true);
			if(lives > 0)
			lives -= 1;
			livesText.text = lives.ToString ();

			StartCoroutine("AnimationFirst");

		}
		Debug.Log (collider.tag);
		if (collider.tag == "candy" || collider.tag == "deadenemy")
		{
			count = count + 1000;
			Debug.Log (count);

			countText.text = "Score: " + count.ToString ();
		}
		if (collider.tag == "nobubble")
		{
			count = count + 10;
			countText.text = "Score: " + count.ToString ();
		}
	}

	public IEnumerator AnimationFirst() {
		yield return new WaitForSeconds(0.5f);

		gameObject.transform.position = originalPosition;
		anim.SetBool ("Die", false);

	}
}
