using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGlassesSplash : MonoBehaviour
{
    public GameObject smallGlassesGroup;
    public List<Transform> smallGlasses;
    public float glassesCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        glassesCount = smallGlassesGroup.transform.childCount;
        for (int i = 0; i < glassesCount; i++)
        {
            smallGlasses.Add(smallGlassesGroup.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SplashGlasses(Vector3 source)
    {
        foreach (Transform t in smallGlasses)
        {
            t.gameObject.GetComponent<MeshRenderer>().enabled = true;
            t.GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine(Splash(t, source));
            StartCoroutine(Smaller(t));
            //Destroy(t.gameObject, 5.0f);
            StartCoroutine(DeleteSmallGlass(t));
            t.GetComponent<SpawnSmallGlasses>().StartSpawn((this.transform.position - source).normalized);
        }
    }

    IEnumerator Splash(Transform t, Vector3 source)
    {
        yield return null;
        Vector3 dir = this.transform.position - source;
        t.GetComponent<Rigidbody>().AddForce(dir.normalized * 50.0f, ForceMode.Force);
    }

    IEnumerator Smaller(Transform t)
    {
        while (true && smallGlasses.Count == glassesCount)
        {
            t.transform.localScale *= 0.99f;
            yield return null;
        }
    }

    IEnumerator DeleteSmallGlass(Transform t)
    {
        yield return new WaitForSeconds(2.0f);
        t.GetComponent<SpawnSmallGlasses>().inSpawn = false;
        yield return new WaitForSeconds(1.0f);
        smallGlasses.Remove(t);
        Destroy(t.gameObject);
    }

}
