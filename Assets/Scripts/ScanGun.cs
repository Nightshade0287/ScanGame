using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScanGun : MonoBehaviour
{
    [Header("Gun Specs")]
    public float maxScanSpread;
    public float minScanSpread;
    public float maxScanDistance;
    public float scanRate;
    public float scanIntensity;

    [Header("References")]
    public GameObject dot;
    public Transform ScanPoint;
    public LayerMask NormalScan;
    public InputActionProperty TriggerInput;

    private float spread;
    private float triggerValue;
    private Vector3 scanDir;
    private bool allowScan = true;

    public void Update()
    {
        triggerValue = TriggerInput.action.ReadValue<float>();

        // Use the trigger value to calculate the spread of the scan gun
        spread = Mathf.Lerp(minScanSpread, maxScanSpread, triggerValue);

        if(allowScan && triggerValue > 0.02f)
        {
            for(int i = 0; i < scanIntensity; i++)
            {
                Scan();
            }
        }  
    }

    public void Scan()
    {
        allowScan = false;
        //Calculate a new Scan Direction
        CalculateScanDir();

        RaycastHit hit;
        if(Physics.Raycast(ScanPoint.position, scanDir, out hit, maxScanDistance, NormalScan, QueryTriggerInteraction.Ignore))
        {
            GameObject newDot = Instantiate(dot, hit.point, Quaternion.identity);
        }
        
        Invoke("resetScan", scanRate);
    }

    public void resetScan()
    {
        allowScan = true;
    }

    public void CalculateScanDir()
    {
        float x = Random.Range(-spread,spread);
        float y = Random.Range(-spread,spread);
        float z = Random.Range(-spread,spread);

        scanDir = (ScanPoint.forward + new Vector3(x,y,z)).normalized;
    }
}

