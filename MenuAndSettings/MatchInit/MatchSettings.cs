using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSettings : MonoBehaviour
{

    public static MatchSettings instance;

    public static int shipChosenIndex = 0;

    public static Dictionary<string, AIDataSO> difficultyPresets = new();
    private void Awake()
    {
        if (MatchSettings.instance == null)
        {
            instance = this;

            foreach(AIDataSO aiData in SOReference.instance.difficultyPresets)
            {
                if (!difficultyPresets.ContainsKey(aiData.preset))
                {
                    difficultyPresets.Add(aiData.preset, aiData);
                }
            }
        }
        else if (MatchSettings.instance != this)
        {
            Destroy(gameObject);
        }
    }


}
