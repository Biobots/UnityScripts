using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using DG.Tweening;

public class GameEngine : MonoBehaviour {

    public string category;
    public GameObject chosenStruct;
    public GameObject chosenAlpha;
    public GameObject infoPop;
    GameObject info;
    //GameObject Tour;

    delegate IEnumerator PhaseControl();
    //delegate void PhaseDestroyer();

    PhaseControl preStartPhase;
    PhaseControl preStructurePhase;
    PhaseControl preMainPhase;
    PhaseControl preCompletePhase;

    PhaseControl startPhase;
    PhaseControl structurePhase;
    PhaseControl mainPhase;
    PhaseControl completePhase;

    PhaseControl aftStartPhase;
    PhaseControl aftStructurePhase;
    PhaseControl aftMainPhase;
    PhaseControl aftCompletePhase;

    public List<Element> targetElements;
    public List<Element> elementsPool;
    public List<GameObject> structure;
    public GamePhase phase;
    // Use this for initialization
    void InitializeItems()
    {
        targetElements = new List<Element>();
        elementsPool = new List<Element>();
        structure = new List<GameObject>();
        if (!GameGlobal.Initialized)
        { GameGlobal.Initialize(); }
        foreach (Element e in GameGlobal.areas[GameGlobal.CurrentCategory])
        {
            targetElements.Add(e);
            if (!e.isInPosition)
            {
                elementsPool.Add(e);
            }
        }
        //subscirbe

        preStartPhase += PreStart;
        startPhase += StartMain;
        aftStartPhase += AfterStart;

        preStructurePhase += PreStructure;
        structurePhase += StructureMain;
        aftStructurePhase += AfterStructure;

        preMainPhase += PreMain;
        mainPhase += Main;
        aftMainPhase += AfterMain;

        preCompletePhase += PreComplete;
        completePhase += CompleteMain;
        aftCompletePhase += AfterComplete;
    }
    void Awake()
    {
        phase = GamePhase.Load;
    }
    void Start () {
        InitializeItems();
        //Tour = GameObject.Find("TourPanel");
        //PhaseTo(GamePhase.Start);
    }

    // Update is called once per frame
    void Update () {

        switch (phase)
        {
            case GamePhase.Start:
                StartCoroutine(startPhase());
                break;
            case GamePhase.Structure:
                StartCoroutine(structurePhase());
                break;
            case GamePhase.Main:
                StartCoroutine(mainPhase());
                break;
            case GamePhase.Complete:
                StartCoroutine(completePhase());
                break;
        }
    }

