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

    [SerializeField] GameObject capsuleLeft;
    Renderer capsuleLeft_Renderer;
    [SerializeField] GameObject capsuleRight;
    Renderer capsuleRight_Renderer;

    [SerializeField] float epsilon = 0.0001f;

    public Material normalMaterial;
    public Material selectedMaterial;

    void Start()
    {
        capsuleLeft_Renderer = capsuleLeft.GetComponent<Renderer>();
        capsuleRight_Renderer = capsuleRight.GetComponent<Renderer>();
    }
    
    void Update() {
        if(rotationReferenceLeft.parent != null && rotationReferenceRight.parent != null) {
            capsuleLeft_Renderer.material = normalMaterial;
            capsuleRight_Renderer.material = normalMaterial;
            return;
        }
        
        Vector3 OR = new Vector3(0,0,0);
        float offset = 0f;
        
        if(rotationReferenceLeft.parent == null && rotationReferenceRight.parent != null) {
            OR = rotationReferenceLeft.position;
            offset = 45f;
            capsuleLeft_Renderer.material = selectedMaterial;
            capsuleRight_Renderer.material = normalMaterial;
        } else if(rotationReferenceLeft.parent != null && rotationReferenceRight.parent == null) {
            OR = rotationReferenceRight.position;
            offset = -45f;
            capsuleLeft_Renderer.material = normalMaterial;
            capsuleRight_Renderer.material = selectedMaterial;
        } else if(rotationReferenceLeft.parent == null && rotationReferenceRight.parent == null) {
            capsuleLeft_Renderer.material = selectedMaterial;
            capsuleRight_Renderer.material = selectedMaterial;
            OR = (rotationReferenceLeft.position + rotationReferenceRight.position) / 2;
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
