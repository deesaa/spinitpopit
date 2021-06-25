using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimView : MonoBehaviour
{
    public Transform leftRangePivot;
    public Transform rightRangePivot;
    public Transform arrowPivot;

    private float leftRangeAngle, rightRangeAngle;

    private float currentRotation;
    
    public void Awake()
    {
        leftRangeAngle = leftRangePivot.eulerAngles.z;
        rightRangeAngle = rightRangePivot.eulerAngles.z;
    }

    public void SetAngleRange(float angle)
    {
        leftRangePivot.localRotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        rightRangePivot.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        leftRangeAngle = leftRangePivot.eulerAngles.z;
        rightRangeAngle = rightRangePivot.eulerAngles.z;
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }

    public void SetAngle(Quaternion rotation, bool clamp)
    {
        if (clamp)
        {
            float newAngle = rotation.eulerAngles.z;
            float angleRange = leftRangeAngle + 360 - rightRangeAngle;
        
            float k = newAngle / 360f;
            float d = angleRange * k;
        
            newAngle = rightRangeAngle + d;
        
            arrowPivot.rotation = Quaternion.Euler(0, 0, newAngle);
        }
        else
        {
            Debug.Log(rotation.eulerAngles.z);
            arrowPivot.rotation = rotation;
        }
        
    }
}
