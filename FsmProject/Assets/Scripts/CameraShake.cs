using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    [SerializeField] private float globalImpulseForce = 1f;
    [SerializeField] private CinemachineImpulseListener impulseListener;
    private CinemachineImpulseDefinition impulseDefinition;
    private void Awake()
    {
        instance = this;
    }

    public void ShakeCamera(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalImpulseForce);
    }

    public void ScreenShakeFromProfile(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
    {
        //apply settings
        SetupScreenShakeSettings(profile, impulseSource);

        //shake
        impulseSource.GenerateImpulseWithForce(profile.impactForce);

    }

    public void SetupScreenShakeSettings(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
    {
        impulseDefinition = impulseSource.m_ImpulseDefinition;
        impulseDefinition.m_ImpulseDuration = profile.impactTime;
        impulseSource.m_DefaultVelocity = profile.defaultVelocity;


        impulseListener.m_ReactionSettings.m_AmplitudeGain = profile.listenerAmplitude;
        impulseListener.m_ReactionSettings.m_FrequencyGain = profile.listenerFrequency;
        impulseListener.m_ReactionSettings.m_Duration = profile.listenerDuration;

    }
}
