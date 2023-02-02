using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWreckage : MonoBehaviour
{
    private Rigidbody rb = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb = rb == null ? GetComponent<Rigidbody>() : rb;
        rb.velocity = Vector3.zero;
        StartCoroutine(RotateWreck());
    }

    IEnumerator RotateWreck()
    {
        yield return new WaitForSeconds(0.5f);
        int leftOrRight = Random.Range(0,1) == 0 ? -1: 1;
        rb.AddTorque(transform.forward * Random.Range(0, 10) * leftOrRight, ForceMode.Acceleration);

        yield break;
    }
}
