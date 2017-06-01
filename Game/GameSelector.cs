using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameSelector : MonoBehaviour {

    Dictionary<string, List<Element>> areas;
    Dictionary<string, List<Element>> completed;
    public Dictionary<string, List<GameObject>> models;
    public GameObject infoPop;


    // Use this for initialization
    void Start () {
        if (!GameGlobal.Initialized)
        { GameGlobal.Initialize(); }
        //if (!GameGlobal.GameSelectLock)
        //{ Instantiate(Resources.Load("Prefabs/Components/TourPanel")); }
        infoPop = Instantiate(Resources.Load("Prefabs/Components/InfoCard")) as GameObject;
        infoPop = GameObject.Find("InfoCard");
        //infoPop.transform.SetParent(GameObject.Find("Canvas").transform, false);
        //infoPop.transform.position = new Vector3(2 * Screen.width, 2 * Screen.height, 0);
        //infoPop.name = "InfoPop";
        GameObject.Find("vrayYupmid").transform.DetachChildren();
        areas = GameGlobal.areas;
        completed = GameGlobal.completed;
        models = new Dictionary<string, List<GameObject>>();
        foreach (Element e in GlobalSys.ElementIndex.Values)
        {
            //if (e.Category == null)
            //{ break; }
            //else if (areas.ContainsKey(e.Category))
            //{ areas[e.Category].Add(e); }
            //else
            //{
            //    List<Element> temp = new List<Element>();
            //    temp.Add(e);
            //    areas.Add(e.Category, temp);
            //}
            
            //删除器官
            if (e.Category == "内脏器官")
            {
                break;
            }
            //删除器官

            if (!models.ContainsKey(e.Category))
            {
                List<GameObject> tempmodels = new List<GameObject>();
                models.Add(e.Category, tempmodels);
            }
            GameObject model = GameObject.Find(e.Key);
            models[e.Category].Add(model);
            model.AddComponent<MeshCollider>();
            model.AddComponent<SelectAttachment>();
        }
    }

    // Update is called once per frame
    void Update () {
        GameObject.Find("ProcessSlider").GetComponent<Slider>().value = CalculatePercentage();
	}
    public List<Element> GetAreaList(Element input)
    {
        return areas[input.Category];
    }
    float CalculatePercentage()
    {
        int tempa = 0;
        int tempb = 0;
        foreach (List<Element> list in GameGlobal.completed.Values)
        {
            tempa += list.Count;
        }
        foreach (List<Element> list in GameGlobal.areas.Values)
        {
            tempb += list.Count;
        }
        return (float)Math.Round((float)tempa / tempb, 3);
    }
    public void Return()
    {
        SceneManager.LoadScene("Main");
    }
    public void LockSelectTour()
    {
        GameGlobal.GameSelectLock = true;
    }
    public void LoadOrgan()
    {
        GameGlobal.CurrentCategory = "内脏器官";
        SceneManager.LoadSceneAsync("GameScene");
    }
}
