using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDoorCrush : MonoBehaviour
{
    GameObject audioListener;
    public AudioClip breakGlassSFX;

    // Start is called before the first frame update
    void Start()
    {
        audioListener = GameObject.Find("AudioListener");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this, 5.0f);
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
        //this.GetComponent<Rigidbody>().isKinematic = false;
        //this.GetComponent<Rigidbody>().AddForce((this.transform.position - collision.transform.position) * 100.0f, ForceMode.Force);
        this.GetComponent<SmallGlassesSplash>().SplashGlasses(collision.transform.position);
        AudioSource.PlayClipAtPoint(breakGlassSFX, audioListener.transform.position);
    }

}
