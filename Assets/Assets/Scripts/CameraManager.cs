using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    private CinemachineBasicMultiChannelPerlin noiseCamera;

    private void Start() {
        noiseCamera = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ScreenShake(){
        StartCoroutine(ShakeCamera(shakeIntensity, shakeTime));

    }

    IEnumerator ShakeCamera(float intensity, float time){
        float startIntensity = intensity;
        float startTime = time;
        noiseCamera.m_AmplitudeGain = startIntensity;
        while(startTime > 0){
            startTime -= Time.deltaTime;
            noiseCamera.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f,  1 - (startTime/time));
            Debug.Log(startTime);
            yield return null;
        }
    }
}
