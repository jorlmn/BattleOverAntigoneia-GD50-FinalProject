using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private ShipEngine shipEngine;
    private Vector2 playerVelocity;
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        shipEngine = GetComponent<ShipEngine>();

        rb.freezeRotation = true;
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
        rb.AddTorque(transform.up * shipEngine.shipVelocities["rotation"] * playerVelocity.x, ForceMode.Acceleration);

        rb.AddForce(transform.forward * shipEngine.shipVelocities[shipEngine.currentVelocity] * playerVelocity.y, ForceMode.Acceleration);
    }

    public void ToggleTurbo()
    {
        shipEngine.ToggleTurbo();
    }

    public void ToggleStealth()
    {
        shipEngine.ToggleStealth();
    }
}
