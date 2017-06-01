using UnityEngine;
using System;
using System.Collections;

public class RoundMovement : MonoBehaviour {
    static public float radius = 50;
    static public float height = 50;
    static public float MoveSpeed = 20;
    static public float RotateSpeed = 5;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Vertical");
        transform.Translate(0, h * Time.deltaTime * MoveSpeed, 0);

        //currentRadius = Vector3.Distance(transform.position, new Vector3(0, transform.position.y, 0));
        float angle = (float)Math.PI * 2 / (RotateSpeed/Time.deltaTime);
        float x = transform.position.x;
        float z = transform.position.z;

        if (Input.GetKey("a"))
        {
            transform.position = new Vector3(CalculateX(z, x, -1 * angle), transform.position.y, CalculateZ(z, x, -1 * angle));
        }
        if (Input.GetKey("d"))
        {
            transform.position = new Vector3(CalculateX(z, x, angle), transform.position.y, CalculateZ(z, x, angle));
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float s = Input.GetAxis("Mouse ScrollWheel") * 20;
            this.transform.Translate(0, 0, s * Time.deltaTime * MoveSpeed * 5);
        }

        transform.LookAt(new Vector3(0, transform.position.y, 0));
    }
    float CalculateZ(float z, float x, float angle)
    {
        return (float)(z * Math.Cos(angle) + x * Math.Sin(angle));
    }
    float CalculateX(float z, float x, float angle)
    {
        return (float)(x * Math.Cos(angle) - z * Math.Sin(angle));
    }
}
