using UnityEngine;
using System.Collections;

public class ShootBubble : MonoBehaviour
{
	public Rigidbody2D bubble;				
	public float speed = 20f;	
	public float shootTime = 1f;
	float shootTimeRemaining;
	bool shot = false;

	private playerControl playerCtrl;	

	void Start () {
		shootTimeRemaining = shootTime;
	}

	void Awake() {
		playerCtrl = transform.root.GetComponent<playerControl>();

	}

	void FixedUpdate() {
		if (shot && shootTimeRemaining > 0)
			shootTimeRemaining -= Time.deltaTime;
		else {
			shootTimeRemaining = shootTime;
			shot = false;
		}
	}

	void Update ()
	{
		if (shot && shootTimeRemaining > 0)
			shootTimeRemaining -= Time.deltaTime;
		else {
			shootTimeRemaining = shootTime;
			shot = false;
		}

		if(!shot && Input.GetButtonDown("Fire1")) {
			shot = true;
			GetComponent<AudioSource>().Play();

			if(playerCtrl.facing) {
				Rigidbody2D bulletInstance = Instantiate(bubble, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);
				bulletInstance.gravityScale = 0;
			}
			else {
				Rigidbody2D bulletInstance = Instantiate(bubble, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-speed, 0);
				bulletInstance.gravityScale = 0;

			}
		}
	}
}
