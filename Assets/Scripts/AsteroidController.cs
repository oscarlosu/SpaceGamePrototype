using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
    private float xBorder = 43f;
    private float yBorder = 27f;
    private bool cooldown = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < -xBorder || transform.position.x > xBorder)
        {
            if (!cooldown)
            {
                float newY;
                if (transform.position.x < 0)
                {
                    newY = xBorder;
                }
                else
                {
                    newY = -xBorder;
                }
                transform.position = new Vector2(newY, transform.position.y);
                cooldown = true;
                StartCoroutine("CooldownTimer");
            }
        }

        if (transform.position.y < -yBorder || transform.position.y > yBorder)
        {
            if (!cooldown)
            {
                float newY;
                if (transform.position.y < 0)
                {
                    newY = yBorder;
                }
                else
                {
                    newY = -yBorder;
                }
                transform.position = new Vector2(transform.position.x, newY);
                cooldown = true;
                StartCoroutine("CooldownTimer");
            }
        }
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

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(1.0f);
        cooldown = false;
    }
}
