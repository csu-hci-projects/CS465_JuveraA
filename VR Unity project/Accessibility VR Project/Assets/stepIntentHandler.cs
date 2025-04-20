using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi.Json;
using Meta.WitAi.CallbackHandlers;
using Meta.WitAi.Utilities;

public class StepIntentHandler : WitIntentMatcher
{
    // Called when the intent is matched successfully (e.g., "forward 3 steps")
    protected override void OnResponseSuccess(WitResponseNode response)
    {
        Debug.Log("----------------------SUCCESS FORWARD---------------------.");
        Debug.Log(response.ToString());

        int steps = 1;

        // Try to get the number of steps from Wit.ai's response
        var stepValue = WitResultUtilities.GetFirstEntityValue(response, "wit$number:number");
        Debug.Log(stepValue + " Steps");

        // If a valid number was found, parse it
        if (!string.IsNullOrEmpty(stepValue) && int.TryParse(stepValue, out int parsed))
        {
            steps = parsed;
        }

        // Find the XR Rig (your VR player object)
        GameObject xrRig = GameObject.Find("XR Origin (XR Rig)");

        // Start movement coroutine
        // Reference: https://discussions.unity.com/t/smooth-forward-movement-with-a-coroutine/207724/2
        StartCoroutine(MoveSteps(xrRig.transform, steps));
    }

    // Coroutine to move the player smoothly over time in the direction of their head
    private IEnumerator MoveSteps(Transform target, int steps)
    {
        float stepDistance = 0.5f;         // Distance per step in meters
        float durationPerStep = 0.2f;      // Duration for one step (for smooth movement)
        float totalDistance = steps * stepDistance;
        float distanceMoved = 0f;

        // Grab the CharacterController for movement
        CharacterController controller = target.GetComponent<CharacterController>();

        // Continue moving until the full step distance is completed
        while (distanceMoved < totalDistance)
        {
            float moveSpeed = stepDistance / durationPerStep;

            // Get current head direction every frame
            Transform head = Camera.main.transform;
            Vector3 moveDir = new Vector3(head.forward.x, 0, head.forward.z).normalized;

            // Calculate how far to move this frame
            Vector3 move = moveDir * moveSpeed * Time.deltaTime;

            // Move the XR Rig
            controller.Move(move);

            // Track how far we've moved in total
            distanceMoved += move.magnitude;

            yield return null; // Wait for next frame
        }
    }

    // Called if the response is invalid or doesn't match the intent
    protected override void OnResponseInvalid(WitResponseNode response, string error)
    {
        Debug.LogWarning(error);
        Debug.LogWarning(response?.ToString());
    }
}
