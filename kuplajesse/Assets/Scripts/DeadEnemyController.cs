using UnityEngine;
using System.Collections;

public class DeadEnemyController : MonoBehaviour {

	public GameObject player;
	public AudioSource audio;
	private SpriteRenderer ren;


	void Awake () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 1);
		audio = GetComponent<AudioSource>();
		ren = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	void FixedUpdate () {
		Physics2D.IgnoreLayerCollision (13, 9, true);		
		Physics2D.IgnoreLayerCollision (13, 13, true);
		Physics2D.IgnoreLayerCollision (13, 10, true);

		if (GetComponent<Rigidbody2D> ().position.y > 3.5f)
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
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
