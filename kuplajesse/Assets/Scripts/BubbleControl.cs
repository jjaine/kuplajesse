using UnityEngine;
using System.Collections;

public class BubbleControl : MonoBehaviour {

	public Vector3 originalPosition;
	public Rigidbody2D rb;
	public AudioSource audio;
	private SpriteRenderer ren;
    public Rigidbody2D candy;
    public float speed = 10f;
    private int random;
	private float r;
	public float shootTime = 4f;
	float shootTimeRemaining;
	private Animator anim;				


    // Use this for initialization
    void Start () {
		originalPosition = gameObject.transform.position;
		rb = GetComponent<Rigidbody2D>();
		audio = GetComponent<AudioSource>();
		ren = gameObject.GetComponent<SpriteRenderer> ();
		r = Random.Range(0f, 0.5f);
		shootTimeRemaining = shootTime;
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x - originalPosition.x < -3.5f || gameObject.transform.position.x - originalPosition.x > 3.5f) {
			rb.velocity = new Vector2 (0, 1);
			gameObject.tag = "nobubble";
		}
		if (gameObject.transform.position.x < -8.5f || gameObject.transform.position.x > 8.5f) {
			rb.velocity = new Vector2 (0, 1);
			gameObject.tag = "nobubble";
		}
			
	}
	void FixedUpdate ()
	{
		Physics2D.IgnoreLayerCollision (12, 12, true);
		Physics2D.IgnoreLayerCollision (13, 12, true);
		Physics2D.IgnoreLayerCollision (12, 10, true);

		Physics2D.IgnoreLayerCollision (12, 9, gameObject.tag == "nobubble");
		Physics2D.IgnoreLayerCollision (12, 8, gameObject.tag == "bubble");


		if (GetComponent<Rigidbody2D> ().position.y > (3.5f+r)){
			if (GetComponent<Rigidbody2D> ().position.x < -0.2f+r) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (1, 0);
			} else if (GetComponent<Rigidbody2D> ().position.x > 0.2f+r){
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1, 0);
			}
			else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

				if (shootTimeRemaining > 0)
					shootTimeRemaining -= Time.deltaTime;
				else {
					Destroy (gameObject);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{		

		Collider2D collider = col.collider;
		Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collider.GetComponent<Collider2D>(), collider.tag=="Player" && gameObject.tag == "nobubble");

		if (collider.tag == "enemy" && gameObject.tag == "bubble") {
			Destroy (gameObject);
		}
		if (collider.tag == "Player") {
			Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collider.GetComponent<Collider2D>());

			StartCoroutine("PlayAudioAndDie");
        }
	}

	public IEnumerator PlayAudioAndDie() {
		audio.Play();             //assuming it is selected on the audio

		ren.enabled = false;
		yield return new WaitForSeconds(1.2f);        //not sure if this is called right but you get the point
		Destroy(this.gameObject);
	}
}
