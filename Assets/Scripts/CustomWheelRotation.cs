using System;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomWheelRotation : MonoBehaviour
{

    [SerializeField] Transform rotationReference;
    [SerializeField] float epsilon = 0.0001f;

    void Update() {
        if(rotationReference.parent != null) return; // Objeto no está agarrado
        
        Vector3 OA = transform.position;
        Vector3 OB = rotationReference.position;

        Vector3 AB = OB - OA;
        float alpha = Mathf.Atan(AB.x / AB.y);

        if(alpha * Mathf.Rad2Deg <= epsilon) return; // Llegó

        Debug.Log("Alpha: " + alpha);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + alpha * Mathf.Rad2Deg, transform.localEulerAngles.z);
    }
}
