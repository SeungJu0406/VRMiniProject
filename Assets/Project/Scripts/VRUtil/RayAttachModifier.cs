using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayAttachModifier : MonoBehaviour
{
    IXRSelectInteractable selectInteractable;

    protected void OnEnable()
    {
        selectInteractable = GetComponent<IXRSelectInteractable>();
        if (selectInteractable as Object == null)
        {
            Debug.Log("¾ÈµÊ");
            return;
        }
        selectInteractable.selectEntered.AddListener(OnSelectEntered);
    }
    protected void OnDisable()
    {
        if (selectInteractable != null)
        {
            selectInteractable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRRayInteractor == false) return;

        Transform atttachTransform = args.interactorObject.GetAttachTransform(args.interactableObject);
        Pose originAttachTransform = args.interactorObject.GetLocalAttachPoseOnSelect(args.interactableObject);
        atttachTransform.SetLocalPose(originAttachTransform);
       
    }

}
