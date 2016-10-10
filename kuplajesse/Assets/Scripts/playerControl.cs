using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {

    public bool jump = false;

    public bool facing = true; // use later for setting the player character to correct direction

    //forces for moving
    public float jumpForce = 500f;
    public float moveForce = 200f;
    public float maxSpeed = 3f;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
            jump = true;
    }

    void FixedUpdate ()
    {
        float h = Input.GetAxis("Horizontal");

        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) >= maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (h > 0 && !facing) ;
        // implement later Flip();
        else if (h < 0 && facing) ;
        // implement later Flip();

        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
}
