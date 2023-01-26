using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SOReference : MonoBehaviour
{
    public static SOReference instance;
    public List<ProjectileSO> projectileSos;

    private void Awake()
    {
        if (SOReference.instance == null)
        {
            instance = this;
            projectileSos = projectileSos.OrderBy(o => o.id).ToList();
        }
        else if (SOReference.instance != this)
        {
            Destroy(gameObject);
        }
    }
}
