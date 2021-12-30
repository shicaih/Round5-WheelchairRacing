using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameTimerUI : MonoBehaviour
{
    public GameTimer gameTimer;
    Text timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = this.GetComponent<Text>();
        timer.text = gameTimer.realCostTime.ToString() + "s";
    }

    // Update is called once per frame
    void Update()
    {
        int minute = Mathf.FloorToInt(gameTimer.realCostTime / 60.0f);
        int seconds = Mathf.FloorToInt(gameTimer.realCostTime - minute * 60.0f);
        timer.text = minute.ToString() + " min " + seconds.ToString() + " s";
    }
}
