using System;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomWheelRotation : MonoBehaviour
{

    [SerializeField] Transform wheelCenter;
    [SerializeField] Transform rotationReferenceLeft;
    [SerializeField] Transform rotationReferenceRight;

    [SerializeField] float epsilon = 0.0001f;
    
    void Update() {
        if(rotationReferenceLeft.parent != null && rotationReferenceRight.parent != null) return; // Objeto no está agarrado
        
        Vector3 OR = new Vector3(0,0,0);
        float offset = 0f;
        
        if(rotationReferenceLeft.parent == null && rotationReferenceRight.parent != null) {
            OR = rotationReferenceLeft.position;
            offset = 45f;
        } else if(rotationReferenceLeft.parent != null && rotationReferenceRight.parent == null) {
            OR = rotationReferenceRight.position;
            offset = -45f;
        } else if(rotationReferenceLeft.parent == null && rotationReferenceRight.parent == null) {
            OR = (rotationReferenceLeft.position + rotationReferenceRight.position) / 2;
            Debug.Log("OR: " + OR);
        }
        
        Vector3 OV = wheelCenter.position;
        
        Vector3 VR = OR - OV;

        VR = Vector3.ProjectOnPlane(VR, wheelCenter.forward); // Proyectamos al plano dado por el volante

        float finalRot = Mathf.Atan2(Vector3.Dot(wheelCenter.up, VR), Vector3.Dot(wheelCenter.right, VR)) * Mathf.Rad2Deg; // Entre -180 y 180 grados
        finalRot = -finalRot; // Mi rotación local va al revés
        finalRot += offset;
        if (finalRot <= 0f) finalRot += 360f;
        if (finalRot >= 360f) finalRot -= 360f;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, finalRot, transform.localEulerAngles.z);
    }
}
