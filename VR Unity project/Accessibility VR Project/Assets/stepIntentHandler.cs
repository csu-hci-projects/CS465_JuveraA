using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi.Json;
using Meta.WitAi.CallbackHandlers;
using Meta.WitAi.Utilities;  



public class StepIntentHandler : WitIntentMatcher {

    protected override void OnResponseSuccess(WitResponseNode response) {
        Debug.Log("----------------------SUCCESS FORWARD---------------------.");
        Debug.Log(response.ToString());

        int steps = 1; 
        //try to get number from response 
        var stepValue = WitResultUtilities.GetFirstEntityValue(response, "wit$number:number");
        Debug.Log(stepValue + " Steps");

        //moving more than 1 so use it as step count
        if (!string.IsNullOrEmpty(stepValue) && int.TryParse(stepValue, out int parsed)) {
            steps = parsed;
        }
        
        GameObject xrRig = GameObject.Find("XR Origin (XR Rig)");
        //start move

        //https://discussions.unity.com/t/smooth-forward-movement-with-a-coroutine/207724/2
        StartCoroutine(MoveSteps(xrRig.transform, steps));

    }
    //used to move the player forward in a more natural way instead of teleporting
    private IEnumerator MoveSteps(Transform target, int steps) {
        float stepDistance = 0.5f;
        float durationPerStep = 0.2f;
        //grab controller 
        CharacterController controller = target.GetComponent<CharacterController>();
        //https://stackoverflow.com/questions/47170638/how-do-i-make-my-character-move-in-the-direction-of-the-camera-unity
        //move in the direction the head is pointing 
        Transform head = Camera.main.transform;
        Vector3 moveDir = new Vector3(head.forward.x, 0, head.forward.z).normalized;
        //move one step at a time
        for (int i = 0; i < steps; i++) {
            float elapsed = 0;
            while (elapsed < durationPerStep)
            {
                //move smoothly
                controller.Move(moveDir * (stepDistance / durationPerStep) * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }



    protected override void OnResponseInvalid(WitResponseNode response, string error) {
        Debug.LogWarning(error);
        Debug.LogWarning(response?.ToString());
    }
}