using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;
using Meta.WitAi;
//https://developers.meta.com/horizon/documentation/unity/voice-sdk-tutorials-1
public class VoiceActivator : MonoBehaviour
{
    public AppVoiceExperience voiceExperience;

    private void Start()
    {
        voiceExperience = GetComponent<AppVoiceExperience>();
        // Listen for when a voice request completes so we can re-activate listening
        voiceExperience.VoiceEvents.OnRequestCompleted.AddListener(ActivateWit);

        voiceExperience.Activate();
    }

    private void ActivateWit()
    {
        voiceExperience.Activate();
    }
}
