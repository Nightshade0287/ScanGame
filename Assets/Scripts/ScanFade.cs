using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScanFade : MonoBehaviour
{
    public float maxFadeTime;
    public float minFadeTime;
    public InputActionProperty TriggerInput;

    public float fadeTime;
    public float triggerValue;

    public void Start()
    {
        triggerValue = TriggerInput.action.ReadValue<float>();
        fadeTime = Mathf.Lerp(5f, 20f, triggerValue);
        Invoke("RemoveDot", fadeTime);
    }

    public void RemoveDot()
    {
        Destroy(gameObject);
    }
}
