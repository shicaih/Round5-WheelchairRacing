using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearNurse : MonoBehaviour
{
    float timeCounter = 0.0f;
    public float totalTime = 5.0f;
    float multi = 1.0f;
    public float moveSpeed = 1.0f;

    Quaternion original;
    Quaternion opposite;

    public float turnTime = 0.5f;
    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        original = this.transform.rotation;
        opposite = Quaternion.LookRotation(-this.transform.forward, this.transform.up);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter <= totalTime)
            {
                this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                timeCounter = 0.0f;
                multi *= -1.0f;
                if (multi > 0)
                {
                    StartCoroutine(TurnAround(original));
                }
                else
                {
                    StartCoroutine(TurnAround(opposite));
                }
                canMove = false;
                StartCoroutine(WaitTillTurn());

            }
        }


    }

    IEnumerator TurnAround(Quaternion target)
    {
        while (this.transform.rotation != target)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target, 15 * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator WaitTillTurn()
    {
        yield return new WaitForSecondsRealtime(turnTime);
        canMove = true;
    }
}
