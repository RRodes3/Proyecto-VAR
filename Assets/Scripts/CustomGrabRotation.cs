using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabRotation : XRGrabInteractable{
    [SerializeField] Transform oldParentTransform;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (attachTransform != null) {
            attachTransform.SetPositionAndRotation(transform.position, transform.rotation);
        }

        base.OnSelectEntering(args);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase) {
        transform.SetParent(oldParentTransform, true);
        base.ProcessInteractable(updatePhase);

        // if (isSelected) {
        //     Debug.Log("NormalRot: " + transform.rotation);
        //     // Queremos hacer como que el volante sigue en su contenedor, pasamos la rotación a espacio local
        //     Quaternion localRot = Quaternion.Inverse(oldParentTransform.rotation) * transform.rotation;
        //     Debug.Log("LocalRot: " + localRot);
        //     // Lo regresamos a coordenadas globales
        //     Quaternion corrected = oldParentTransform.rotation * localRot;
        //     Debug.Log("Corrected: " + corrected);
        //     transform.rotation = corrected;
        // }
    }
}
