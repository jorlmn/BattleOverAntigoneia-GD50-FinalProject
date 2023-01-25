using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxControl : MonoBehaviour
{
    void Start()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Random.Range(0, 360));
    }
}
