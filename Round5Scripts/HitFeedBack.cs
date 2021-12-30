using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFeedBack : MonoBehaviour
{

    //bouncing is not working yet , the collision collider may be the wheel piece, should move the whole wheel chair
    public GameObject audioListener;
    public AudioClip crushSFX;
    public float forceMulti = 10f;

    public enum hitType { crushing, bouncing };
    public hitType curHitType = hitType.crushing;

    public float sendBackTimeCounter = 0.0f;
    public float sendBackTime = 0.3f;
    public float sendBackSpeed = 0.2f;
    private float insideMulti = 1.0f;
    public bool inSendingBack = false;
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
            Vector3 dir = (collision.collider.transform.position - this.transform.position).normalized;
            switch (curHitType)
            {
                case hitType.crushing:
                    collision.rigidbody.AddForce(dir * forceMulti * insideMulti, ForceMode.Impulse);
                    AudioSource.PlayClipAtPoint(crushSFX, audioListener.transform.position);
                    WheelChairController wcc = collision.transform.parent.GetComponent<WheelChairController>();
                    DontCheckWinForAWhile(wcc);
                    break;
                case hitType.bouncing:
                    if (!inSendingBack)
                        StartCoroutine(SendBack(collision, dir));
                    break;
            }
        }


    }
    IEnumerator DontCheckWinForAWhile(WheelChairController wcc)
    {
        wcc.dontCheckWin = true;
        yield return new WaitForSecondsRealtime(0.5f);
        wcc.dontCheckWin = false;
    }
    IEnumerator SendBack(Collision collision, Vector3 dir)
    {
        inSendingBack = true;
        dir.y = 0;
        dir = dir.normalized;
        while (sendBackTimeCounter < sendBackTime)
        {
            sendBackTimeCounter += Time.deltaTime;
            collision.transform.parent.transform.position += dir * sendBackSpeed * Time.deltaTime;
            yield return null;
        }
        sendBackTimeCounter = 0.0f;
        inSendingBack = false;
    }
}
