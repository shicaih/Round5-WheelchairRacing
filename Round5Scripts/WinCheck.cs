using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinCheck : MonoBehaviour
{
    public GameObject gameTimer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wheel"))
        {
            if (!other.transform.parent.GetComponent<WheelChairController>().dontCheckWin)
            {
                if (other.transform.parent.GetComponent<WheelChairController>().isP1)
                {
                    StartCoroutine(GOTOLWS());
                }
                else
                {
                    StartCoroutine(GOTORWS());
                }
            }
        }


    }

    IEnumerator GOTOLWS()
    {
        yield return new WaitForSeconds(0.5f);
        DontDestroyOnLoad(gameTimer);
        gameTimer.GetComponent<GameTimer>().inWin = true;
        SceneManager.LoadScene("EndLeftWin");
    }

    IEnumerator GOTORWS()
    {
        yield return new WaitForSeconds(0.5f);
        DontDestroyOnLoad(gameTimer);
        gameTimer.GetComponent<GameTimer>().inWin = true;
        SceneManager.LoadScene("EndRightWin");
    }
}
