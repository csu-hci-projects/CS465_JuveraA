using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTurnBasedRotation : MonoBehaviour
{
    public Transform headset;          // Assign the Main Camera in Inspector
    public float turnThreshold = 45f;  // Degrees from forward to trigger turn
    public float turnSpeed = 60f;      // Degrees per second

    void Update()
    {
        float yRotation = headset.localEulerAngles.y;

        // Normalize angle to range [-180, 180]
        if (yRotation > 180f)
            yRotation -= 360f;

        // Rotate rig if head turns far enough
        if (yRotation > turnThreshold)//turn right
        {
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }
        else if (yRotation < -turnThreshold)//turn left
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        }
    }
}
