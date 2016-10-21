using UnityEngine;
using System.Collections;

public class ShootBubble : MonoBehaviour
{
	public Rigidbody2D bubble;				
	public float speed = 20f;		

	private playerControl playerCtrl;	

	void Awake() {
		playerCtrl = transform.root.GetComponent<playerControl>();
	}


	void Update ()
	{
		if(Input.GetButtonDown("Fire1")) {
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
