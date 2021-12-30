using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETCFontFall : MonoBehaviour
{


    IEnumerator lalala()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * 4.0f, ForceMode.Impulse);
        this.GetComponent<Rigidbody>().AddForceAtPosition(-Vector3.up * 2.0f, this.transform.right, ForceMode.Impulse);
        this.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.left * 1.5f, this.transform.right, ForceMode.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lalala());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "nurse_ani")
        {
            GameObject nurse = collision.gameObject;
            nurse.transform.parent.GetComponent<LinearNurse>().enabled = false;
            nurse.GetComponent<Rigidbody>().AddForce(
                (nurse.transform.position - this.transform.position).normalized
                * 4.0f, ForceMode.Impulse);
        }
    }
}
