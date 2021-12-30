using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControl : MonoBehaviour
{
    public bool triggered = false;
    Color triggeredColor = new Color(48.0f / 255.0f, 246.0f / 255.0f, 80.0f / 255.0f, 0.3f);
    WheelManager wheelManager;
    public Transform respawnPoint;
    public AudioClip checkPointSFX;
    public GameObject audioListener;

    public bool isStartPoint = false;
    // Start is called before the first frame update
    void Start()
    {
        wheelManager = GameObject.Find("WheelManager").GetComponent<WheelManager>();
        audioListener = GameObject.Find("AudioListener");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wheel") && !triggered)
        {
            if (!other.transform.parent.GetComponent<WheelChairController>().dontCheckWin)
            {
                if (other.transform.parent.GetComponent<WheelChairController>().isP1)
                {
                    if (this.transform.parent.name == "LeftPlayerCheckPoints")
                    {
                        triggered = true;
                        this.transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", triggeredColor);
                        wheelManager.checkPoint_p1 = this.gameObject.transform;
                        if (!isStartPoint)
                            AudioSource.PlayClipAtPoint(checkPointSFX, audioListener.transform.position);
                    }
                }
                else
                {
                    if (this.transform.parent.name == "RightPlayerCheckPoints")
                    {
                        triggered = true;
                        this.transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", triggeredColor);
                        wheelManager.checkPoint_p2 = this.gameObject.transform;
                        if (!isStartPoint)
                            AudioSource.PlayClipAtPoint(checkPointSFX, audioListener.transform.position);
                    }
                }
            }
        }


    }
}
