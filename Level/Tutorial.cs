using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using DG.Tweening;

public class Tutorial : MonoBehaviour {

    LevelEngine engine;
    // Use this for initialization
    void Start () {

        engine = gameObject.GetComponent<LevelEngine>();
        engine.AddCard("Muscular_Transverse_Abdominus", CardColor.Blue);
        engine.AddCard("Muscular_Rectus_Abdominus", CardColor.Brown);
        engine.AddCard("Muscular_Obliquus_Internus", CardColor.Green);
        engine.AddCard("Muscular_Obliquus_Externus", CardColor.Red);
    }

    // Update is called once per frame
    void Update () {

	}
}
