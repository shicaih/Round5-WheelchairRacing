using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseAI : MonoBehaviour
{
    public GameObject curTarget;

    Transform endPoint;
    public WheelManager wheelManager;
    enum AIState { Idle, Approach, Attack };
    [SerializeField]
    AIState curState = AIState.Approach;

    public float moveSpeed = 0.5f;
    public float turnSpeed = 20f;


    // Start is called before the first frame update
    void Start()
    {
        wheelManager = GameObject.Find("WheelManager").GetComponent<WheelManager>();
        curTarget = wheelManager.curP1.GetComponent<WheelChairController>().middle;
        StartCoroutine(SetTarget());
        endPoint = wheelManager.endPoint;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAIStateChange();
        UpdateAIState();
    }


    void CheckAIStateChange()
    {
        //if (curState == AIState.Approach)
        //{
        //    if (CheckInAttackZone())
        //    {
        //        ExitApprochState();
        //        EnterAttackState();
        //    }
        //
        //}
        //if (curState == AIState.Attack)
        //{
        //    if (CheckIfFarAway() && CheckAttackCDOK())
        //    {
        //        ExitAttackState();
        //        EnterApproachState();
        //    }
        //}

    }

    void UpdateAIState()
    {
        switch (curState)
        {
            case AIState.Idle:
                UpdateIdleState();
                break;
            case AIState.Approach:
                UpdateApproachState();
                break;
                //case AIState.Attack:
                //    UpdateAttackState();
                //    break;
                //case AIState.Dead:
                //    UpdateDeadState();
                //    break;
                //case AIState.Flee:
                //    UpdateFleeState();
                //    break;
        }
    }

    void UpdateIdleState() { }

    #region Approach AI State
    void ExitApprochState() {; }
    void EnterApproachState() { curState = AIState.Approach; }
    void UpdateApproachState()
    {
        if (curTarget == null)
            SetToAnotherTargetDuringRepair();
        MoveToTarget();
    }

    void MoveToTarget()
    {
        Vector3 dir = (curTarget.transform.position - this.transform.position).normalized;
        dir.y = 0;
        dir = dir.normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime;
    }

    #endregion


    IEnumerator SetTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            SetTargetByHowCloseToEnd();
        }
    }
    void SetTargetByHowCloseToEnd()
    {
        if ((wheelManager.GetBackSeatByChair(wheelManager.curP1).transform.position - endPoint.position).magnitude <
            (wheelManager.GetBackSeatByChair(wheelManager.curP2).transform.position - endPoint.position).magnitude)
        {
            curTarget = wheelManager.GetBackSeatByChair(wheelManager.curP1);
        }
        else
        {
            curTarget = wheelManager.GetBackSeatByChair(wheelManager.curP2);
        }
    }

    public void SetToAnotherTargetDuringRepair()
    {
        if (curTarget == wheelManager.GetBackSeatByChair(wheelManager.curP1))
        {
            if (wheelManager.GetBackSeatByChair(wheelManager.curP2) != null)
            {
                curTarget = wheelManager.GetBackSeatByChair(wheelManager.curP2);
            }
        }
        if (curTarget == wheelManager.GetBackSeatByChair(wheelManager.curP2))
        {
            if (wheelManager.GetBackSeatByChair(wheelManager.curP1) != null)
            {
                curTarget = wheelManager.GetBackSeatByChair(wheelManager.curP1);
            }
        }
        if (curTarget == null)
        {
            if (wheelManager.GetBackSeatByChair(wheelManager.curP2) != null)
            {
                curTarget = wheelManager.GetBackSeatByChair(wheelManager.curP2);
            }
            if (wheelManager.GetBackSeatByChair(wheelManager.curP1) != null)
            {
                curTarget = wheelManager.GetBackSeatByChair(wheelManager.curP1);
            }
        }
    }
}
