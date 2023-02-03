using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipWreckage : MonoBehaviour
{
    private Rigidbody rb = null;

    [SerializeField] List<ParticleSystem> explosions = new ();
    List<int> inactiveExplosionsIndex = new();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb = rb == null ? GetComponent<Rigidbody>() : rb;
        rb.velocity = Vector3.zero;

        for (int i = 0; i < explosions.Count; i++)
        {
            inactiveExplosionsIndex.Add(i);
        }

        int explosionQuantity = Random.Range((int)(explosions.Count/2), explosions.Count);

        while (explosionQuantity > 0)
        {
            int randomExplosion = Random.Range(0, inactiveExplosionsIndex.Count - 1);

            explosions[inactiveExplosionsIndex[randomExplosion]].gameObject.SetActive(true);
            inactiveExplosionsIndex.Remove(randomExplosion);

            explosionQuantity--;
        }


        StartCoroutine(RotateWreck());
    }

    IEnumerator RotateWreck()
    {
        yield return new WaitForSeconds(0.5f);
        int leftOrRight = Random.Range(0,1) == 0 ? -1: 1;
        rb.AddTorque(transform.forward * Random.Range(1, 10) * leftOrRight, ForceMode.Acceleration);

        yield break;
    }
}
