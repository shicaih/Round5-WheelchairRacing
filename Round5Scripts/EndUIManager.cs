using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndUIManager : MonoBehaviour
{
    public GameObject credit;
    public GameObject restart;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitRestart());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator WaitRestart()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        credit.SetActive(true);
        yield return new WaitForSecondsRealtime(2.0f);
        restart.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("alley_scene");

    }
}
