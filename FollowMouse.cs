using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane - 950));
    }
}
