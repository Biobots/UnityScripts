using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectAttachment : MonoBehaviour {

    GameSelector gs;
    Element el;
    bool audiolock;
    // Use this for initialization
    void Start () {
        gs = Camera.main.GetComponent<GameSelector>();
        el = GlobalSys.GetElement(gameObject);
        audiolock = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseOver()
    {
        Element el = GlobalSys.GetElement(gameObject);
        //List<Element> area = gs.GetAreaList(el);
        //Vector3 pos = new Vector3(0, 0, 0);
        foreach (GameObject g in gs.models[el.Category])
        {
            g.GetComponent<MeshRenderer>().material.color = new Color(229f / 255, 101f / 255, 101f / 255);
            foreach (Element e in GameGlobal.completed[el.Category])
            {
                if (e.Key == g.name)
                {
                    g.GetComponent<MeshRenderer>().material.color = new Color(46f / 255, 98f / 255, 59f / 255);
                }
            }
            //pos += Camera.main.WorldToScreenPoint(transform.position);
        }
        //pos = pos / area.Count + new Vector3(Screen.width / 2, 0, 0);
        //Vector3 mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //gs.infoPop.transform.position = mScreenPosition;
        gs.infoPop.transform.Find("Text").GetComponent<Text>().text = el.Category;
        //gs.infoPop.SetActive(true);
        //gs.infoPop.transform.Find("Text").GetComponent<Text>().GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
        //gs.infoPop.GetComponent<RectTransform>().sizeDelta = new Vector2(gs.infoPop.transform.Find("Text").GetComponent<RectTransform>().rect.width, gs.infoPop.transform.Find("Text").GetComponent<RectTransform>().rect.height);
        gs.infoPop.transform.Find("Number").GetComponent<Text>().text = gs.models[el.Category].Count.ToString();
        gs.infoPop.transform.Find("Percent").Find("Image").GetComponent<Image>().fillAmount = CalculatePercentage();
        gs.infoPop.transform.Find("Percent").Find("Text").GetComponent<Text>().text = 100 * CalculatePercentage() + "%";
        if (audiolock)
        {
            GameObject.Find("vrayYupmid").GetComponent<AudioSource>().Play();
            audiolock = false;
        }

    }
    void OnMouseExit()
    {
        Element el = GlobalSys.GetElement(gameObject);
        List<Element> area = gs.GetAreaList(el);
        foreach (GameObject g in gs.models[el.Category])
        {
            g.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        //gs.infoPop.SetActive(false);
        audiolock = true;
    }
    void OnMouseUp()
    {
        GameGlobal.CurrentCategory = GlobalSys.GetElement(gameObject).Category;
        SceneManager.LoadSceneAsync("GameScene");
    }
    float CalculatePercentage()
    {
        int tempa = 0;
        int tempb = 0;
        tempa = GameGlobal.completed[el.Category].Count;
        tempb = GameGlobal.areas[el.Category].Count;
        return (float)tempa / tempb;
    }
}
