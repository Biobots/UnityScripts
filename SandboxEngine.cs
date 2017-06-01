using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Leap;

public class SandboxEngine : MonoBehaviour {

    public List<Element> CurrentElements;
    public List<GameObject> Cards;
    public bool InPanel;
    public bool InItemPanel;

    static public float radius = 50;
    static public float height = 50;
    static public float MoveSpeed = 20;
    static public float RotateSpeed = 5;
    // Use this for initialization
    void Awake()
    {
        if (!GameGlobal.Initialized)
        { GameGlobal.Initialize(); }
        CurrentElements = new List<Element>();
        Cards = new List<GameObject>();
    }
    void Start () {
        GameObject ctgryCollection = GameObject.Find("Collection");
        foreach (string ctgry in GameGlobal.areas.Keys)
        {
            LoadCtgry(ctgry);
        }
        InPanel = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Vertical");
        transform.Translate(0, h * Time.deltaTime * MoveSpeed, 0);

        //currentRadius = Vector3.Distance(transform.position, new Vector3(0, transform.position.y, 0));
        float angle = (float)Math.PI * 2 / (RotateSpeed / Time.deltaTime);
        float x = transform.position.x;
        float z = transform.position.z;

        if (Input.GetKey("a"))
        {
            transform.position = new Vector3(CalculateX(z, x, -1 * angle), transform.position.y, CalculateZ(z, x, -1 * angle));
        }
        if (Input.GetKey("d"))
        {
            transform.position = new Vector3(CalculateX(z, x, angle), transform.position.y, CalculateZ(z, x, angle));
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && InPanel == false)
        {
            float s = Input.GetAxis("Mouse ScrollWheel") * 20;
            this.transform.Translate(0, 0, s * Time.deltaTime * MoveSpeed * 5);
        }

        transform.LookAt(new Vector3(0, transform.position.y, 0));
    }
    float CalculateZ(float z, float x, float angle)
    {
        return (float)(z * Math.Cos(angle) + x * Math.Sin(angle));
    }
    float CalculateX(float z, float x, float angle)
    {
        return (float)(x * Math.Cos(angle) - z * Math.Sin(angle));
    }
    public void SetInPanel()
    {
        InPanel = !InPanel;
    }
    
    void LoadCtgry(string name)
    {
        GameObject button = Instantiate(Resources.Load("Prefabs/Components/Btn")) as GameObject;
        button.name = name;
        button.transform.Find("Name").GetComponent<Text>().text = name;
        button.transform.Find("Num").GetComponent<Text>().text = GameGlobal.areas[name].Count.ToString();
        button.transform.SetParent(GameObject.Find("Grid1").transform);
        button.transform.localScale = button.transform.parent.localScale;
    }
    void LoadItem(string name)
    {
        GameObject button;
        if (GlobalSys.ElementIndex[name].Value != null)
        { button = Instantiate(Resources.Load("Prefabs/Components/DeleteBtn")) as GameObject; }
        else
        { button = Instantiate(Resources.Load("Prefabs/Components/ApplyBtn")) as GameObject; }
        button.name = "B" + name;
        button.transform.Find("Name").GetComponent<Text>().text = GlobalSys.ElementIndex[name].Name;
        button.GetComponent<CardButton>().value = GlobalSys.ElementIndex[name];
        button.transform.SetParent(GameObject.Find("Grid2").transform);
        button.transform.localScale = button.transform.parent.localScale;
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
    public void OpenCtgry(string area)
    {
        if (InItemPanel)
        {
            GameObject grid = GameObject.Find("Grid2");
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                Destroy(grid.transform.GetChild(i).gameObject);
            }
        }
        GameObject items = GameObject.Find("ItemPanel");
        items.transform.DOLocalMoveX(170.8f, 1);
        foreach (Element e in GameGlobal.areas[area])
        {
            LoadItem(e.Key);
        }
        InItemPanel = true;
    }
}
