using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    Vector3 vel = Vector3.zero;

    public bool inFollowing = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (inFollowing)
        {
            Vector3 desiredPos = target.transform.position + offset;
            this.transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref vel, smoothTime);
        }

    }
    public void HardResetCamera()
    {
        Vector3 desiredPos = target.transform.position + offset;
        this.transform.position = desiredPos;
    }
}
