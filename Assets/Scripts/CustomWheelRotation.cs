using System;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomWheelRotation : MonoBehaviour
{

    [SerializeField] Transform wheelCenter;
    [SerializeField] Transform rotationReference;
    [SerializeField] float epsilon = 0.0001f;
    
    void Update() {
        if(rotationReference.parent != null) return; // Objeto no está agarrado
        
        Vector3 OV = wheelCenter.transform.position;
        Vector3 OR = rotationReference.position;
        Vector3 VR = OR - OV;

        float finalRot = Mathf.Atan2(VR.y, VR.x) * Mathf.Rad2Deg; // Entre -180 y 180 grados
        finalRot = -finalRot; // Mi rotación local va al revés
        if(finalRot <= 0f) finalRot += 360f;
        if(finalRot >= 360f) finalRot -= 360f;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, finalRot, transform.localEulerAngles.z);
    }
}
