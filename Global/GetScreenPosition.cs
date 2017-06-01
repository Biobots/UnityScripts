using UnityEngine;
using System.Collections;

public class GetScreenPosition : MonoBehaviour {

    private Camera theCamera;
    public Transform tx;
	// Use this for initialization
	void Start () {
	if(!theCamera)
        {
            theCamera = Camera.main;
        }
        tx = theCamera.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public Vector3 GetCenter(float distance)
    {
        Vector3[] corners = GetCorners(distance);
        return new Vector3((corners[0].x + corners[1].x + corners[2].x + corners[3].x) / 4, (corners[0].y + corners[1].y + corners[2].y + corners[3].y) / 4, (corners[0].z + corners[1].z + corners[2].z + corners[3].z) / 4);
    }
    Vector3[] GetCorners(float distance)
    {
        Vector3[] corners = new Vector3[4];
        float halfFOV = (theCamera.fieldOfView * 0.5f) * Mathf.Deg2Rad;
        float aspect = theCamera.aspect;
        float height = distance * Mathf.Tan(halfFOV);
        float width = height * aspect;
        corners[0] = tx.position - (tx.right * width);
        corners[0] += tx.up * height;
        corners[0] += tx.forward * distance;

        corners[1] = tx.position + (tx.right * width);
        corners[1] += tx.up * height;
        corners[1] += tx.forward * distance;

        corners[2] = tx.position - (tx.right * width);
        corners[2] -= tx.up * height;
        corners[2] += tx.forward * distance;

        corners[3] = tx.position + (tx.right * width);
        corners[3] -= tx.up * height;
        corners[3] += tx.forward * distance;

        return corners;
    }
}
