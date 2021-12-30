using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMiddle : MonoBehaviour
{
    public GameObject c1;
    public GameObject c2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(c1.transform.position, c2.transform.position, 0.5f);
    }
}
