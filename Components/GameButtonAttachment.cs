using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameButtonAttachment : MonoBehaviour {

    public bool condition;
    public string phase;
    // Use this for initialization
	void Start () {
        condition = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ChangePhase(string name)
    {
        Camera.main.GetComponent<GameEngine>().PhaseTo((GamePhase)Enum.Parse(typeof(GamePhase), name));
    }
    public void ChangePhase()
    {
        Camera.main.GetComponent<GameEngine>().PhaseTo((GamePhase)Enum.Parse(typeof(GamePhase), phase));
    }
    public void Click()
    {
        condition = true;
    }
    public void GetNew()
    {
        GameEngine engine = Camera.main.GetComponent<GameEngine>();
        UnityEngine.Random rdm = new UnityEngine.Random();
        if (engine.elementsPool.Count != 0)
        {
            int index = UnityEngine.Random.Range(0, engine.elementsPool.Count - 1);
            engine.CreateNew(engine.elementsPool[index]);
        }
    }
    public void ChangePercentage(float value)
    {
        if (value < 0.5)
        { gameObject.GetComponent<Text>().color = Color.white; }
        else
        { gameObject.GetComponent<Text>().color = Color.black; }
        gameObject.GetComponent<Text>().text = 100 * value + "%";
    }

    public void ExitGameMain()
    {
        SceneManager.LoadSceneAsync("GameSelect");
    }
}
