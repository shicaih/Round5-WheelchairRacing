using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallTrigger : MonoBehaviour
{
    public GameObject parentTree;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wheel"))
        {
            if (!other.transform.parent.GetComponent<WheelChairController>().dontCheckWin)
                parentTree.GetComponent<TreeFall>().StartTreeFall();
        }

    }
}
