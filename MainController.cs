using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

    bool LeftOut;
    bool InfoOut;
	// Use this for initialization
	void Start () {
        LeftOut = false;
        InfoOut = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LeftFade()
    {
        if (!LeftOut)
        { LeftFadeOut(); }
        else
        { LeftFadeIn(); }
    }
    void LeftFadeOut()
    {
        GameObject.Find("Left").transform.DOLocalMoveX(-312.5f, 1);
        GameObject.Find("Fade").transform.DORotate(new Vector3(0, 0, -180), 1);
        LeftOut = true;
    }
    void LeftFadeIn()
    {
        GameObject.Find("Left").transform.DOLocalMoveX(-493, 1);
        GameObject.Find("Fade").transform.DORotate(new Vector3(0, 0, 0), 1);
        LeftOut = false;
    }
    public void InfoFade()
    {
        if (!InfoOut)
        { InfoFadeOut(); }
        else
        { InfoFadeIn(); }
    }
    void InfoFadeOut()
    {
        GameObject.Find("InfoPanel").transform.DOLocalMoveY(-154, 1);
        InfoOut = true;
    }
    void InfoFadeIn()
    {
        GameObject.Find("InfoPanel").transform.DOLocalMoveY(-280.6f, 1);
        InfoOut = false;
    }
    public void ItemsFadeIn()
    {
        GameObject.Find("ItemPanel").transform.DOLocalMoveX(0, 1);
        //if (GameObject.Find("ItemPanel").transform.DOLocalMoveX(0, 1).IsComplete())
        //{
        //    GameObject grid = GameObject.Find("Grid2");
        //    for (int i = 0; i < grid.transform.childCount; i++)
        //    {
        //        Destroy(grid.transform.GetChild(i).gameObject);
        //    }
        //}
        GameObject grid = GameObject.Find("Grid2");
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
        Camera.main.GetComponent<SandboxEngine>().InItemPanel = false;
    }
    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }
}
