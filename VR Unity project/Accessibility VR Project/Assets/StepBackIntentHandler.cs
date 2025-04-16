using System.Collections;
using UnityEngine;
using Meta.WitAi.Json;
using Meta.WitAi.CallbackHandlers;
using Meta.WitAi.Utilities;

public class StepBackIntentHandler : WitIntentMatcher
{
    protected override void OnResponseSuccess(WitResponseNode response)
    {
        Debug.Log(" ---------------------SUCCESS BACKWARD---------------");
        Debug.Log(response.ToString());

        int steps = 1;
        //try to get number from response 
        var stepValue = WitResultUtilities.GetFirstEntityValue(response, "wit$number:number");
        Debug.Log(stepValue + "steps back");
        //moving more than 1 so use it as step count
        if (!string.IsNullOrEmpty(stepValue) && int.TryParse(stepValue, out var parsed))
        {
            steps = parsed;
        }

        GameObject xrRig = GameObject.Find("XR Origin (XR Rig)");
        //start move
        StartCoroutine(MoveBackSteps(xrRig, steps));
        
    }
    //used to move the plaer forward in a more natural way instead of teleporting
    private IEnumerator MoveBackSteps(GameObject target, int steps)
    {
        float stepDistance = 0.5f;
        float durationPerStep = 0.2f;
        //grab controller
        CharacterController controller = target.GetComponent<CharacterController>();
        Transform head = Camera.main.transform;
        //move in the opposite direction the head is pointing 
        Vector3 moveDir = -new Vector3(head.forward.x, 0, head.forward.z).normalized;


        for (int i = 0; i < steps; i++)
        {
            float elapsed = 0;
            while (elapsed < durationPerStep)
            {
                controller.Move(moveDir * (stepDistance / durationPerStep) * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }

    protected override void OnResponseInvalid(WitResponseNode response, string error)
    {
        Debug.LogWarning(" Invalid response for back movement: " + error);
        Debug.LogWarning("Full response: " + response?.ToString());
    }
}
