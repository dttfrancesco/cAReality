﻿using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int current = 0;
    public float maxSpeed = 6;
    public float acceleration = 2f;
    [SerializeField] private float decelerationDistance = 0.2f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float WPradius = 0.2f;
    public bool isUnderBananaDominance = false;
    public bool isSelectedCharacter = false;
    public bool isUnderShield = false;
    public bool skipPowerUp = false;
    private float currentSpeed = 0f;

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Running)
        {
            return; // Do nothing if the game is paused
        }

        Vector3 targetDirection = waypoints[current].transform.position - transform.position;
        float distanceToTarget = targetDirection.magnitude;

        if (distanceToTarget < WPradius)
        {
            current = (current + 1) % waypoints.Length; // Move to the next waypoint
            return; // Skip rotation and movement for this frame to prevent erratic behavior
        }

        targetDirection.Normalize(); // Normalize the direction

        // Debugging lines
        Debug.DrawLine(transform.position, waypoints[current].transform.position, Color.red);

        // Rotate towards the target waypoint
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Accelerate or decelerate
        if (distanceToTarget < decelerationDistance)
        {
            currentSpeed = Mathf.Max(currentSpeed - (acceleration * Time.deltaTime), 0f);
        }
        else
        {
            currentSpeed = Mathf.Min(currentSpeed + (acceleration * Time.deltaTime), maxSpeed);
        }

        // Move the object
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    public void makeChacraterSelected()
    {
        isSelectedCharacter = true;
    }

    public void makeUnderShieldProtection()
    {
        isUnderShield = true;
    }

    public void removeShieldProtection()
    {
        isUnderShield = false;
    }

    public void skipOnePowerUp()
    {
        skipPowerUp = true;
    }

    public void RestorePowerUp()
    {
        skipPowerUp = false;
    }

}
