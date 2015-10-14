using UnityEngine;
using System.Collections;

public class controllerTest : MonoBehaviour {
    public bool player1 = true;

    private string horizontal = "";
    private string vertical = "";

	// Use this for initialization
	void Start () {
        if(player1)
        {
            horizontal = "p1Horizontal";
            vertical = "p1Vertical";
        }
        else
        {
            horizontal = "p2Horizontal";
            vertical = "p2Vertical";
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(Input.GetAxis(horizontal) * 5 * Time.deltaTime, Input.GetAxis(vertical)*5 * Time.deltaTime, 0);
	}
}
