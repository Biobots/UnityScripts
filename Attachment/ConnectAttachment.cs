using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;

public class ConnectAttachment : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetMouseButtonUp(1))
        {
            GlobalSys.currentChosen.Value.GetComponent<MeshRenderer>().material.color = Color.white;
            GameObject.Find("InfoText").GetComponent<Text>().text = "";
            GameObject.Find("NameText").GetComponent<Text>().text = "Nothing Selected";
            GlobalSys.currentChosen.isChosen = false;
            GlobalSys.currentChosen = null;
        }
	}
    IEnumerator OnMouseDown()
    {
        Camera camera = Camera.main;
        if (camera)
        {
            Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);//将物体实际坐标转换为屏幕坐标
            Vector3 mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);//获取鼠标屏幕坐标
            Vector3 offset = transform.position - camera.ScreenToWorldPoint(mScreenPosition);//获取移动的向量
            List<Element> temp = GlobalSys.GetList(GlobalSys.GetElement(gameObject));
            foreach (Element e in temp)
            {
                if (e.Value != gameObject)
                {
                    e.Value.transform.parent = gameObject.transform;
                    e.Value.transform.localPosition = GlobalSys.SearchVector(GlobalSys.GetElement(gameObject), e);
                }
            }
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
        transform.DetachChildren();
    }
}
