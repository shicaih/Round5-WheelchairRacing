using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    float beginTime;
    public float realCostTime;
    public bool inWin = false;
    // Start is called before the first frame update
    void Start()
    {
        beginTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inWin)
        {
            realCostTime = Time.time - beginTime;
        }
    }
}
