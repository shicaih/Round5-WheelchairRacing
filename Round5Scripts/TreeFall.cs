using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    GameObject audioListener;
    public Vector3 euAngle = new Vector3(0, 0, -90);
    Quaternion targetQ;
    public AudioClip treeFallSFX;
    // Start is called before the first frame update
    void Start()
    {
        targetQ = Quaternion.Euler(euAngle);
        audioListener = GameObject.Find("AudioListener");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTreeFall()
    {
        StartCoroutine(TreeFalling());
        AudioSource.PlayClipAtPoint(treeFallSFX, audioListener.transform.position);
    }
    IEnumerator TreeFalling()
    {
        while (true)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQ, Time.deltaTime * 2.0f);
            yield return null;
        }
    }
}
