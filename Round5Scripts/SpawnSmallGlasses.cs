using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSmallGlasses : MonoBehaviour
{

    public bool inSpawn = false;
    public Vector3 dir = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartSpawn(Vector3 realDir)
    {
        dir = realDir;
        inSpawn = true;
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (inSpawn)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            GameObject go = GameObject.Instantiate<GameObject>(this.gameObject);
            go.GetComponent<Rigidbody>().AddForce(dir.normalized * 50.0f, ForceMode.Force);
            Destroy(go, 0.5f);
        }
    }
}
