using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeHitAwayByPlayer : MonoBehaviour
{
    public GameObject audioListener;
    public AudioClip hitAwaySFX;
    public float force = 100.0f;
    public bool ifDestroyed = false;
    public float destroyTime = 2.0f;

    public float soundWaitTime = 0.5f;
    bool soundPlayed = false;
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
        if (collision.collider.transform.gameObject.layer == LayerMask.NameToLayer("Wheel"))
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().AddForce(((this.transform.position - collision.transform.position).normalized + 0.1f * Vector3.up) * force, ForceMode.Force);

            if (!soundPlayed)
            {
                StartCoroutine(PlayCollSound());
            }

            if (ifDestroyed)
            {
                Destroy(this.gameObject, destroyTime);
            }
        }

    }

    IEnumerator PlayCollSound()
    {
        AudioSource.PlayClipAtPoint(hitAwaySFX, audioListener.transform.position);
        soundPlayed = true;
        yield return new WaitForSecondsRealtime(soundWaitTime);
        soundPlayed = false;
    }
}
