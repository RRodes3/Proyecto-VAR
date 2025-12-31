using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabRotation : XRGrabInteractable{
    [SerializeField] Transform originalTransform;

    protected override void OnSelectExiting(SelectExitEventArgs args) {
        base.OnSelectExiting(args);
        this.transform.gameObject.transform.position = originalTransform.position;
        this.transform.SetParent(originalTransform.parent);
    }

}
