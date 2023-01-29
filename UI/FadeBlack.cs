using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlack : MonoBehaviour
{
    [SerializeField] int seconds = 2;
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSecondsRealtime(seconds);

        gameObject.GetComponent<Animator>().SetTrigger("FadeIn");
        yield break;
    }
}
