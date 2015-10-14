using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.Space))
        {
            if ((Application.loadedLevel + 1) < (Application.levelCount))
                Application.LoadLevel(Application.loadedLevel + 1);
            else
                Application.LoadLevel(0);
        }
	}
}
