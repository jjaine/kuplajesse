using UnityEngine;
using System.Collections;

public class platformOneWay : MonoBehaviour {
    public BoxCollider2D platform;
    public bool oneWay = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        platform = transform.parent.GetComponent<BoxCollider2D>();
        platform.enabled = !oneWay;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Player")
			oneWay = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag == "Player")
			oneWay = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        oneWay = false;
    }
}
