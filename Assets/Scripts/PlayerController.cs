using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Controls
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    // Components
    public Rigidbody2D rigidBody;
    public GameObject bullet;
    // Parameters
    public Vector3 bulletSpawnOffset;
    public float bulletSpeed;
    // Available equipment
    bool leftThrust;
    bool rightThrust;
    bool gun;
    bool shield;

    // Use this for initialization
    void Start () {
        leftThrust = false;
        rightThrust = false;
        gun = false;
        shield = false;
    }
	
	// Update is called once per frame
	void Update () {
        // Take user input
        // Movement
        if (Input.GetKeyDown(up))
        {
            Forward();
        }        
        if (Input.GetKeyDown(left))
        {
            LeftThrust();
        }
        if (Input.GetKeyDown(right))
        {
            RightThrust();
        }
        // Others
        if (Input.GetKeyDown(down))
        {
            Shoot();
        }
    }

    void Forward()
    {
        rigidBody.AddForce(transform.up, ForceMode2D.Impulse);
    }
    void LeftThrust()
    {
        if(leftThrust)
        {
            rigidBody.AddTorque(1, ForceMode2D.Impulse);
        }        
    }
    void RightThrust()
    {
        if(rightThrust)
        {
            rigidBody.AddTorque(-1, ForceMode2D.Impulse);
        }        
    }
    void Shoot()
    {
        if (gun)
        {
            // Instantiate projectile
            GameObject newBullet = (GameObject)Instantiate(bullet, transform.position + bulletSpawnOffset, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
            // Backwards thrust
            rigidBody.AddForce(-transform.up, ForceMode2D.Impulse);
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Collide with bullet
        if(other.gameObject.tag == "Bullet")
        {
            // Destroy player
            GameObject.Destroy(this.gameObject);
            // Destroy bullet
            GameObject.Destroy(other.gameObject);
        }
        // Collide with equipment
        if (other.gameObject.tag == "LaserGun")
        {
            // Set flag
            gun = true;
            // Destroy object
            GameObject.Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "LeftThrust")
        {
            // Set flag
            leftThrust = true;
            // Destroy object
            GameObject.Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "RightThrust")
        {
            // Set flag
            rightThrust = true;
            // Destroy object
            GameObject.Destroy(other.gameObject);
        }
    }
}
