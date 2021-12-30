using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject p1_Win;
    public GameObject p2_Win;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.gameObject.layer == LayerMask.NameToLayer("Wheel"))
        {
            if (collision.collider.transform.gameObject.GetComponentInParent<WheelChairController>().isP1)
            {
                p1_Win.SetActive(true);
            }
            else
            {
                p2_Win.SetActive(true);
            }
        }


    }
}
