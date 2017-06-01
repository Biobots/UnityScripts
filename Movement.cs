using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

    static public float radius = 50;
    static public float height = 50;
    static public float MoveSpeed = 20;
    static public float RotateSpeed = 100;

    //static public float currentRadius = 50;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //float h = Input.GetAxis("Vertical");
        //transform.Translate(0, h * Time.deltaTime * MoveSpeed, 0);

        ////currentRadius = Vector3.Distance(transform.position, new Vector3(0, transform.position.y, 0));
        //float angle = (float)Math.PI * 2 / RotateSpeed;
        //float x = transform.position.x;
        //float z = transform.position.z;

        //if (Input.GetKey("a"))
        //{
        //    transform.position = new Vector3(CalculateX(z, x, -1 * angle), transform.position.y, CalculateZ(z, x, -1 * angle));
        //}
        //if (Input.GetKey("d"))
        //{
        //    transform.position = new Vector3(CalculateX(z, x, angle), transform.position.y, CalculateZ(z, x, angle));
        //}
        //if (Input.GetAxis("Mouse ScrollWheel") != 0)
        //{
        //    float s = Input.GetAxis("Mouse ScrollWheel") * 10;
        //    this.transform.Translate(0, 0, s * Time.deltaTime * MoveSpeed);
        //}

        //transform.LookAt(new Vector3(0, transform.position.y, 0));
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y;
        //Vector2 mX = Input.mouseScrollDelta;
        this.transform.Translate(x * Time.deltaTime * MoveSpeed, 0, z * Time.deltaTime * MoveSpeed);
        //移动
        if (Input.GetKey("q"))
        {
            this.transform.Rotate(0, RotateSpeed * Time.deltaTime, 0, Space.Self);
            print("turn left");
        }
        if (Input.GetKey("e"))
        {
            this.transform.Rotate(0, -1 * RotateSpeed * Time.deltaTime, 0, Space.Self);
            print("turn right");
        }
        //旋转
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            y = Input.GetAxis("Mouse ScrollWheel") * 50;
            this.transform.Translate(0, y * Time.deltaTime * MoveSpeed, 0);
        }
        //缩放
    }

    
}
