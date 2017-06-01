using UnityEngine;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

public class ConnectScene : MonoBehaviour {

    public List<Element> CurrentElements;
    
    // Use this for initialization
    void Start ()
    {
        if (!GameGlobal.Initialized)
        { GameGlobal.Initialize(); }
        CurrentElements = new List<Element>();
        List<Element> allElements = new List<Element>();
        allElements.AddRange(GlobalSys.ElementIndex.Values);
        GlobalSys.Results.Add("AllButton", allElements);
        //foreach (var f in GlobalSys.ElementIndex)
        //{
        //    LoadElement(f.Value, f.Value.Position);
        //    CurrentElements.Add(f.Value);
        //    List<Element> temp = new List<Element>();
        //    temp.Add(f.Value);
        //    GlobalSys.ConnectedList.Add(temp);
        //}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void LoadElement(Element target, Vector3 position)
    {
        target.isChosen = false;
        target.isShown = true;

        target.Value = Instantiate(Resources.Load("Prefabs/" + target.Key), position, new Quaternion(0, 0, 0, 0)) as GameObject;
        target.Value.name = target.Key;

        target.Value.AddComponent<MeshCollider>();
        target.Value.AddComponent<ConnectAttachment>();
        target.Value.AddComponent<DelegateAttachment>();
        target.Value.GetComponent<DelegateAttachment>().MouseUp += ConnectProcess;

    }
    public void ConnectProcess(MouseEventArgs e)
    {
        Element pointer = GlobalSys.GetElement(e.Pointer);
        List<Element> pointerList = GlobalSys.GetList(pointer);
        foreach (Element currentElement in CurrentElements)
        {
            if (pointer != currentElement && !(pointerList.Contains(currentElement)))
            {
                for (int i = 0; i < pointerList.Count; i++)
                {
                    if (TryConnect(pointerList[i], currentElement) && !(pointerList.Contains(currentElement)))
                    {
                        List<Element> temp = GlobalSys.GetList(currentElement);
                        pointerList.AddRange(temp);
                        GlobalSys.ConnectedList.Remove(temp);
                    }
                }
                //foreach (Element partner in pointerList)
                //{
                //    //if (partner.Members.Contains(currentElement))
                //    {
                //        if (TryConnect(partner, currentElement))
                //        {
                //            List<Element> temp = GlobalSys.GetList(currentElement);
                //            pointerList.AddRange(temp);
                //            GlobalSys.ConnectedList.Remove(temp);
                //        }
                //    }
                //}
            }
        }
    }
    bool TryConnect(Element pointer, Element target)
    {
        float dis = Vector3.Distance(pointer.Value.transform.position, target.Value.transform.position);
        float standardDis = Vector3.Distance(pointer.Position, target.Position);
        if (Math.Abs(dis - standardDis) < GlobalSys.MinFixDistance)
        {
            return true;
        }
        else { return false; }
    }
}
