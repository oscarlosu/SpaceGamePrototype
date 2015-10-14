using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine("TurnOn");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(0.05f);
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
