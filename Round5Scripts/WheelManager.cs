using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    public ArduinoInputReader p1_leftVeloReader, p1_rightVeloReader;
    public ArduinoInputReader p2_leftVeloReader, p2_rightVeloReader;

    public GameObject curP1;
    public GameObject curP2;
    public GameObject playerPrefabP1;
    public GameObject playerPrefabP2;
    public Transform endPoint;

    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject nurse;

    public GameObject checkPointsGroupLeft;
    public GameObject checkPointsGroupRight;
    public Transform checkPoint_p1;
    public Transform checkPoint_p2;

    public float gyroMax = 50;
    public float motorSpeed = 400;

    public float roadSpeedUp = 1.5f;

    public bool enableRealWheel = true;

    public float p1_z;
    public float p2_z;

    public float cur_p1_leftVel;
    public float cur_p1_rightVel;
    public float cur_p2_leftVel;
    public float cur_p2_rightVel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        p1_z = curP1.GetComponent<WheelChairController>().middle.transform.position.z;
        p2_z = curP2.GetComponent<WheelChairController>().middle.transform.position.z;
        cur_p1_leftVel = curP1.GetComponent<WheelChairController>().leftVel;
        cur_p1_rightVel = curP1.GetComponent<WheelChairController>().rightVel;
        cur_p2_leftVel = curP2.GetComponent<WheelChairController>().leftVel;
        cur_p2_rightVel = curP2.GetComponent<WheelChairController>().rightVel;
    }

    public void ReSpawnPlayer(bool isP1, float timer)
    {
        StartCoroutine(InstanAfterAWhile(isP1, timer));
    }
    IEnumerator InstanAfterAWhile(bool isP1, float timer)
    {
        if (isP1)
        {
            curP1.GetComponent<WheelChairController>().dontCheckWin = true;
        }
        else
        {
            curP2.GetComponent<WheelChairController>().dontCheckWin = true;
        }
        yield return new WaitForSecondsRealtime(timer / 5.0f);
        if (isP1)
        {
            Camera1.GetComponent<CameraFollow>().inFollowing = false;
        }
        else
        {
            Camera2.GetComponent<CameraFollow>().inFollowing = false;
        }
        yield return new WaitForSecondsRealtime(timer / 5.0f * 4.0f);
        if (isP1)// spawn p1_chair
        {
            // GameObject newPlayer = Instantiate<GameObject>(playerPrefab, curP1.GetComponent<WheelChairController>().diePosition, curP1.transform.rotation);
            GameObject newPlayer = Instantiate<GameObject>(playerPrefabP1, checkPoint_p1.GetComponent<CheckPointControl>().respawnPoint.position, checkPoint_p1.GetComponent<CheckPointControl>().respawnPoint.rotation);
            newPlayer.transform.position = checkPoint_p1.GetComponent<CheckPointControl>().respawnPoint.position;
            newPlayer.transform.rotation = Quaternion.Euler(0, 0, 0);
            newPlayer.GetComponent<WheelChairController>().isP1 = isP1;
            SetP1ArduinoInputReader(newPlayer.GetComponent<WheelChairController>());
            Destroy(curP1);
            //nurse.GetComponent<NurseAI>().SetToAnotherTargetDuringRepair();
            curP1 = newPlayer;
            //curP1.transform.LookAt(endPoint);
            Camera1.GetComponent<CameraFollow>().target = GetBackSeatByChair(curP1);
            Camera1.transform.position = GetBackSeatByChair(curP1).transform.position + Camera1.GetComponent<CameraFollow>().offset;
            Camera1.GetComponent<CameraFollow>().inFollowing = true;
            Camera1.GetComponent<CameraFollow>().HardResetCamera();
            curP1.GetComponent<WheelChairController>().dontCheckWin = false;
            curP1.GetComponent<WheelChairController>().enableArdino = enableRealWheel;
        }
        else// spawn p2_chair
        {
            //GameObject newPlayer = Instantiate<GameObject>(playerPrefab, curP2.GetComponent<WheelChairController>().diePosition, curP1.transform.rotation);
            GameObject newPlayer = Instantiate<GameObject>(playerPrefabP2, checkPoint_p2.GetComponent<CheckPointControl>().respawnPoint.position, checkPoint_p2.GetComponent<CheckPointControl>().respawnPoint.rotation);
            newPlayer.GetComponent<WheelChairController>().isP1 = isP1;
            SetP2ArduinoInputReader(newPlayer.GetComponent<WheelChairController>());
            Destroy(curP2);
            //nurse.GetComponent<NurseAI>().SetToAnotherTargetDuringRepair();
            curP2 = newPlayer;
            //curP2.transform.LookAt(endPoint);
            Camera2.GetComponent<CameraFollow>().target = GetBackSeatByChair(curP2);
            Camera2.transform.position = GetBackSeatByChair(curP2).transform.position + Camera2.GetComponent<CameraFollow>().offset;
            Camera2.GetComponent<CameraFollow>().inFollowing = true;
            Camera2.GetComponent<CameraFollow>().HardResetCamera();
            curP2.GetComponent<WheelChairController>().dontCheckWin = false;
            curP2.GetComponent<WheelChairController>().enableArdino = enableRealWheel;
        }
    }

    public void SetP1ArduinoInputReader(WheelChairController wcc)
    {
        wcc.GetComponent<WheelChairController>().leftVeloReader = p1_leftVeloReader;
        wcc.GetComponent<WheelChairController>().rightVeloReader = p1_rightVeloReader;
        //wcc.gyroMax = gyroMax;
    }

    public void SetP2ArduinoInputReader(WheelChairController wcc)
    {
        wcc.GetComponent<WheelChairController>().leftVeloReader = p2_leftVeloReader;
        wcc.GetComponent<WheelChairController>().rightVeloReader = p2_rightVeloReader;
        //wcc.gyroMax = gyroMax;
    }

    public GameObject GetBackSeatByChair(GameObject chair)
    {
        return chair.GetComponent<WheelChairController>().middle;
    }

}
