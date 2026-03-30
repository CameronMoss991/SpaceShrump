using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake S; // Singleton

    [Header("Inscribed")]
    public float shakeAmount = 0.1f; // How far it moves
    public float shakeDuration = 0.2f; // How long it lasts

    private Vector3 originalPos;

    void Awake() {
        if (S == null) S = this;
        originalPos = transform.localPosition;
    }

    public void Shake() {
        // Stop any current shake before starting a new one
        CancelInvoke("DoShake"); 
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", shakeDuration);
    }

    void DoShake() {
        // Randomly offset the camera around its original position
        Vector3 randomPos = originalPos + Random.insideUnitSphere * shakeAmount;
        // Keep the Z the same so the camera doesn't zoom in/out
        transform.localPosition = new Vector3(randomPos.x, randomPos.y, originalPos.z);
    }

    void StopShake() {
        CancelInvoke("DoShake");
        transform.localPosition = originalPos; // Reset to perfect center
    }
}