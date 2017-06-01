using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class LevelOne : MonoBehaviour {

    LevelEngine engine;
    // Use this for initialization
	void Start () {
        engine = Camera.main.GetComponent<LevelEngine>();
        engine.AddCard("Muscular_Plantaris", CardColor.Blue, 0);
        engine.AddCard("Muscular_Soleus", CardColor.Blue, 0);
        engine.AddCard("Muscular_Tibialis_Anterior", CardColor.Blue, 0);
        engine.AddCard("Muscular_Semitendinosus", CardColor.Blue, 0);
        engine.AddCard("Muscular_Biceps_Femoris", CardColor.Blue, 0);
        engine.AddCard("Muscular_Gracilis", CardColor.Blue, 0);
        engine.AddCard("Muscular_Adductor_Longus", CardColor.Blue, 0);
        engine.AddCard("Muscular_Vastus_Lateralis", CardColor.Blue, 0);
        engine.AddCard("Muscular_Vastus_Medialis", CardColor.Blue, 0);
        engine.AddCard("Muscular_Sartorius", CardColor.Blue, 0);
        engine.AddCard("Muscular_Plantar_Interossei", CardColor.Blue, 0);
        engine.AddCard("Muscular_Peroneus_Tertius", CardColor.Blue, 0);
        engine.AddCard("Muscular_Peroneus_Brevis", CardColor.Blue, 0);
        engine.AddCard("Muscular_Peroneus_Longus", CardColor.Blue, 0);
        engine.AddCard("Muscular_Popliteus", CardColor.Blue, 0);
        //engine.AddCard("Muscular_Peroneus_Brevis", CardColor.Blue, 0);
        //engine.AddCard("Muscular_Peroneus_Brevis", CardColor.Blue, 0);
        //engine.AddCard("Muscular_Peroneus_Brevis", CardColor.Blue, 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
