using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelEngine : MonoBehaviour {

    public List<Element> CurrentElements;
    public List<GameObject> Cards;
    bool moveOut;
    // Use this for initialization
    void Awake()
    {
        if (!GameGlobal.Initialized)
        { GameGlobal.Initialize(); }
        CurrentElements = new List<Element>();
        Cards = new List<GameObject>();
        moveOut = false;
    }
    void Start () {
        GameObject ctgryCollection = GameObject.Find("Collection");
        //foreach (string ctgry in GameGlobal.areas.Keys)
        //{
        //    LoadCtgry(ctgry);
        //}
        //ctgryCollection.transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update () {
	
	}

    void LoadCtgry(string name)
    {
        GameObject button = Instantiate(Resources.Load("Prefabs/Components/CategoryButton")) as GameObject;
        button.name = name;
        button.transform.Find("Text").GetComponent<Text>().text = name;
        button.transform.SetParent(GameObject.Find("Collection").transform);
    }

    public void LoadElement(Element target, Vector3 position)
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
                        GameObject.Find("Title").GetComponent<AudioSource>().Play();
                    }
                }
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

    public void UpdateCards()
    {
        GameObject grid = GameObject.Find("CardGrid");
        grid.transform.DetachChildren();
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].transform.SetParent(grid.transform);
        }
        //foreach(GameObject g in Cards)
        //{
        //    g.transform.SetParent(grid.transform);
        //}
    }
    public void AddCard(string name, CardColor color)
    {
        GameObject card = Instantiate(Resources.Load("Prefabs/Components/Result")) as GameObject;
        switch (color)
        {
            case CardColor.Blue:
                card.GetComponent<Image>().color = new Color(46f / 255, 68f / 255, 98f / 255);
                card.transform.FindChild("Apply").GetComponent<Image>().color = new Color(141f / 255, 154f / 255, 173f / 255);
                card.transform.FindChild("Info").GetComponent<Image>().color = new Color(13f / 255, 19f / 255, 28f / 255);
                break;
            case CardColor.Red:
                card.GetComponent<Image>().color = new Color(98f / 255, 55f / 255, 46f / 255);
                card.transform.FindChild("Apply").GetComponent<Image>().color = new Color(176f / 255, 142f / 255, 135f / 255);
                card.transform.FindChild("Info").GetComponent<Image>().color = new Color(37f / 255, 21f / 255, 17f / 255);
                break;
            case CardColor.Brown:
                card.GetComponent<Image>().color = new Color(84f / 255, 84f / 255, 41f / 255);
                card.transform.FindChild("Apply").GetComponent<Image>().color = new Color(160f / 255, 160f / 255, 121f / 255);
                card.transform.FindChild("Info").GetComponent<Image>().color = new Color(32f / 255, 33f / 255, 16f / 255);
                break;
            case CardColor.Green:
                card.GetComponent<Image>().color = new Color(46f / 255, 98f / 255, 95f / 255);
                card.transform.FindChild("Apply").GetComponent<Image>().color = new Color(161f / 255, 208f / 255, 206f / 255);
                card.transform.FindChild("Info").GetComponent<Image>().color = new Color(19f / 255, 41f / 255, 40f / 255);
                break;
        }
        card.name = "B" + name;
        card.transform.FindChild("Name").GetComponent<Text>().text = GlobalSys.ElementIndex[name].Name;
        card.transform.SetParent(GameObject.Find("CardGrid").transform);
        card.transform.localScale = card.transform.parent.localScale;
        card.GetComponent<CardButton>().value = GlobalSys.ElementIndex[name];
        Cards.Add(card);
    }
    public void AddCard(string name, CardColor color, float shift)
    {
        GameObject card = Instantiate(Resources.Load("Prefabs/Components/Result")) as GameObject;
        switch (color)
        {
            case CardColor.Blue:
                card.GetComponent<Image>().color = new Color(46f / 255, 68f / 255, 98f / 255);
                card.transform.FindChild("Apply").GetComponent<Image>().color = new Color(141f / 255, 154f / 255, 173f / 255);
                card.transform.FindChild("Info").GetComponent<Image>().color = new Color(13f / 255, 19f / 255, 28f / 255);
                break;
            case CardColor.Red:
                card.GetComponent<Image>().color = new Color(98f / 255, 55f / 255, 46f / 255);
                card.transform.FindChild("Apply").GetComponent<Image>().color = new Color(176f / 255, 142f / 255, 135f / 255);
                card.transform.FindChild("Info").GetComponent<Image>().color = new Color(37f / 255, 21f / 255, 17f / 255);
                break;
            case CardColor.Brown:
                card.GetComponent<Image>().color = new Color(84f / 255, 84f / 255, 41f / 255);
                card.transform.FindChild("Apply").GetComponent<Image>().color = new Color(160f / 255, 160f / 255, 121f / 255);
                card.transform.FindChild("Info").GetComponent<Image>().color = new Color(32f / 255, 33f / 255, 16f / 255);
                break;
            case CardColor.Green:
                card.GetComponent<Image>().color = new Color(46f / 255, 98f / 255, 95f / 255);
                card.transform.FindChild("Apply").GetComponent<Image>().color = new Color(161f / 255, 208f / 255, 206f / 255);
                card.transform.FindChild("Info").GetComponent<Image>().color = new Color(19f / 255, 41f / 255, 40f / 255);
                break;
        }
        card.name = "B" + name;
        card.transform.FindChild("Name").GetComponent<Text>().text = GlobalSys.ElementIndex[name].Name;
        card.transform.SetParent(GameObject.Find("CardGrid").transform);
        card.transform.localScale = card.transform.parent.localScale;
        card.GetComponent<CardButton>().value = GlobalSys.ElementIndex[name];
        Cards.Add(card);

        card.GetComponent<CardButton>().shift = shift;
    }

    public void ToDetail(string name)
    {
        GameObject ctgryCollection = GameObject.Find("Collection");
        GameObject title = GameObject.Find("Title");
        Cards.Clear();
        ctgryCollection.transform.SetAsFirstSibling();
        UpdateGrid(name);
        title.GetComponent<Text>().text = name;
    }
    public void ToIndex()
    {
        GameObject ctgryCollection = GameObject.Find("Collection");
        GameObject title = GameObject.Find("Title");
        GameObject.Find("Cancel").GetComponent<AudioSource>().Play();
        ctgryCollection.transform.SetAsLastSibling();
        GameObject grid = GameObject.Find("CardGrid");
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
        GameObject panel = GameObject.Find("CardPanel");
        for (int i=0;i<panel.transform.childCount;i++)
        {
            if (panel.transform.GetChild(i).name!="CardGrid")
            { Destroy(panel.transform.GetChild(i).gameObject); }
        }
        title.GetComponent<Text>().text = "类别";
    }
    public void UpdateGrid(string name)
    {
        foreach (Element e in GlobalSys.ElementIndex.Values)
        {
            if (e.Category== name)
            { AddCard(e.Key, CardColor.Blue); }
        }
    }
    public void Return()
    {
        SceneManager.LoadScene("Opening");
    }
    public void MoveOut()
    {
        if (!moveOut)
        {
            GameObject panel = GameObject.Find("SidePanel");
            panel.transform.DOLocalMoveX(400, 1);
            moveOut = true;
        }
    }
    public void MoveIn()
    {
        if (moveOut)
        {
            GameObject panel = GameObject.Find("SidePanel");
            panel.transform.DOLocalMoveX(605, 1);
            moveOut = false;
        }
    }
}
