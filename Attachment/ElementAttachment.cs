using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ElementAttachment : MonoBehaviour {

    public event MouseEventHandler SplitParents;
    IEnumerator OnMouseDown()
    {
        Camera camera = Camera.main;
        if (camera
            && GlobalSys.ElementIndex[gameObject.name].isChosen
            && !(GlobalSys.ElementIndex[gameObject.name].isLinkedToCore))
        {
            Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);//将物体实际坐标转换为屏幕坐标
            Vector3 mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);//获取鼠标屏幕坐标
            Vector3 offset = transform.position - camera.ScreenToWorldPoint(mScreenPosition);//获取移动的向量
            while (Input.GetMouseButton(0))
            {
                mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);//更新鼠标坐标
                transform.position = offset + camera.ScreenToWorldPoint(mScreenPosition);//offset累加
                //print("drag");
                yield return new WaitForFixedUpdate();
            }
        }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(1))
        {
            SplitParents(new MouseEventArgs(gameObject));
        }
    }
}
