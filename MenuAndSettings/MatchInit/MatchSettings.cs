using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSettings : MonoBehaviour
{

    public static MatchSettings instance;

    public static int shipChosenIndex = 0;
    private void Awake()
    {
        if (MatchSettings.instance == null)
        {
            instance = this;
        }
        else if (MatchSettings.instance != this)
        {
            Destroy(gameObject);
        }
    }
}
