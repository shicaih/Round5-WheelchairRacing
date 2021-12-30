using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class GoToLevel : MonoBehaviour
{
    // Start is called before the first frame update
    VideoPlayer vp;
    public GameObject waitPic;
    bool inSkip = false;
    void Start()
    {
        vp = this.GetComponent<VideoPlayer>();
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "intro.mp4");
        StartCoroutine(StartLevelScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (vp.isPrepared)
        {
            waitPic.SetActive(false);
        }
    }
    IEnumerator StartLevelScene()
    {
        //yield return null;
        //waitPic.SetActive(false);
        while (true && !inSkip)
        {
            yield return null;
            if (!vp.isPlaying && !inSkip)
            {
                SceneManager.LoadScene("alley_scene");
            }
        }

    }

    public void SkipIntroVideo()
    {
        waitPic.SetActive(true);
        vp.Stop();
        inSkip = true;
        StartCoroutine(LoadSceneAfterAWhile());
    }

    IEnumerator LoadSceneAfterAWhile()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("alley_scene");
    }
}
