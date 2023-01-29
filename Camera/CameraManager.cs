using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera to Aim At Following Target")]
    public Transform playerShipTarget;

    [Header("Camera Object and Settings")]
    [SerializeField] float maxZoomDistance = 25f;
    [SerializeField] float minZoomDistance = 10f;
    [SerializeField] float defaultIdleZoomDistance = 15f;
    [SerializeField] float aimingMinZoomDistance = 15f;
    [SerializeField] float cameraSensitivity = 0.5f;
    [SerializeField] float scrollWheelSensitivity = 50f;
    [SerializeField] float cameraMinInclination = 25f;
    [SerializeField] float cameraMaxInclination = 60f;
    [SerializeField] float minDistanceToInclinate = 13f;


    private float currentZoomDistance;
    private float aimingZoomDistance = 20f;
    private bool resettingZoom = false;
    private bool adjustedZoomWhileAiming = false;
    [SerializeField] float zoomResetSpeed = 0.15f;


    [Header("Camera Parent Objects")]
    public Transform cameraGrandParent;
    public Transform cameraParent;

    private float cameraParentXposition;
    private float cameraParentZposition;
    private Vector3 cameraGrandParentPivotEuler;
    private Vector3 cameraParentRotation;
    private Vector3 cameraPivotVector3 = Vector3.zero;
    private Vector3 cameraDistanceVector3 = Vector3.zero;
    private Vector3 cameraPivotVelocity = Vector3.zero;

    public void OnEnable()
    {
        currentZoomDistance = defaultIdleZoomDistance;

        cameraGrandParentPivotEuler = cameraGrandParent.eulerAngles;

        cameraParentRotation = cameraParent.eulerAngles;
    }

    private void LateUpdate()
    {
        FixCameraAtPlayer();

        if (StateManager.GameState == StateManager.gameStates.playState)
        {
            ZoomInAndOut();
            PivotSideways();

            AimingMovement();
        }
    }

    public void StartedAiming()
    {
        resettingZoom = false;
        adjustedZoomWhileAiming = false;
        StopAllCoroutines();
    }

    public void StoppedAiming()
    {
        StartCoroutine(CamPositionReset());
    }

    public void ResettingCam()
    {
        StartCoroutine(CamPositionReset());
        resettingZoom = true;
        adjustedZoomWhileAiming = false;
        StartCoroutine(CamZoomReset());
    }

    private void FixCameraAtPlayer()
    {
        cameraGrandParent.position = playerShipTarget.position;
    }
    private void ZoomInAndOut()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0 && !resettingZoom)
        {
            currentZoomDistance += Input.GetAxisRaw("Mouse ScrollWheel") * scrollWheelSensitivity * -1;

            currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDistance, maxZoomDistance);

            cameraDistanceVector3.y = currentZoomDistance;
            transform.localPosition = cameraDistanceVector3;
            
            SetInclination();
        }
    }

    private void PivotSideways()
    {
        if (Input.GetKey(Keybindings.keyCamPivot) && StateManager.AimState == StateManager.AimStates.notAiming)
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                cameraGrandParentPivotEuler.y += Input.GetAxis("Mouse X") * -1;
                cameraGrandParent.rotation = Quaternion.Euler(cameraGrandParentPivotEuler);
            }
        }
    }

    private void AimingMovement()
    {
        if (StateManager.AimState == StateManager.AimStates.aiming)
        {
            cameraParentXposition = cameraParent.localPosition.x;
            cameraParentXposition += Input.GetAxis("Mouse X") * cameraSensitivity;
            cameraParentXposition = Mathf.Clamp(cameraParentXposition, -20, 20);

            cameraParentZposition = cameraParent.localPosition.z;
            cameraParentZposition += Input.GetAxis("Mouse Y") * cameraSensitivity;
            cameraParentZposition = Mathf.Clamp(cameraParentZposition, -20, 20);

            cameraPivotVector3.x = cameraParentXposition;
            cameraPivotVector3.y = cameraParent.localPosition.y;
            cameraPivotVector3.z = cameraParentZposition;

            cameraParent.localPosition = cameraPivotVector3;

            if (adjustedZoomWhileAiming == false)
            {
                if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
                {
                    adjustedZoomWhileAiming = true;
                }

                aimingZoomDistance = Mathf.Lerp(aimingMinZoomDistance, maxZoomDistance, Mathf.Max(Mathf.Abs(cameraParentXposition), Mathf.Abs(cameraParentZposition)) / 20);
                currentZoomDistance = Mathf.SmoothStep(currentZoomDistance, aimingZoomDistance, zoomResetSpeed);

                cameraDistanceVector3.y = currentZoomDistance;
                transform.localPosition = cameraDistanceVector3;

                SetInclination();
            }
        }
    }

    private void SetInclination()
    {
        cameraParentRotation.x = Mathf.Lerp(cameraMaxInclination, cameraMinInclination, (cameraDistanceVector3.y - minZoomDistance) / minDistanceToInclinate);
        cameraParent.localRotation = Quaternion.Euler(cameraParentRotation);
    }

    IEnumerator CamPositionReset()
    {
        while (cameraParent.localPosition.x != 0 || cameraParent.localPosition.z != 0 || currentZoomDistance != defaultIdleZoomDistance)
        {
            yield return new WaitForEndOfFrame();
            cameraParent.localPosition = Vector3.SmoothDamp(cameraParent.localPosition, Vector3.zero, ref cameraPivotVelocity, cameraSensitivity);

            if (Vector3.Distance(cameraParent.localPosition, Vector3.zero) < 0.01)
            {
                cameraParent.localPosition = Vector3.zero;
                yield break;
            }
        }
    }

    IEnumerator CamZoomReset()
    {
        while (currentZoomDistance != defaultIdleZoomDistance)
        {
            yield return new WaitForEndOfFrame();
            
            currentZoomDistance = Mathf.SmoothStep(currentZoomDistance, defaultIdleZoomDistance, zoomResetSpeed);
            cameraDistanceVector3.y = currentZoomDistance;
            transform.localPosition = cameraDistanceVector3;

            SetInclination();

            if (Mathf.Abs(currentZoomDistance - defaultIdleZoomDistance) < 0.1)
            {

                currentZoomDistance = defaultIdleZoomDistance;
                cameraDistanceVector3.y = currentZoomDistance;
                transform.localPosition = cameraDistanceVector3;

                SetInclination();

                resettingZoom = false;
                yield break;
            }
        }
    }
}
