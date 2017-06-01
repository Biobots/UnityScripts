using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class TabButton : MonoBehaviour {

    GameObject grid;
    // Use this for initialization
    void Start () {
        grid = GameObject.Find("Grid");
    }
	
	// Update is called once per frame
	void Update () {
    }
    void OnMouseUp()
    {

    }
    void Clear()
    {
        //var all = grid.GetComponentsInChildren<GameObject>();
        //foreach (GameObject chid in all)
        //{
        //    if (chid != grid)
        //    { Destroy(chid); }
        //}
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
    }
    void Add(Element e)
    {
        GameObject resultButton = Instantiate(Resources.Load<GameObject>("Prefabs/Components/TestResult"));
        resultButton.transform.SetParent(grid.transform);
        resultButton.name = e.Key;
        resultButton.GetComponentInChildren<Text>().text = e.Key;
        resultButton.GetComponent<ResultButton>().el = e;
    }
    public void Load()
    {
        Clear();
        List<Element> temp = GlobalSys.Results[gameObject.name];
        foreach(Element e in temp)
        {
            Add(e);
        }
    }
}
