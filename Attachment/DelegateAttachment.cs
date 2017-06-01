using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class DelegateAttachment : MonoBehaviour {

    #region Mouse Events
    public MouseEventHandler MouseOver;
    public MouseEventHandler MouseExit;
    public MouseEventHandler MouseDown;
    public MouseEventHandler MouseUp;
    #endregion
    #region Mouse Trigger
    void OnMouseOver()
    {
        if (MouseOver != null)
        {
            MouseOver(new MouseEventArgs(this.gameObject));
        }
    }
    void OnMouseExit()
    {
        if (MouseExit != null)
        {
            MouseExit(new MouseEventArgs(this.gameObject));
        }
    }
    void OnMouseDown()
    {
        if (MouseDown != null)
        {
            MouseDown(new MouseEventArgs(this.gameObject));
        }
    }
    void OnMouseUp()
    {
        if (MouseUp != null)
        {
            MouseUp(new MouseEventArgs(this.gameObject));
        }
    }

    #endregion


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