    #region PublicFunctions
    public void PhaseTo(GamePhase target)
    {
        switch (phase)
        {
            case GamePhase.Start:
                StartCoroutine(aftStartPhase());
                break;
            case GamePhase.Structure:
                StartCoroutine(aftStructurePhase());
                break;
            case GamePhase.Main:
                StartCoroutine(aftMainPhase());
                break;
            case GamePhase.Complete:
                StartCoroutine(aftCompletePhase());
                break;
        }
        switch (target)
        {
            case GamePhase.Start:
                StartCoroutine(preStartPhase());
                break;
            case GamePhase.Structure:
                StartCoroutine(preStructurePhase());
                break;
            case GamePhase.Main:
                StartCoroutine(preMainPhase());
                break;
            case GamePhase.Complete:
                StartCoroutine(preCompletePhase());
                break;
        }
        //Pre-After-Funcs
        phase = target;
    }
    #region Banner
    public void ShowBanner(Color bgcolor, Color tcolor, string text, float pause)
    {
        GameObject banner = Instantiate(Resources.Load("Prefabs/Components/Banner")) as GameObject;
        banner.transform.Find("Text").gameObject.GetComponent<Text>().text = text;
        banner.transform.Find("Text").gameObject.GetComponent<Text>().color = tcolor;
        banner.transform.localPosition = new Vector3(3 * Screen.width / 2, Screen.height / 2, 0);
        banner.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height / 3);
        banner.transform.SetParent(GameObject.Find("Canvas").transform);
        //banner.transform.position = new Vector3(Screen.width, 0, 0);
        StartCoroutine(ScrollBanner(pause, banner));
    }
    public IEnumerator ScrollBanner(float pause, GameObject banner)
    {
        while (banner.transform.position.x > -1 * Screen.width / 2)
        {
            if (banner.transform.position.x <= 5 + 1 * Screen.width / 2 && banner.transform.position.x >= -5 + 1 * Screen.width / 2)
            {
                yield return new WaitForSeconds(pause);
                banner.transform.position += new Vector3(-10, 0, 0);
            }
            else
            {
                banner.transform.position += new Vector3(-15, 0, 0);
                yield return new WaitForFixedUpdate();
            }
        }
        Destroy(banner);
    }
    #endregion
    #endregion

    #region StartPhase

    IEnumerator PreStart()
    {
        ShowBanner(Color.grey, Color.cyan, GameGlobal.CurrentCategory, 2);
        yield return new WaitForSeconds(4);
        GameObject toStruct = Instantiate(Resources.Load("Prefabs/Components/ToStructureButton")) as GameObject;
        toStruct.transform.SetParent(GameObject.Find("Canvas").transform, false);
        toStruct.transform.position = new Vector3(4 * Screen.width / 5 , Screen.height / 7, 0);
        toStruct.name = "ToStructureButton";
        toStruct.GetComponent<GameButtonAttachment>().phase = "Structure";
    }
    IEnumerator StartMain()
    {
        yield return null;
    }
    IEnumerator AfterStart()
    {
        Destroy(GameObject.Find("ToStructureButton"));
        yield return null;
    }
    #endregion

    #region StructurePhase
    IEnumerator ShowPreStructureBanner()
    {
        ShowBanner(Color.grey, Color.cyan, GameGlobal.CurrentCategory + "结构", 1);
        yield return null;
    }
    IEnumerator PreStructure()
    {
        infoPop = Instantiate(Resources.Load("Prefabs/Components/InfoPop")) as GameObject;
        infoPop.transform.SetParent(GameObject.Find("Canvas").transform, false);
        infoPop.transform.position = new Vector3(2 * Screen.width, 2 * Screen.height, 0);
        infoPop.name = "InfoPop";
        ShowBanner(Color.grey, Color.cyan, GameGlobal.CurrentCategory + "结构", 2);
        yield return new WaitForSeconds(1);
        GameObject collection = GameObject.Find("StructureCollection");
        Vector3 position = new Vector3(0, 0, 0);
        structure = new List<GameObject>();
        foreach (Element e in targetElements)
        {
            GameObject tempStruct;
            tempStruct = Instantiate(Resources.Load("Prefabs/" + e.Key)) as GameObject;
            tempStruct.name = e.Key + "Struct";
            tempStruct.AddComponent<StructureAttachment>();
            tempStruct.AddComponent<MeshCollider>();
            structure.Add(tempStruct);
            if (GameGlobal.CurrentCategory == "内脏器官")
            {
                tempStruct.transform.Translate(0, 136, 0);
            }
            position += tempStruct.transform.position;
        }
        collection.transform.position = position / targetElements.Count;
        foreach (GameObject g in structure)
        {
            g.transform.SetParent(collection.transform);
        }
        collection.transform.position = Camera.main.GetComponent<GetScreenPosition>().GetCenter(50);
        GameObject toMain = Instantiate(Resources.Load("Prefabs/Components/ToStructureButton")) as GameObject;
        toMain.transform.SetParent(GameObject.Find("Canvas").transform, false);
        toMain.transform.position = new Vector3(4 * Screen.width / 5, Screen.height / 7, 0);
        toMain.name = "ToMainButton";
        toMain.transform.Find("Text").GetComponent<Text>().text = "开始！";
        toMain.GetComponent<GameButtonAttachment>().phase = "Main";
        //GameObject toStart = Instantiate(Resources.Load("Prefabs/Components/ToStructureButton")) as GameObject;
        //toStart.transform.SetParent(GameObject.Find("Canvas").transform, false);
        //toStart.transform.position = new Vector3(1 * Screen.width / 5, Screen.height / 7, 0);
        //toStart.name = "ToStartButton";
        //toStart.transform.Find("Text").GetComponent<Text>().text = "查看简介";
        //toStart.GetComponent<GameButtonAttachment>().phase = "Start";
    }
    IEnumerator StructureMain()
    {
        Vector3 mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        infoPop.transform.position = mScreenPosition;
        if (chosenStruct == null)
        {
            infoPop.transform.Find("NameText").GetComponent<Text>().text = "未选择";
            infoPop.transform.Find("InfoText").GetComponent<Text>().text = "";
            infoPop.SetActive(false);
        }
        else
        {
            Element chosen = GlobalSys.ElementIndex[chosenStruct.name.Substring(0, chosenStruct.name.Length - 6)];
            infoPop.transform.Find("NameText").GetComponent<Text>().text = chosen.Name;
            infoPop.transform.Find("InfoText").GetComponent<Text>().text = chosen.Info;
            infoPop.SetActive(true);
        }
        yield return null;
    }
    IEnumerator AfterStructure()
    {
        GameObject collection = GameObject.Find("StructureCollection");
        for (int i = 0; i < collection.transform.childCount; i++)
        {
            Destroy(collection.transform.GetChild(i).gameObject);
        }
        Destroy(GameObject.Find("ToMainButton"));
        Destroy(GameObject.Find("ToStartButton"));
        Destroy(infoPop);
        yield return null;
    }
    #endregion

    #region MainPhase
    #region Pre
    IEnumerator PreMain()
    {
        GameObject collection = GameObject.Find("StructureCollection");
        Vector3 position = new Vector3(0, 0, 0);
        info = Instantiate(Resources.Load("Prefabs/Components/InfoCard")) as GameObject;
        info.transform.SetParent(GameObject.Find("Canvas").transform, false);
        info.name = "info";
        foreach (Element e in targetElements)
        {
            GameObject tempTarget;
            tempTarget = Instantiate(Resources.Load("Prefabs/" + e.Key)) as GameObject;
            tempTarget.name = e.Key + "Al";
            tempTarget.AddComponent<ModelAttachment>();
            structure.Add(tempTarget);
            position += tempTarget.transform.position;
            if (!e.isInPosition)
            {
                tempTarget.GetComponent<Renderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
                Color tempColor = tempTarget.GetComponent<Renderer>().material.color;
                tempTarget.GetComponent<Renderer>().material.color = new Color(tempColor.r, tempColor.g, tempColor.b, 50f / 255);
            }
        }
        collection.transform.position = position / targetElements.Count;
        foreach (GameObject g in structure)
        {
            g.transform.SetParent(collection.transform);
        }
        collection.transform.position = Camera.main.GetComponent<GetScreenPosition>().GetCenter(50);
        collection.transform.position.Set(collection.transform.position.x, 0, collection.transform.position.z);
        Camera.main.gameObject.AddComponent<RoundMovement>();

        GameObject panel = Instantiate(Resources.Load("Prefabs/Components/GetPanel")) as GameObject;
        panel.transform.SetParent(GameObject.Find("Canvas").transform, false);
        panel.name = "GetPanel";
        yield return null;
    }
    #endregion
    #region Main
    bool isEnd = false;
    IEnumerator Main()
    {
        info.transform.Find("Number").GetComponent<Text>().text = (GameGlobal.areas[GameGlobal.CurrentCategory].Count - GameGlobal.completed[GameGlobal.CurrentCategory].Count).ToString();
        info.transform.Find("Percent").Find("Image").GetComponent<Image>().fillAmount = CalculatePercentage();
        info.transform.Find("Percent").Find("Text").GetComponent<Text>().text = 100 * CalculatePercentage() + "%";
        if (GameGlobal.areas[GameGlobal.CurrentCategory].Count == GameGlobal.completed[GameGlobal.CurrentCategory].Count && !isEnd)
        {
            GameObject toStruct = Instantiate(Resources.Load("Prefabs/Components/Button")) as GameObject;
            toStruct.transform.SetParent(GameObject.Find("Canvas").transform, false);
            //toStruct.transform.position = new Vector3(4 * Screen.width / 5, Screen.height / 7, 0);
            toStruct.name = "ToCompleteButton";
            toStruct.GetComponent<GameButtonAttachment>().phase = "Complete";
            isEnd = true;
        }
        yield return null;
    }
    #endregion
    #region After
    IEnumerator AfterMain()
    {
        GameObject objCollection = GameObject.Find("ObjectCollection");
        GameObject strCollection = GameObject.Find("StructureCollection");
        Destroy(Camera.main.gameObject.GetComponent<RoundMovement>());
        for (int i = 0; i < objCollection.transform.childCount; i++)
        {
            Destroy(objCollection.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < strCollection.transform.childCount; i++)
        {
            Destroy(strCollection.transform.GetChild(i).gameObject);
        }
        Destroy(GameObject.Find("GetPanel"));
        Destroy(GameObject.Find("info"));
        yield return null;
    }
    #endregion
    #endregion

    #region CompletePhase
    #region Pre
    IEnumerator PreComplete()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameSelect");
        yield return null;
    }
    #endregion
    #region Main
    IEnumerator CompleteMain()
    {
        yield return null;
    }
    #endregion
    #region After
    IEnumerator AfterComplete()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameSelect");
        yield return null;
    }
    #endregion
    #endregion



    #region ButtonTrigger

    #endregion
    IEnumerator DoIfTrue(Action func, Func<bool> condition)
    {
        yield return new WaitUntil(condition);
        func();
    }

    public void CreateNew(string key)
    {
        Element target = GlobalSys.ElementIndex[key];
        GameObject model;
        model = Instantiate(Resources.Load("Prefabs/" + target.Key), Camera.main.GetComponent<GetScreenPosition>().GetCenter(40), new Quaternion()) as GameObject;
        model.name = target.Key;
        target.Value = model;
        model.AddComponent<AlphaAttachment>();
        model.AddComponent<MeshCollider>();
    }
    public void CreateNew(Element e)
    {
        Element target = GlobalSys.ElementIndex[e.Key];
        GameObject model;
        model = Instantiate(Resources.Load("Prefabs/" + target.Key), Camera.main.GetComponent<GetScreenPosition>().GetCenter(40), new Quaternion()) as GameObject;
        model.name = target.Key;
        target.Value = model;
        model.AddComponent<AlphaAttachment>();
        model.AddComponent<MeshCollider>();
        //e.isInPosition = false;
        elementsPool.Remove(e);
        model.transform.SetParent(GameObject.Find("ObjectCollection").transform);
    }
    float CalculatePercentage()
    {
        int tempa = 0;
        int tempb = 0;
        tempa = GameGlobal.completed[GameGlobal.CurrentCategory].Count;
        tempb = GameGlobal.areas[GameGlobal.CurrentCategory].Count;
        return (float)Math.Round((float)tempa / tempb, 3);
    }
}
