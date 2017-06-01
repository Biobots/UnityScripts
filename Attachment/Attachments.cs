using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class Attachments : MonoBehaviour {

    void Start ()
    {
        MouseFunc.Awake();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseOver()
    {
        Listener.instance
            .From(gameObject)
            .Let(GameObject.Find("Text").gameObject)
            .Activate(MouseFunc.MouseOverObject);
    }
    void OnMouseExit()
    {
        Listener.instance
            .From(gameObject)
            .Let(GameObject.Find("Text").gameObject)
            .Activate(MouseFunc.MouseExitObject);
    }
}
