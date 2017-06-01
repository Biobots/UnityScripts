using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

public class AlphaAttachment : MonoBehaviour {
    GameEngine engine;

    private float SpeedX = 240;
    private float SpeedY = 120;

    private float MinLimitY = -180;
    private float MaxLimitY = 180;

    private float mX = 0.0F;
    private float mY = 0.0F;

    public float Damping = 10F;

    // Use this for initialization
    void Start () {
        engine = Camera.main.GetComponent<GameEngine>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetMouseButton(1))
        //{
        //    mX += Input.GetAxis("Mouse X") * SpeedX * 0.02F;
        //    mY -= Input.GetAxis("Mouse Y") * SpeedY * 0.02F;
        //    mY = ClampAngle(mY, MinLimitY, MaxLimitY);
        //    Quaternion mRotation = Quaternion.Euler(-mY, mX, 0);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, mRotation, Time.deltaTime * Damping);
        //}

    }
    IEnumerator OnMouseDown()
    {
        Camera camera = Camera.main;
        if (camera && !GlobalSys.GetElement(gameObject).isInPosition && Input.GetMouseButton(0))
        {
            Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);//将物体实际坐标转换为屏幕坐标
            Vector3 mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);//获取鼠标屏幕坐标
            Vector3 offset = transform.position - camera.ScreenToWorldPoint(mScreenPosition);//获取移动的向量
            while (Input.GetMouseButton(0))
            {
                mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);//更新鼠标坐标
                transform.position = offset + camera.ScreenToWorldPoint(mScreenPosition);//offset累加
                yield return new WaitForFixedUpdate();
            }
        }
        
    }
    void OnMouseUp()
    {
        GameObject target = GameObject.Find(gameObject.name + "Al");
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) <= GlobalSys.MinFixDistance && !GlobalSys.GetElement(gameObject).isInPosition)
        {
            gameObject.transform.position = target.transform.position;
            Element temp = GlobalSys.GetElement(gameObject);
            temp.isInPosition = true;
            engine.elementsPool.Remove(temp);
            GameGlobal.completed[temp.Category].Add(temp);
            GameObject.Find("ObjectCollection").GetComponent<AudioSource>().Play();
            ParticleSystem p = GameObject.Find("Particle").GetComponent<ParticleSystem>();
            //GameObject.Find("Mesh").GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;
            p.transform.position = transform.position;
            p.Play();
        }
    }
    void OnMouseOver()
    {
        GameObject temp = GameObject.Find(gameObject.name + "Al");
        GameObject.Find("info").transform.Find("Text").GetComponent<Text>().text = GlobalSys.GetElement(gameObject).Name;
        Color tempColor = temp.GetComponent<Renderer>().material.color;
        temp.GetComponent<Renderer>().material.color = new Color(tempColor.r, tempColor.g, tempColor.b, 200f / 255);
        
    }
    void OnMouseExit()
    {
        GameObject temp = GameObject.Find(gameObject.name + "Al");
        GameObject.Find("info").transform.Find("Text").GetComponent<Text>().text = "";
        Color tempColor = temp.GetComponent<Renderer>().material.color;
        temp.GetComponent<Renderer>().material.color = new Color(tempColor.r, tempColor.g, tempColor.b, 50f / 255);
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}
