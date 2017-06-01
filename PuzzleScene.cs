using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class PuzzleScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GlobalSys.Initialize();//Debug Mode
        foreach (var f in GlobalSys.ElementIndex)
        {
            if (f.Value.Children.Count == 0) { LoadElement(f.Value, new Vector3(1,1,1)); }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}


    void LoadElement(Element target,Vector3 position)
    {
        target.isChosen = false;
        target.isShown = true;

        target.Value = Instantiate(Resources.Load("Prefabs/" + target.Key), position, new Quaternion(0, 0, 0, 0)) as GameObject;
        target.Value.name = target.Key;

        target.Value.AddComponent<ElementAttachment>();
        target.Value.AddComponent<DelegateAttachment>();
        target.Value.GetComponent<ElementAttachment>().SplitParents += TrySplit;
        target.Value.GetComponent<DelegateAttachment>().MouseOver += UpdateChosen;
        target.Value.GetComponent<DelegateAttachment>().MouseOver += UpdateInfoByMouse;
        target.Value.GetComponent<DelegateAttachment>().MouseExit += LeaveChosen;
        if (target.Parent != null) { target.Value.GetComponent<DelegateAttachment>().MouseUp += TryFixCore; }
        else { target.Value.GetComponent<DelegateAttachment>().MouseUp += TryFixPosition; }
    }
    void DestroyElement(Element target)
    {
        DestroyImmediate(target.Value);

        target.isChosen = false;
        target.isShown = false;
    }


    #region Triggers
    void LeaveChosen(MouseEventArgs e)
    {

    }
    void UpdateInfoByMouse(MouseEventArgs e)
    {

    }


    void UpdateChosen(MouseEventArgs e)
    {
        if (GlobalSys.currentChosen != null) { GlobalSys.currentChosen.isChosen = false; }
        GlobalSys.currentChosen = GlobalSys.GetElement(e.Pointer);
        GlobalSys.currentChosen.isChosen = true;
    }
    void TryFixCore(MouseEventArgs e)
    {
        Element pointer = GlobalSys.GetElement(e.Pointer);
        if (pointer.Parent == null) { return; }
        Element parent = pointer.Parent;
        foreach (Element f in parent.Children)
        {
            if (f.Value == null) { return; }
        }
        if (pointer != parent.CoreChild)
        {
            if (GlobalSys.TryLinkChild(pointer, parent.CoreChild))
            {
                pointer.isLinkedToCore = true;
                foreach (Element f in parent.Children)
                {
                    if (f == parent.CoreChild) { continue; }
                    if (!(f.isLinkedToCore)) { return; }
                }
                ChildrenToParent(parent, parent.CoreChild.Value.transform.position);
            }
        }
    }
    void TryFixPosition(MouseEventArgs e)
    {
        float dis = Vector3.Distance(e.Pointer.transform.position, GlobalSys.GetElement(e.Pointer).Position);
        if (dis < GlobalSys.MinFixDistance)
        { e.Pointer.transform.position = GlobalSys.GetElement(e.Pointer).Position; }
    }

    void TrySplit(MouseEventArgs e)
    {
        ParentToChildren(GlobalSys.GetElement(e.Pointer));
    }
    #endregion
    #region Update
    void ChildrenToParent(Element parent, Vector3 corePosition)//checked
    {
        LoadElement(parent, corePosition);
        DestroyElement(parent.CoreChild);
        foreach (Element child in parent.Children)
        {
            DestroyElement(child);
        }
    }
    void ParentToChildren(Element parent)
    {
        if (parent.Children.Count != 0)
        {
            LoadElement(parent.CoreChild, parent.Value.transform.position);
            foreach (Element child in parent.Children)
            {
                if (!(child.isCoreChild))
                {
                    LoadElement(child, parent.CoreChild.Value.transform.position + GlobalSys.SearchVector(parent.CoreChild, child));
                    child.isLinkedToCore = false;
                }
            }
            DestroyElement(parent);
        }
    }
    #endregion
}
