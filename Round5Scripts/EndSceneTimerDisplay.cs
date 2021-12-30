using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneTimerDisplay : MonoBehaviour
{
    Text textUI;
    GameTimer gameTimer;
    // Start is called before the first frame update
    void Start()
    {
        textUI = GetComponent<Text>();
        gameTimer = GameObject.Find("GameTimer").GetComponent<GameTimer>();
        textUI.text = gameTimer.realCostTime.ToString() + "s";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
