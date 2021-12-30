using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChairController : MonoBehaviour
{
    // add by caicai start
    private float forwardHold = 10.0f, backwardHold = -10.0f;
    public ArduinoInputReader leftVeloReader, rightVeloReader;
    // add by caicai end

    [HideInInspector]
    public WheelManager wheelManager;
    [HideInInspector]
    public int brokenWheelNum = 0;
    [HideInInspector]
    public bool needToRepair = false;
    //public Vector3 diePosition;

    public GameObject middle;
    public GameObject leftWheel;
    public GameObject rightWheel;
    public GameObject tinyLeftWheel;
    public GameObject tinyRightWheel;

    public bool isP1 = true;
    KeyCode leftInput = KeyCode.Q;
    KeyCode leftBackInput = KeyCode.A;
    KeyCode rightInput = KeyCode.T;
    KeyCode rightBackInput = KeyCode.G;
    KeyCode respawnInput = KeyCode.Alpha1;

    public bool useChangingTransform = false;
    public float speed = 2.0f;
    public float rotateSpeed = 20.0f;

    public bool useJointMotor = false;
    public float leftMaxMotorSpeed = 400.0f;
    public float rightMaxMotorSpeed = 400.0f;
    public float leftMotorSpeedMulti = 1.0f;
    public float rightMotorSpeedMulti = 1.0f;
    HingeJoint leftHinge;
    HingeJoint tinyLeftHinge;
    HingeJoint rightHinge;
    HingeJoint tinyRightHinge;
    public bool dontCheckWin = false;

    public bool enableArdino = true;

    [HideInInspector]
    public float leftVel;
    [HideInInspector]
    public float rightVel;

    void Start()
    {
        wheelManager = GameObject.Find("WheelManager").GetComponent<WheelManager>();
        SetPlayerInputByP1P2();
        //SetMyoBand();
        leftMaxMotorSpeed = wheelManager.motorSpeed;
        rightMaxMotorSpeed = wheelManager.motorSpeed;
        enableArdino = wheelManager.enableRealWheel;
    }

    void SetPlayerInputByP1P2()
    {
        if (!isP1)
        {
            leftInput = KeyCode.UpArrow;
            leftBackInput = KeyCode.DownArrow;

            rightInput = KeyCode.Keypad3;
            rightBackInput = KeyCode.KeypadPeriod;

            respawnInput = KeyCode.Alpha2;
        }
    }

    //void SetMyoBand()
    //{
    //    if (isP1)
    //    {
    //        wheelManager.SetP1Myo(this);
    //    }
    //    else
    //    {
    //        wheelManager.SetP2Myo(this);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        enableArdino = wheelManager.enableRealWheel;
        leftMaxMotorSpeed = wheelManager.motorSpeed;
        rightMaxMotorSpeed = wheelManager.motorSpeed;
        ResetBrokenWheelNum();
        CheckWheel();
        UpdateInput();
        CheckRepair();

    }

    void CheckRepair()
    {
        if (brokenWheelNum >= 2 && !needToRepair)
        {
            Respawn(1.0f);
        }
        else if ((middle.transform.position - leftWheel.transform.position).magnitude > 2.0f)
        {
            Respawn(1.0f);
        }
        else if ((middle.transform.position - rightWheel.transform.position).magnitude > 2.0f)
        {
            Respawn(1.0f);
        }
    }

    void Respawn(float timer)
    {
        wheelManager.ReSpawnPlayer(this.isP1, timer);
        needToRepair = true;
    }

    void CheckWheel()
    {
        if (useJointMotor)
        {
            if (!leftWheel.TryGetComponent<HingeJoint>(out leftHinge))
            {
                brokenWheelNum++;
            }
            if (!tinyLeftWheel.TryGetComponent<HingeJoint>(out tinyLeftHinge))
            {
                brokenWheelNum++;
            }
            if (!rightWheel.TryGetComponent<HingeJoint>(out rightHinge))
            {
                brokenWheelNum++;
            }
            if (!tinyRightWheel.TryGetComponent<HingeJoint>(out tinyRightHinge))
            {
                brokenWheelNum++;
            }

        }
    }
    void ResetBrokenWheelNum()
    {
        brokenWheelNum = 0;
    }

    void UpdateInput()
    {
        if (Input.GetKeyDown(respawnInput) && !needToRepair)
        {
            Respawn(0.0f);
        }

        if (useChangingTransform)
        {
            if (Input.GetKey(leftInput))
            {
                this.transform.position += middle.transform.forward * speed * Time.deltaTime;
                this.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
            }
            if (Input.GetKey(rightInput))
            {
                this.transform.position += middle.transform.forward * speed * Time.deltaTime;
                this.transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
            }
        }
        if (useJointMotor)
        {
            if (Input.GetKeyDown(leftInput))
            {
                if (leftHinge != null)
                {
                    var leftMotor = leftHinge.motor;
                    leftMotor.targetVelocity = leftMaxMotorSpeed * leftMotorSpeedMulti;
                    leftHinge.motor = leftMotor;
                }

                if (tinyLeftHinge != null)
                {
                    var tinyLeftMotor = tinyLeftHinge.motor;
                    tinyLeftMotor.targetVelocity = leftMaxMotorSpeed * 0.2f * leftMotorSpeedMulti;
                    tinyLeftHinge.motor = tinyLeftMotor;
                }
            }
            if (Input.GetKeyUp(leftInput))
            {
                if (leftHinge != null)
                {
                    var leftMotor = leftHinge.motor;
                    leftMotor.targetVelocity = 0;
                    leftHinge.motor = leftMotor;
                }
                if (tinyLeftHinge != null)
                {
                    var tinyLeftMotor = tinyLeftHinge.motor;
                    tinyLeftMotor.targetVelocity = 0;
                    tinyLeftHinge.motor = tinyLeftMotor;
                }
            }

            if (Input.GetKeyDown(leftBackInput))
            {
                if (leftHinge != null)
                {
                    var leftMotor = leftHinge.motor;
                    leftMotor.targetVelocity = -leftMaxMotorSpeed * leftMotorSpeedMulti;
                    leftHinge.motor = leftMotor;
                }

                if (tinyLeftHinge != null)
                {
                    var tinyLeftMotor = tinyLeftHinge.motor;
                    tinyLeftMotor.targetVelocity = -leftMaxMotorSpeed * 0.2f * leftMotorSpeedMulti;
                    tinyLeftHinge.motor = tinyLeftMotor;
                }
            }
            if (Input.GetKeyUp(leftBackInput))
            {
                if (leftHinge != null)
                {
                    var leftMotor = leftHinge.motor;
                    leftMotor.targetVelocity = 0;
                    leftHinge.motor = leftMotor;
                }
                if (tinyLeftHinge != null)
                {
                    var tinyLeftMotor = tinyLeftHinge.motor;
                    tinyLeftMotor.targetVelocity = 0;
                    tinyLeftHinge.motor = tinyLeftMotor;
                }
            }

            if (Input.GetKeyDown(rightInput))
            {

                if (rightHinge != null)
                {
                    var rightMotor = rightHinge.motor;
                    rightMotor.targetVelocity = rightMaxMotorSpeed * rightMotorSpeedMulti;
                    rightHinge.motor = rightMotor;
                }

                if (tinyRightHinge != null)
                {
                    var tinyRightMotor = tinyRightHinge.motor;
                    tinyRightMotor.targetVelocity = rightMaxMotorSpeed * 0.2f * rightMotorSpeedMulti;
                    tinyRightHinge.motor = tinyRightMotor;
                }
            }
            if (Input.GetKeyUp(rightInput))
            {
                if (rightHinge != null)
                {
                    var rightMotor = rightHinge.motor;
                    rightMotor.targetVelocity = 0;
                    rightHinge.motor = rightMotor;
                }

                if (tinyRightHinge != null)
                {
                    var tinyRightMotor = tinyRightHinge.motor;
                    tinyRightMotor.targetVelocity = 0;
                    tinyRightHinge.motor = tinyRightMotor;
                }
            }


            if (Input.GetKeyDown(rightBackInput))
            {

                if (rightHinge != null)
                {
                    var rightMotor = rightHinge.motor;
                    rightMotor.targetVelocity = -rightMaxMotorSpeed * rightMotorSpeedMulti;
                    rightHinge.motor = rightMotor;
                }

                if (tinyRightHinge != null)
                {
                    var tinyRightMotor = tinyRightHinge.motor;
                    tinyRightMotor.targetVelocity = -rightMaxMotorSpeed * 0.2f * rightMotorSpeedMulti;
                    tinyRightHinge.motor = tinyRightMotor;
                }
            }
            if (Input.GetKeyUp(rightBackInput))
            {
                if (rightHinge != null)
                {
                    var rightMotor = rightHinge.motor;
                    rightMotor.targetVelocity = 0;
                    rightHinge.motor = rightMotor;
                }

                if (tinyRightHinge != null)
                {
                    var tinyRightMotor = tinyRightHinge.motor;
                    tinyRightMotor.targetVelocity = 0;
                    tinyRightHinge.motor = tinyRightMotor;
                }
            }


            //if (Input.GetKey(backInput))
            //{
            //    this.transform.position -= 0.3f * Time.deltaTime * middle.transform.forward;
            //}
            // add by caicai start
            // 
            if (enableArdino)
            {
                if (IsLeftMovingForward())
                {
                    if (leftHinge != null)
                    {
                        var leftMotor = leftHinge.motor;
                        leftMotor.targetVelocity = leftMaxMotorSpeed * leftMotorSpeedMulti;
                        leftHinge.motor = leftMotor;
                    }

                    if (tinyLeftHinge != null)
                    {
                        var tinyLeftMotor = tinyLeftHinge.motor;
                        tinyLeftMotor.targetVelocity = leftMaxMotorSpeed * 0.2f * leftMotorSpeedMulti;
                        tinyLeftHinge.motor = tinyLeftMotor;
                    }
                }

                if (IsRightMovingForward())
                {
                    if (rightHinge != null)
                    {
                        var rightMotor = rightHinge.motor;
                        rightMotor.targetVelocity = rightMaxMotorSpeed * rightMotorSpeedMulti;
                        rightHinge.motor = rightMotor;
                    }

                    if (tinyRightHinge != null)
                    {
                        var tinyRightMotor = tinyRightHinge.motor;
                        tinyRightMotor.targetVelocity = rightMaxMotorSpeed * 0.2f * rightMotorSpeedMulti;
                        tinyRightHinge.motor = tinyRightMotor;
                    }
                }

                if (IsLeftMovingBackward())
                {
                    if (leftHinge != null)
                    {
                        var leftMotor = leftHinge.motor;
                        leftMotor.targetVelocity = -leftMaxMotorSpeed * leftMotorSpeedMulti;
                        leftHinge.motor = leftMotor;
                    }

                    if (tinyLeftHinge != null)
                    {
                        var tinyLeftMotor = tinyLeftHinge.motor;
                        tinyLeftMotor.targetVelocity = -leftMaxMotorSpeed * 0.2f * leftMotorSpeedMulti;
                        tinyLeftHinge.motor = tinyLeftMotor;
                    }
                }

                if (IsRightMovingBackward())
                {
                    if (rightHinge != null)
                    {
                        var rightMotor = rightHinge.motor;
                        rightMotor.targetVelocity = -rightMaxMotorSpeed * rightMotorSpeedMulti;
                        rightHinge.motor = rightMotor;
                    }

                    if (tinyRightHinge != null)
                    {
                        var tinyRightMotor = tinyRightHinge.motor;
                        tinyRightMotor.targetVelocity = -rightMaxMotorSpeed * 0.2f * rightMotorSpeedMulti;
                        tinyRightHinge.motor = tinyRightMotor;
                    }
                }

                if (IsLeftStop())
                {
                    if (leftHinge != null)
                    {
                        var leftMotor = leftHinge.motor;
                        leftMotor.targetVelocity = 0;
                        leftHinge.motor = leftMotor;
                    }
                    if (tinyLeftHinge != null)
                    {
                        var tinyLeftMotor = tinyLeftHinge.motor;
                        tinyLeftMotor.targetVelocity = 0;
                        tinyLeftHinge.motor = tinyLeftMotor;
                    }
                }

                if (IsRightStop())
                {
                    if (rightHinge != null)
                    {
                        var rightMotor = rightHinge.motor;
                        rightMotor.targetVelocity = 0;
                        rightHinge.motor = rightMotor;
                    }

                    if (tinyRightHinge != null)
                    {
                        var tinyRightMotor = tinyRightHinge.motor;
                        tinyRightMotor.targetVelocity = 0;
                        tinyRightHinge.motor = tinyRightMotor;
                    }
                }
            }
            // add by caicai end
            leftVel = 0;
            rightVel = 0;
            if (leftHinge != null)
            {
                leftVel = leftHinge.motor.targetVelocity;
            }
            if (rightHinge != null)
            {
                rightVel = rightHinge.motor.targetVelocity;
            }
        }

    }

    // add by caicai start
    // determine if should move forward

    private bool IsLeftMovingForward()
    {
        if (leftVeloReader.velocity > forwardHold) return true;
        return false;
    }

    private bool IsRightMovingForward()
    {
        if (rightVeloReader.velocity > forwardHold) return true;
        return false;
    }



    private bool IsLeftMovingBackward()
    {
        if (leftVeloReader.velocity < backwardHold) return true;
        return false;
    }

    private bool IsRightMovingBackward()
    {
        if (rightVeloReader.velocity < backwardHold) return true;
        return false;
    }

    private bool IsLeftStop()
    {
        if (leftVeloReader.velocity <= forwardHold && leftVeloReader.velocity >= backwardHold) return true;
        return false;
    }
    private bool IsRightStop()
    {
        if (rightVeloReader.velocity <= forwardHold && rightVeloReader.velocity >= backwardHold) return true;
        return false;
    }


    // add by caicai end
}
