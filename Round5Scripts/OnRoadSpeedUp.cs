using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoadSpeedUp : MonoBehaviour
{
    // Start is called before the first frame update
    float motorMulti;
    WheelManager wheelManager;
    void Start()
    {
        wheelManager = GameObject.Find("WheelManager").GetComponent<WheelManager>();
        motorMulti = wheelManager.roadSpeedUp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wheel"))
        {
            WheelChairController wcc = other.transform.parent.GetComponent<WheelChairController>();
            if (other.gameObject.tag == "Wheel_Left")
            {
                wcc.leftMotorSpeedMulti = motorMulti;
            }
            if (other.gameObject.tag == "Wheel_Right")
            {
                wcc.rightMotorSpeedMulti = motorMulti;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wheel"))
        {
            WheelChairController wcc = other.transform.parent.GetComponent<WheelChairController>();
            if (other.gameObject.tag == "Wheel_Left")
            {
                wcc.leftMotorSpeedMulti = 1;
            }
            if (other.gameObject.tag == "Wheel_Right")
            {
                wcc.rightMotorSpeedMulti = 1;
            }

        }
    }

}
