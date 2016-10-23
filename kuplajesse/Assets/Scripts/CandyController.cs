using UnityEngine;
using System.Collections;

public class CandyController : MonoBehaviour {

	public GameObject player;
	public AudioSource audio;
	private SpriteRenderer ren;

	void Awake () {
		audio = GetComponent<AudioSource>();
		ren = gameObject.GetComponent<SpriteRenderer> ();
		gameObject.GetComponent<ConstantForce2D> ().torque = 100;
	}

	void FixedUpdate () {
		if (GetComponent<Rigidbody2D>().position.y < -6)
			GetComponent<Rigidbody2D>().gameObject.transform.position = new Vector2(GetComponent<Rigidbody2D>().position.x, 6);
		
		Physics2D.IgnoreLayerCollision (14, 14, true);
		Physics2D.IgnoreLayerCollision (14, 13, true);		
		Physics2D.IgnoreLayerCollision (14, 12, true);

		if(gameObject.GetComponent<ConstantForce2D> ().torque > 0)
		gameObject.GetComponent<ConstantForce2D> ().torque -= 5;

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

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
