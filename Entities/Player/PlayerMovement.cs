using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ShipDataSO shipData;
    public float currentVelocity = 20f;
    private Vector2 playerVelocity;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currentVelocity = shipData.standardVelocity;
    }

    void Update()
    {
        GetPlayerInput();
    }

    void FixedUpdate()
    {
        MovePlayerShip();
    }

    void GetPlayerInput()
    {
        Vector2 playerInputs = new Vector2(InputManager.horizontalInput, InputManager.verticalInput);

        playerVelocity = playerInputs.normalized;
    }

    void MovePlayerShip()
    {
        rb.AddTorque(transform.up * shipData.rotationSpeed * playerVelocity.x, ForceMode.Acceleration);

        rb.AddForce(transform.forward * currentVelocity * playerVelocity.y, ForceMode.Acceleration);
    }

    public void ToggleTurbo()
    {
        currentVelocity = currentVelocity == shipData.turboVelocity ? shipData.standardVelocity : shipData.turboVelocity;
    }

    public void ToggleStealth()
    {
        currentVelocity = currentVelocity == shipData.stealthVelocity ? shipData.standardVelocity : shipData.stealthVelocity;
    }
}
