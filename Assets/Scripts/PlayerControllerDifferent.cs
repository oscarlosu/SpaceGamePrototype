using UnityEngine;
using System.Collections;

public class PlayerControllerDifferent : MonoBehaviour {
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    // Components
    public Rigidbody2D rigidbody;
    public GameObject bullet;
    public SpriteRenderer shield;
    // Parameters
    public float bulletSpawnOffset;
    public float bulletSpeed;
    // Available equipment
    bool leftThrust;
    bool rightThrust;
    bool gun;
    bool shieldsUp;
    private bool cooldown = false;
    private bool gunCooldown = false;

    private float xBorder = 43f;
    private float yBorder = 27f;
    // Use this for initialization
    void Start () {
        rigidbody.AddForceAtPosition(transform.position * 500f, Vector2.zero);
        shield.enabled = false;
        leftThrust = false;
        rightThrust = false;
        gun = false;
        shieldsUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 force = (Vector2.zero - new Vector2(transform.position.x, transform.position.y)).normalized;
        rigidbody.AddForce(force * (Mathf.Abs(Vector2.Distance(new Vector2(xBorder-transform.position.x, yBorder - transform.position.y),Vector2.zero))/4));

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
        //rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(transform.up * 10f, ForceMode2D.Impulse);
    }
    void LeftThrust()
    {
        if (leftThrust)
        {
            if (rigidbody.angularVelocity < 0)
                rigidbody.angularVelocity = 0;
            else
                rigidbody.AddTorque(1f, ForceMode2D.Impulse);
        }
    }
    void RightThrust()
    {
        if (rightThrust)
        {
            if (rigidbody.angularVelocity > 0)
                rigidbody.angularVelocity = 0;
            else
                rigidbody.AddTorque(-1f, ForceMode2D.Impulse);
        }
    }
    void Shoot()
    {
        if (gun && !gunCooldown)
        {
            // Instantiate projectile
            GameObject newBullet = (GameObject)Instantiate(bullet, transform.position + transform.forward * bulletSpawnOffset, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * (bulletSpeed + rigidbody.velocity.magnitude), ForceMode2D.Impulse);
            // Backwards thrust
            //rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(-transform.up * 2.5f, ForceMode2D.Impulse);
            gunCooldown = true;
            StartCoroutine("GunCooldownFuntion");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Collide with bullet
        if (other.gameObject.tag == "Bullet")
        {
            // Destroy player
            if (shieldsUp)
            {
                shieldsUp = false;
                shield.enabled = false;
            }
            else
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
        if (other.gameObject.tag == "Shield")
        {
            shieldsUp = true;
            shield.enabled = true;
            GameObject.Destroy(other.gameObject);
        }
    }

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(1.0f);
        cooldown = false;
    }

    IEnumerator GunCooldownFuntion()
    {
        yield return new WaitForSeconds(0.5f);
        gunCooldown = false;
    }
}
