using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // Collide with bullet
        if (other.gameObject.tag == "Bullet")
        {
            // Destroy asteroid
            GameObject.Destroy(this.gameObject);
            // Destroy bullet
            GameObject.Destroy(other.gameObject);
        }
    }
}
