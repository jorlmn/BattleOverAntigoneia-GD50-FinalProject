using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    [HideInInspector] public  Camera playerCamera;
    [SerializeField] LayerMask cameraToCrossHairLayerMask;

    public Vector3 aimPoint = Vector3.zero;

    void Update()
    {
        if (StateManager.AimState == StateManager.AimStates.aiming)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cameraToCrossHairLayerMask))
            {
                aimPoint = hit.point;
            }
        }
    }

}
